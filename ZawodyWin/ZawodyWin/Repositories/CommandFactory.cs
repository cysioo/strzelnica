using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ZawodyWin.Repositories
{
    public class CommandFactory
    {
        public static SQLiteCommand CreateInsertCommand<T>(T model) where T : class
        {
            var query = new StringBuilder("INSERT INTO ");
            var parameters = GetInsertCommandParams(model);
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

        private static IEnumerable<CommandGenerationParam> GetInsertCommandParams<T>(T model) where T : class
        {
            var result = new List<CommandGenerationParam>();
            var modelInsertProperties = GetInsertProperties(model);

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

        private static IEnumerable<PropertyInfo> GetInsertProperties<T>(T model) where T : class
        {
            return GetDbProperties(model).Where(x => x.Name != "Id");
        }

        private static IEnumerable<PropertyInfo> GetDbProperties<T>(T model) where T : class
        {
            return model.GetType().GetProperties();
        }
    }
}
