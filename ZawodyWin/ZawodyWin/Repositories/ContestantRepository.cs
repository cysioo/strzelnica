using System;
using System.Collections.Generic;
using System.Linq;
using ZawodyWin.DataModels;
using ZawodyWin.DB;

namespace ZawodyWin.Repositories
{
    public class ContestantRepository
    {
        public ContestantRepository() { 
        }

        public Contestant? Get(long id)
        {
            using (var context = new DataContext())
            {
                return context.Contestants.Find(id);
            }
        }

        public IEnumerable<Contestant> GetContestantsForCompetitions(IEnumerable<long> competitionIds)
        {
            using (var context = new DataContext())
            {
                var contestants = context.Contestants.Where(x => competitionIds.Contains(x.CompetitionId)).Distinct();
                return contestants.ToList();
            }
        }

        public long Add(Contestant contestant)
        {
            using (var context = new DataContext())
            {
                context.Contestants.Add(contestant);
                context.SaveChanges();
                return contestant.Id;
            }
        }

        internal bool Update(Contestant contestant)
        {
            using (var context = new DataContext())
            {
                context.Contestants.Attach(contestant).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var numberOfUpdatedRows = context.SaveChanges();
                if (numberOfUpdatedRows == 0)
                {
                    throw new InvalidOperationException($"No tournament updated (expected to update tournament {contestant.Id}!.");
                }
                if (numberOfUpdatedRows > 1)
                {
                    throw new InvalidOperationException($"more then 1 tournament updated (expected to update only tournament {contestant.Id}!.");
                }
                return numberOfUpdatedRows == 1;
            }
        }
    }
}
