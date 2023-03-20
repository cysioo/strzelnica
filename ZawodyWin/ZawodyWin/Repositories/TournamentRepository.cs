//using Microsoft.Data.Sqlite;
//using SQLite;
using System.Data.SQLite;
using ZawodyWin.DataModels;

namespace ZawodyWin.Repositories
{
    public class TournamentRepository
    {
        private string _connectionString = "Data Source=DB/TournamentDB.db;";
        public TournamentRepository() { 
        }

        public long Add(Tournament tournament)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = CommandFactory.CreateInsertCommand<Tournament>(tournament);
                command.Connection = connection;
                var result = command.ExecuteScalar();
                return (long)result;
            }
        }
    }
}
