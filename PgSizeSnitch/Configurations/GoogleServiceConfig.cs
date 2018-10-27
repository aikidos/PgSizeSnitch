namespace PgSizeSnitch.Configurations
{
    class GoogleServiceConfig
    {
        /// <summary>
        /// Идентификатор клиента.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Секретный ключ клиента.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Id таблицы для хранения информации приложением.
        /// </summary>
        public string SpreadsheetId { get; set; }
    }
}
