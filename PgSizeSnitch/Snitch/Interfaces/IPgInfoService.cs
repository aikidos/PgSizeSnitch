using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PgSizeSnitch.Configurations;
using PgSizeSnitch.Model.Entities;

namespace PgSizeSnitch.Snitch.Interfaces
{
    interface IPgInfoService
    {
        /// <summary>
        /// Возвращает информацию об имени и размере всех баз данных на сервере.
        /// </summary>
        Task<IEnumerable<DatabaseInfo>> GetDatabaseSizeAsync(DatabaseServerConfig config, CancellationToken cancellationToken);
    }
}
