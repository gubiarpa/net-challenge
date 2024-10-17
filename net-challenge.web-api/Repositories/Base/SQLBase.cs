using Microsoft.Data.SqlClient;
using System.Data;

namespace net_challenge.web_api.Repositories.Base
{
    public abstract class SQLBase
    {
        private readonly string _connectionString;

        private IDictionary<string, object> _parameters;

        public SQLBase(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("KidsoDb");
        }

        protected void AddParameter(string parameterName, object value)
        {
            _parameters[parameterName] = value;
        }

        protected async Task<List<T>> ExecuteSPWithResults<T>(string spName, Func<SqlDataReader, T> mapFunction)
        {
            var results = new List<T>();

            await using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(spName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (_parameters is not null)
                            foreach (var key in _parameters.Keys)
                                command.Parameters.Add(new SqlParameter(key, _parameters[key]));

                        await using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Add(mapFunction(reader));
                            }
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    if (connection.State.Equals(ConnectionState.Open))
                        await connection.CloseAsync();
                }
            }

            return results;
        }

        protected async Task ExecuteSPWithoutResults(string spName, IEnumerable<SqlParameter> parameters)
        {
            await using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(spName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (parameters is null)
                            return;

                        foreach (var parameter in parameters)
                            command.Parameters.Add(parameter);

                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (connection.State.Equals(ConnectionState.Open))
                        await connection.CloseAsync();
                }
            }
        }
    }
}
