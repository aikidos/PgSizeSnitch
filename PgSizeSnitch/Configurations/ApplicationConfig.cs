using System;

namespace PgSizeSnitch.Configurations
{
    class ApplicationConfig
    {
        /// <summary>
        /// Сервера баз данных;
        /// </summary>
        public DatabaseServerConfig[] DatabaseServers { get; set; }

        /// <summary>
        /// Промежуток времени, через который необходимо обновлять статистику.
        /// </summary>
        public TimeSpan RefreshDelay { get; set; }
    }
}
