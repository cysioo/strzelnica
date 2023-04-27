using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using ZawodyWin.DataModels;

namespace ZawodyWin.DB
{
    public class DataContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(Settings.ConnectionString);

        public DbSet<Tournament> Tournaments { get; set; }
    }
}
