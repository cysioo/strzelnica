//using Microsoft.Data.Sqlite;
//using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Windows.Markup;
using ZawodyWin.DataModels;
using ZawodyWin.DB;

namespace ZawodyWin.Repositories
{
    public class OrganizerRepository
    {
        public OrganizerRepository() { 
        }

        public long Add(Organizer organizer)
        {
            using (var context = new DataContext())
            {
                context.Organizers.Add(organizer);
                context.SaveChanges();
                return organizer.Id;
            }
        }

        internal bool Update(Organizer organizer)
        {
            using (var context = new DataContext())
            {
                context.Organizers.Attach(organizer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var numberOfUpdatedRows = context.SaveChanges();
                if (numberOfUpdatedRows == 0)
                {
                    throw new InvalidOperationException($"No tournament updated (expected to update tournament {organizer.Id}!.");
                }
                if (numberOfUpdatedRows > 1)
                {
                    throw new InvalidOperationException($"more then 1 tournament updated (expected to update only tournament {organizer.Id}!.");
                }
                return numberOfUpdatedRows == 1;
            }
        }

        public Organizer? Get(long id)
        {
            using (var context = new DataContext())
            {
                return context.Organizers.Find(id);
            }
        }

        public IEnumerable<Organizer> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Organizers.ToList();
            }
        }
    }
}
