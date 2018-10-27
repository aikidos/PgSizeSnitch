namespace PgSizeSnitch.Configurations
{
    class DatabaseServerConfig
    {
        /// <summary>
        /// Наименование сервера.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Адрес сервера.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Порт сервера.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Общий размер всех жестких дисков на сервере (в гигабайтах).
        /// </summary>
        public decimal TotalDiskSizeInGb { get; set; }
    }
}
