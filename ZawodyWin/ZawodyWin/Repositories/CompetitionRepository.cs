using System.Collections.Generic;
using System.Linq;
using ZawodyWin.DataModels;
using ZawodyWin.DB;
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

            var competitionsToDelete = existingCompetitions.Where(x => !competitionModels.Any(m => m.Name.Trim().Equals(x.Name.Trim())));

            using (var context = new DataContext())
            {
                foreach (var competition in competitionsToInsert)
                {
                    context.Competitions.Add(competition);
                    context.SaveChanges();
                }

                foreach (var competition in competitionsToUpdate)
                {
                    context.Competitions.Attach(competition).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    var numberOfUpdatedRows = context.SaveChanges();
                }

                if (competitionsToDelete.Any())
                {
                    context.Competitions.RemoveRange(competitionsToDelete);
                    var numberOfUpdatedRows = context.SaveChanges();
                }
            }
        }

        public Competition? Get(long id)
        {
            using (var context = new DataContext())
            {
                return context.Competitions.Find(id);
            }
        }
        public IEnumerable<Competition> GetByTournamentId(long tournamentId)
        {
            using (var context = new DataContext())
            {
                return context.Competitions.Where(x => x.TournamentId == tournamentId).ToList();
            }
        }

        public IEnumerable<Competition> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Competitions.ToList();
            }
        }
    }
}
