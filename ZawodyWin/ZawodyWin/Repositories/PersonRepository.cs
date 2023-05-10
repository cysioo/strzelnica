using System;
using System.Collections.Generic;
using System.Linq;
using ZawodyWin.DataModels;
using ZawodyWin.DB;

namespace ZawodyWin.Repositories
{
    public class PersonRepository
    {
        public PersonRepository() { 
        }

        public Person? Get(long id)
        {
            using (var context = new DataContext())
            {
                return context.People.Find(id);
            }
        }

        public Person? Get(string name, string surname)
        {
            using (var context = new DataContext())
            {
                return context.People.FirstOrDefault(x => x.Name == name && x.Surname == surname);
            }
        }

        public IEnumerable<Person> FilterByName(string name)
        {
            using (var context = new DataContext())
            {
                return context.People.Where(x => x.Name.Contains(name));
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
