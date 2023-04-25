using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ZawodyWin.Repositories
{
    public class CommandFactory
    {
        public static SQLiteCommand CreateInsertCommand<T>(T model) where T : class
        {
            var query = new StringBuilder("INSERT INTO ");
            var parameters = GetNonKeyCommandParams(model);
            var columnString = string.Join(", ", parameters.Select(x => x.ColumnName));
            var valuesString = string.Join(", ", parameters.Select(x => x.ParamName));
            query.Append($"[{model.GetType().Name}] ({columnString}) VALUES ({valuesString});");
            query.Append("SELECT last_insert_rowid();");

            var command = new SQLiteCommand(query.ToString());
            foreach ( var parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.ParamName, parameter.ParamValue);
            }

            return command;
        }

        public static SQLiteCommand CreateUpdateCommand<T>(T model) where T : class
        {
            var query = new StringBuilder($"UPDATE {model.GetType().Name} SET ");
            var parameters = GetNonKeyCommandParams(model);
            foreach ( var parameter in parameters)
            {
                query.Append($"{parameter.ColumnName}={parameter.ParamName},");
            }
            query.Remove(query.Length - 1, 1);  // last coma
            query.Append($" WHERE Id=$id");

            var command = new SQLiteCommand(query.ToString());
            foreach (var parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.ParamName, parameter.ParamValue);
            }

            var idProperty = GetModelProperties<T>().First(x => x.Name == "Id");
            var id = idProperty.GetValue(model);
            command.Parameters.AddWithValue("$id", id);

            return command;
        }

        public static SQLiteCommand CreateGetByIdCommand<T>(long id) where T : class
        {
            var selectQuery = CreateSelectQuery<T>();
            var query = $"{selectQuery} WHERE Id=$id";

            var command = new SQLiteCommand(query.ToString());
            command.Parameters.AddWithValue("$id", id);

            return command;
        }

        public static SQLiteCommand CreateGetAllCommand<T>() where T : class
        {
            var query = CreateSelectQuery<T>();
            var command = new SQLiteCommand(query.ToString());

            return command;
        }

        public static string CreateSelectQuery<T>() where T : class
        {
            var modelProperties = GetModelProperties<T>();
            var columnString = string.Join(", ", modelProperties.Select(x => x.Name));
            return $"SELECT {columnString} FROM {typeof(T).Name}";
        }

        public static SQLiteCommand CreateFilterCommand<T>(string whereClause, IDictionary<string, object> filterParams) where T : class
        {
            var selectQuery = CreateSelectQuery<T>();
            var query = $"{selectQuery} WHERE {whereClause}";

            var command = new SQLiteCommand(query);
            foreach (var param in filterParams )
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }

            return command;
        }

        public static SQLiteCommand CreateDeleteCommand(string tableName, IEnumerable<long> idsToDelete)
        {
            var joinedIds = string.Join(", ", idsToDelete);
            var query = $"DELETE FROM {tableName} WHERE Id IN ({joinedIds})";
            return new SQLiteCommand(query);
        }

        private static long GetId<T>(T model) where T : class
        {
            var idProperty = GetModelProperties<T>().First(x => x.Name == "Id");
            var result = idProperty.GetValue(model);
            if (result == null)
            {
                throw new InvalidOperationException("Wrong Model - Id doesn't exist");
            }

            return (long)result;
        }

        private static IEnumerable<CommandGenerationParam> GetNonKeyCommandParams<T>(T model) where T : class
        {
            var result = new List<CommandGenerationParam>();
            var modelInsertProperties = GetNonKeyProperties(model);

            for (var i = 0; i < modelInsertProperties.Count(); i++)
            {
                var commandParam = new CommandGenerationParam();
                commandParam.ColumnName = modelInsertProperties.ElementAt(i).Name;
                commandParam.ParamName = $"$p{i}";
                commandParam.ParamValue = modelInsertProperties.ElementAt(i).GetValue(model);
                result.Add(commandParam);
            }

            return result;
        }

        private static IEnumerable<PropertyInfo> GetNonKeyProperties<T>(T model) where T : class
        {
            return GetModelProperties<T>().Where(x => x.Name != "Id");
        }

        private static IEnumerable<PropertyInfo> GetModelProperties<T>() where T : class
        {
            return typeof(T).GetProperties();
        }
    }
}
