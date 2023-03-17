//using Microsoft.Data.Sqlite;
//using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZawodyWin.DataModels;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using System.Data.SQLite;

namespace ZawodyWin.Repositories
{
    public class TournamentRepository
    {
        private string _connectionString = "Data Source=DB/TournamentDB.db;";
        public TournamentRepository() { 
        }

        public long Add(Tournament tournament)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
            INSERT INTO [Tournament]
                       ([Name]
                       ,[Date]
                       ,[OrganizerId]
                       ,[Place]
                       ,[LeadingRefereeId])
                 VALUES
                       ($name,
                       $date,
                       $organizerId,
                       $place,
                       $leadingRefereeId);
            SELECT last_insert_rowid();
                ";
                command.Parameters.AddWithValue("$name", tournament.Name);
                command.Parameters.AddWithValue("$date", tournament.Date);
                command.Parameters.AddWithValue("$organizerId", tournament.OrganizerId);
                command.Parameters.AddWithValue("$place", tournament.Place);
                command.Parameters.AddWithValue("$leadingRefereeId", tournament.LeadingRefereeId);

                var result = command.ExecuteScalar();
                return (long)result;
            }
        }
    }
}
