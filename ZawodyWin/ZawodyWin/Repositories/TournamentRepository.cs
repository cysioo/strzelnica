//using Microsoft.Data.Sqlite;
//using SQLite;
using System;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.Windows.Markup;
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
                var command = CommandFactory.CreateInsertCommand(tournament);
                command.Connection = connection;
                var result = command.ExecuteScalar();
                return (long)result;
            }
        }

        internal bool Update(Tournament tournament)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = CommandFactory.CreateUpdateCommand(tournament);
                command.Connection = connection;
                var numberOfUpdatedRows = command.ExecuteNonQuery();
                if (numberOfUpdatedRows == 0)
                {
                    throw new Exception($"No tournament updated (expected to update tournament {tournament.Id}!.");
                }
                if (numberOfUpdatedRows > 1)
                {
                    throw new Exception($"more then 1 tournament updated (expected to update only tournament {tournament.Id}!.");
                }
                return numberOfUpdatedRows == 1;
            }
        }

        public Tournament? Get(long id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = CommandFactory.CreateGetByIdCommand<Tournament>(id);
                command.Connection = connection;

                using SQLiteDataReader rdr = command.ExecuteReader();
                var modelProperties = typeof(Tournament).GetProperties();
                if (rdr.Read())
                {
                    Tournament result = new Tournament();
                    for (var i = 0; i < modelProperties.Length; i++)
                    {
                        var property = modelProperties[i];
                        object? columnValue = null;

                        if (property.PropertyType == typeof(string))
                        {
                            columnValue = rdr.GetString(i);
                        }
                        else if (property.PropertyType == typeof(long))
                        {
                            columnValue = rdr.GetInt64(i);
                        }
                        else if (property.PropertyType == typeof(DateTime))
                        {
                            columnValue = rdr.GetDateTime(i);
                        }
                        else if (property.PropertyType == typeof(bool))
                        {
                            columnValue = rdr.GetBoolean(i);
                        }

                        modelProperties[i].SetValue(result, columnValue);
                    }

                    return result;
                }

                return null;
            }
        }
    }
}
