using System;
using System.Collections.Generic;
using System.Linq;
using ZawodyWin.DataModels;
using ZawodyWin.DB;

namespace ZawodyWin.Repositories
{
    public class ShootingClubRepository
    {
        public ShootingClubRepository() { 
        }

        public long Add(ShootingClub shootingClub)
        {
            using (var context = new DataContext())
            {
                context.ShootingClubs.Add(shootingClub);
                context.SaveChanges();
                return shootingClub.Id;
            }
        }

        internal bool Update(ShootingClub shootingClub)
        {
            using (var context = new DataContext())
            {
                context.ShootingClubs.Attach(shootingClub).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var numberOfUpdatedRows = context.SaveChanges();
                if (numberOfUpdatedRows == 0)
                {
                    throw new InvalidOperationException($"No tournament updated (expected to update tournament {shootingClub.Id}!.");
                }
                if (numberOfUpdatedRows > 1)
                {
                    throw new InvalidOperationException($"more then 1 tournament updated (expected to update only tournament {shootingClub.Id}!.");
                }
                return numberOfUpdatedRows == 1;
            }
        }

        public ShootingClub? Get(long id)
        {
            using (var context = new DataContext())
            {
                return context.ShootingClubs.Find(id);
            }
        }

        public IEnumerable<ShootingClub> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.ShootingClubs.ToList();
            }
        }
    }
}
