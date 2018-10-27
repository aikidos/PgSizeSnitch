using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using PgSizeSnitch.Configurations;
using PgSizeSnitch.Model.Entities;
using PgSizeSnitch.Snitch.Interfaces;

namespace PgSizeSnitch.Snitch
{
    class PgInfoService : IPgInfoService
    {
        public async Task<IEnumerable<DatabaseInfo>> GetDatabaseSizeAsync(DatabaseServerConfig config, CancellationToken cancellationToken)
        {
            var connectionString = new NpgsqlConnectionStringBuilder
            {
                Host = config.Host,
                Port = config.Port,
                Username = config.Username,
                Password = config.Password,
                Database = "postgres"
            }.ToString();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QueryAsync<DatabaseInfo>
                (
                    new CommandDefinition
                    (
                        @"select 
                            t1.datname as name, 
                            pg_database_size(t1.datname) as size 
                        from pg_database t1",
                        cancellationToken: cancellationToken
                    )
                );
            }
        }
    }
}
