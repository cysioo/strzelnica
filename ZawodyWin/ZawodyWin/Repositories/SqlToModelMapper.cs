using System;
using System.Data;
using System.Data.SQLite;
using System.Reflection;

namespace ZawodyWin.Repositories
{
    public class SqlToModelMapper
    {
        public static object? GetColumnValueFromReader(SQLiteDataReader reader, PropertyInfo property)
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
