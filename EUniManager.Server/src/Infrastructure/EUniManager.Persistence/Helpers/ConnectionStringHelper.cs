namespace EUniManager.Persistence.Helpers;

using static EUniManager.Application.Constants.EnviromentalVariablesConstant;

public static class ConnectionStringHelper
{
    public static string GetConnectionString()
    {
        const string DATABASE_NAME_ENV_VARIABLE_NAME = "DATABASE_NAME";
        const string SA_PASSWORD_ENV_VARIABLE_NAME = "SA_PASSWORD";

        string canNotLoadEnvVariableMessage =
            string.Format(CAN_NOT_LOAD_ENV_VARIABLE_MESSAGE_TEMPLATE, DATABASE_NAME_ENV_VARIABLE_NAME);
        string databaseName = Environment.GetEnvironmentVariable(DATABASE_NAME_ENV_VARIABLE_NAME) ?? 
                              throw new ArgumentException(canNotLoadEnvVariableMessage);

        canNotLoadEnvVariableMessage =
            canNotLoadEnvVariableMessage.Replace(DATABASE_NAME_ENV_VARIABLE_NAME, SA_PASSWORD_ENV_VARIABLE_NAME);
        string saPassword = Environment.GetEnvironmentVariable(SA_PASSWORD_ENV_VARIABLE_NAME) ?? 
                            throw new ArgumentException(canNotLoadEnvVariableMessage);
        
        string connectionString = $"Data Source=sqlserver;Initial Catalog={databaseName};TrustServerCertificate=True;User ID=SA;Password={saPassword}";

        return connectionString;
    }
}