namespace PgSizeSnitch.Model.Entities
{
    class DatabaseInfo
    {
        /// <summary>
        /// Наименование базы данных.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Размер базы данных в байтах.
        /// </summary>
        public long Size { get; set; }
    }
}
