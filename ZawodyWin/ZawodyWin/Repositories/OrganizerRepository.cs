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
    public class OrganizerRepository
    {
        public OrganizerRepository() { 
        }

        public long Add(Organizer organizer)
        {
            using (var connection = new SQLiteConnection(Settings.ConnectionString))
            {
                connection.Open();
                var command = CommandFactory.CreateInsertCommand(organizer);
                command.Connection = connection;
                var result = command.ExecuteScalar();
                return (long)result;
            }
        }

        internal bool Update(Organizer organizer)
        {
            using (var connection = new SQLiteConnection(Settings.ConnectionString))
            {
                connection.Open();
                var command = CommandFactory.CreateUpdateCommand(organizer);
                command.Connection = connection;
                var numberOfUpdatedRows = command.ExecuteNonQuery();
                if (numberOfUpdatedRows == 0)
                {
                    throw new Exception($"No organizer updated (expected to update tournament {organizer.Id}!.");
                }
                if (numberOfUpdatedRows > 1)
                {
                    throw new Exception($"more then 1 organizer updated (expected to update only tournament {organizer.Id}!.");
                }
                return numberOfUpdatedRows == 1;
            }
        }

        public Organizer? Get(long id)
        {
            using (var connection = new SQLiteConnection(Settings.ConnectionString))
            {
                connection.Open();
                var command = CommandFactory.CreateGetByIdCommand<Organizer>(id);
                command.Connection = connection;

                using SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return GetOrganizer(reader);
                }

                return null;
            }
        }

        public IEnumerable<Organizer> GetAll()
        {
            using (var connection = new SQLiteConnection(Settings.ConnectionString))
            {
                connection.Open();
                var command = CommandFactory.CreateGetAllCommand<Organizer>();
                command.Connection = connection;

                using SQLiteDataReader reader = command.ExecuteReader();
                var result = new List<Organizer>();
                while (reader.Read())
                {
                    result.Add(GetOrganizer(reader));
                }

                return result;
            }
        }

        private static Organizer GetOrganizer(SQLiteDataReader reader)
        {
            var modelProperties = typeof(Organizer).GetProperties();
            Organizer result = new Organizer();
            for (var i = 0; i < modelProperties.Length; i++)
            {
                var property = modelProperties[i];
                object? columnValue = SqlToModelMapper.GetColumnValueFromReader(reader, property);

                modelProperties[i].SetValue(result, columnValue);
            }

            return result;
        }
    }
}
