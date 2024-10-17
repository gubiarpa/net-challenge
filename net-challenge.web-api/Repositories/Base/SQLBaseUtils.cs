using Microsoft.Data.SqlClient;
using System.Data;

namespace net_challenge.web_api.Repositories.Base
{
    public static class SqlBaseUtils
    {
        public static void AddParametersToCommand(this SqlCommand command, IDictionary<string, object> parameters)
        {
            if (parameters.Count > 0)
            {
                foreach (var key in parameters.Keys)
                {
                    command.Parameters.Add(new SqlParameter(key, parameters[key]));
                }
            }
        }

        public static async Task CleanUpAsync(this SqlConnection connection, IDictionary<string, object> parameters)
        {
            parameters.Clear();

            if (connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }
    }
}
