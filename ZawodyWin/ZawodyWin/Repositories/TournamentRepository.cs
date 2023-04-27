using System;
using System.Collections.Generic;
using System.Linq;
using ZawodyWin.DataModels;
using ZawodyWin.DB;

namespace ZawodyWin.Repositories
{
    public class TournamentRepository
    {
        public TournamentRepository() { 
        }

        public long Add(Tournament tournament)
        {
            using (var context = new DataContext())
            {
                context.Tournaments.Add(tournament);
                context.SaveChanges();
                return tournament.Id;
            }
        }

        internal bool Update(Tournament tournament)
        {
            using (var context = new DataContext())
            {
                context.Tournaments.Attach(tournament).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var numberOfUpdatedRows = context.SaveChanges();
                if (numberOfUpdatedRows == 0)
                {
                    throw new InvalidOperationException($"No tournament updated (expected to update tournament {tournament.Id}!.");
                }
                if (numberOfUpdatedRows > 1)
                {
                    throw new InvalidOperationException($"more then 1 tournament updated (expected to update only tournament {tournament.Id}!.");
                }
                return numberOfUpdatedRows == 1;
            }
        }

        public Tournament? Get(long id)
        {
            using (var context = new DataContext())
            {
                return context.Tournaments.Find(id);
            }
        }

        public IEnumerable<Tournament> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Tournaments.ToList();
            }
        }
    }
}
