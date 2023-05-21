using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ZawodyWin.DataModels;
using ZawodyWin.DB;

namespace ZawodyWin.Repositories
{
    public class RefereeRepository
    {
        public RefereeRepository() { 
        }

        public Referee? Get(long id)
        {
            using (var context = new DataContext())
            {
                return context.Referees.Find(id);
            }
        }

        public IEnumerable<Referee> GetRefereesForTournament(long tournamentId)
        {
            using (var context = new DataContext())
            {
                var referees = context.Referees.Where(x => x.TournamentId == tournamentId).Include(x => x.Person);
                return referees.ToList();
            }
        }

        public long Add(Referee referee)
        {
            using (var context = new DataContext())
            {
                context.Referees.Add(referee);
                context.SaveChanges();
                return referee.Id;
            }
        }

        internal bool Update(Referee referee)
        {
            using (var context = new DataContext())
            {
                context.Referees.Attach(referee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var numberOfUpdatedRows = context.SaveChanges();
                if (numberOfUpdatedRows == 0)
                {
                    throw new InvalidOperationException($"No Referee updated (expected to update Referee {referee.Id}!.");
                }
                if (numberOfUpdatedRows > 1)
                {
                    throw new InvalidOperationException($"more then 1 Referee updated (expected to update only Referee {referee.Id}!.");
                }
                return numberOfUpdatedRows == 1;
            }
        }
    }
}
