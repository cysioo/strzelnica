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

        public IEnumerable<Contestant> GetTournamentContestants(long tournamentId)
        {
            using (var context = new DataContext())
            {
                var competitionIds = context.Competitions.Where(x => x.TournamentId == tournamentId).Select(x => x.Id);
                var contestants = context.Contestants.Where(x => competitionIds.Contains(x.CompetitionId)).Distinct();
                return contestants.ToList();
            }
        }

        public IEnumerable<Person> FilterBySurname(string surname)
        {
            using (var context = new DataContext())
            {
                return context.People.Where(x => x.Surname.Contains(surname));
            }
        }

        public long Add(Person person)
        {
            using (var context = new DataContext())
            {
                context.People.Add(person);
                context.SaveChanges();
                return person.Id;
            }
        }

        internal bool Update(Person person)
        {
            using (var context = new DataContext())
            {
                context.People.Attach(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var numberOfUpdatedRows = context.SaveChanges();
                if (numberOfUpdatedRows == 0)
                {
                    throw new InvalidOperationException($"No tournament updated (expected to update tournament {person.Id}!.");
                }
                if (numberOfUpdatedRows > 1)
                {
                    throw new InvalidOperationException($"more then 1 tournament updated (expected to update only tournament {person.Id}!.");
                }
                return numberOfUpdatedRows == 1;
            }
        }
    }
}
