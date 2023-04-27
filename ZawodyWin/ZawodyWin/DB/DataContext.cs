using Microsoft.EntityFrameworkCore;
using ZawodyWin.DataModels;

namespace ZawodyWin.DB
{
    public class DataContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(Settings.ConnectionString);

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<ShootingClub> ShootingClubs { get; set; }
        public DbSet<Competition> Competitions { get; set; }
    }
}
