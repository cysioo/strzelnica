//using Microsoft.Data.Sqlite;
//using SQLite;
using System;
using System.Collections.Generic;
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

                using SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return GetTournament(reader);
                }

                return null;
            }
        }

        public IEnumerable<Tournament> GetAll()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = CommandFactory.CreateGetAllCommand<Tournament>();
                command.Connection = connection;

                using SQLiteDataReader reader = command.ExecuteReader();
                var result = new List<Tournament>();
                while (reader.Read())
                {
                    result.Add(GetTournament(reader));
                }

                return result;
            }
        }

        private static Tournament GetTournament(SQLiteDataReader reader)
        {
            var modelProperties = typeof(Tournament).GetProperties();
            Tournament result = new Tournament();
            for (var i = 0; i < modelProperties.Length; i++)
            {
                var property = modelProperties[i];
                object? columnValue = GetColumnValueFromReader(reader, property);

                modelProperties[i].SetValue(result, columnValue);
            }

            return result;
        }

        private static object? GetColumnValueFromReader(SQLiteDataReader reader, PropertyInfo property)
        {
            //return reader.GetValue(property.Name);

            if (reader.IsDBNull(property.Name))
            {
                return null;
            }

            object? columnValue = null;
            if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
            {
                columnValue = reader.GetDateTime(property.Name);
            }
            else
            {
                columnValue = reader.GetValue(property.Name);
            }

            //if (property.PropertyType == typeof(string))
            //{
            //    columnValue = reader.GetString(property.Name);
            //}
            //else if (property.PropertyType == typeof(long))
            //{
            //    columnValue = reader.GetInt64(property.Name);
            //}
            //else if (property.PropertyType == typeof(DateTime))
            //{
            //    columnValue = reader.GetDateTime(property.Name);
            //}
            //else if (property.PropertyType == typeof(bool))
            //{
            //    columnValue = reader.GetBoolean(property.Name);
            //}

            return columnValue;
        }
    }
}
