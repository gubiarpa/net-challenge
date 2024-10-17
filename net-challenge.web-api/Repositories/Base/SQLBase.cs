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
            _parameters = new Dictionary<string, object>();
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
                        command.AddParametersToCommand(_parameters);

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
                    await connection.CleanUpAsync(_parameters);
                }
            }

            return results;
        }

        protected async Task ExecuteSPWithoutResults(string spName)
        {
            await using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(spName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.AddParametersToCommand(_parameters);

                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    await connection.CleanUpAsync(_parameters);
                }
            }
        }
    }
}
