using System.Data;

namespace BookingSystem.Storage
{
    public static class StorageHelper
    {
        public static IDbCommand CreateCommand(this IDbConnection dbConnection, string commandText)
        {
            IDbCommand command = dbConnection.CreateCommand();
            command.CommandText = commandText;
            return command;
        }

        public static void AddParameterWithValue<T>(this IDbCommand dbCommand, string parameterName, T value)
        {
            IDataParameter parameter = dbCommand.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            dbCommand.Parameters.Add(parameter);
        }
    }
}
