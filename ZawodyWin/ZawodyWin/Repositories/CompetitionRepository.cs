//using Microsoft.Data.Sqlite;
//using SQLite;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using ZawodyWin.DataModels;
using ZawodyWin.ViewModels;

namespace ZawodyWin.Repositories
{
    public class CompetitionRepository
    {
        public CompetitionRepository() { 
        }

        public void SetTournamentCompetitions(long tournamentId, IEnumerable<CompetitionViewModel> competitionModels)
        {
            var competitionsToInsert = new List<Competition>();
            var competitionsToUpdate = new List<Competition>();
            var existingCompetitions = GetByTournamentId(tournamentId);
            foreach (var model in competitionModels)
            {
                var competition = existingCompetitions.FirstOrDefault(x => x.Name == model.Name.Trim());
                if (competition != null)
                {
                    if (competition.NumberOfRounds != model.NumberOfRounds)
                    {
                        competition.NumberOfRounds = model.NumberOfRounds;
                        competitionsToUpdate.Add(competition);
                    }
                }
                else
                {
                    competition = model.ToDbModel();
                    competition.TournamentId = tournamentId;
                    competitionsToInsert.Add(competition);
                }
            }

            var competitionsToDelete = existingCompetitions.Where(x => !competitionModels.Any(m => m.Name.Trim().Equals(x.Name.Trim()))).Select(x => x.Id);

            using (var connection = new SQLiteConnection(Settings.ConnectionString))
            {
                connection.Open();
                foreach (var competition in competitionsToInsert)
                {
                    var command = CommandFactory.CreateInsertCommand(competition);
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }

                foreach (var competition in competitionsToUpdate)
                {
                    var command = CommandFactory.CreateUpdateCommand(competition);
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }

                if (competitionsToDelete.Any())
                {
                    var command = CommandFactory.CreateDeleteCommand("Competition", competitionsToDelete);
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
        }

        public Competition? Get(long id)
        {
            using (var connection = new SQLiteConnection(Settings.ConnectionString))
            {
                connection.Open();
                var command = CommandFactory.CreateGetByIdCommand<Competition>(id);
                command.Connection = connection;

                using SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return GetCompetition(reader);
                }

                return null;
            }
        }
        public IEnumerable<Competition> GetByTournamentId(long tournamentId)
        {
            using (var connection = new SQLiteConnection(Settings.ConnectionString))
            {
                connection.Open();
                var filterParams = new Dictionary<string, object>();
                filterParams["$id"] = tournamentId;
                var whereClause = "TournamentId=$id";
                var command = CommandFactory.CreateFilterCommand<Competition>(whereClause, filterParams);
                command.Connection = connection;
                using SQLiteDataReader reader = command.ExecuteReader();
                var result = new List<Competition>();
                while (reader.Read())
                {
                    result.Add(GetCompetition(reader));
                }

                return result;
            }
        }

        public IEnumerable<Competition> GetAll()
        {
            using (var connection = new SQLiteConnection(Settings.ConnectionString))
            {
                connection.Open();
                var command = CommandFactory.CreateGetAllCommand<Competition>();
                command.Connection = connection;

                using SQLiteDataReader reader = command.ExecuteReader();
                var result = new List<Competition>();
                while (reader.Read())
                {
                    result.Add(GetCompetition(reader));
                }

                return result;
            }
        }

        private static Competition GetCompetition(SQLiteDataReader reader)
        {
            var modelProperties = typeof(Competition).GetProperties();
            Competition result = new Competition();
            for (var i = 0; i < modelProperties.Length; i++)
            {
                var property = modelProperties[i];
                object? columnValue = SqlToModelMapper.GetColumnValueFromReader(reader, property);

                modelProperties[i].SetValue(result, columnValue);
            }

            return result;
        }
    }
}
