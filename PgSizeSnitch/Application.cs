using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PgSizeSnitch.Configurations;
using PgSizeSnitch.Snitch.Interfaces;

namespace PgSizeSnitch
{
    class Application
    {
        private readonly ApplicationConfig _applicationConfig;
        private readonly GoogleServiceConfig _googleServiceConfig;
        private readonly IPgInfoService _pgInfo;
        private readonly IGoogleSheetsService _googleSheets;

        public Application(IOptions<ApplicationConfig> applicationConfig,
            IOptions<GoogleServiceConfig> googleConfig,
            IPgInfoService pgInfo,
            IGoogleSheetsService googleSheets)
        {
            _applicationConfig = applicationConfig.Value;
            _googleServiceConfig = googleConfig.Value;
            _pgInfo = pgInfo;
            _googleSheets = googleSheets;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            Console.Write("Google authorization...");
            await _googleSheets.AuthorizeAsync(cancellationToken);
            Console.WriteLine("ok");

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                // Собираем статистику по пространству, которое занимает каждая БД на сервере
                foreach (var serverConfig in _applicationConfig.DatabaseServers)
                {
                    Console.Write($"Refresh statistics for `{serverConfig.Name}` server...");

                    string serverName = serverConfig.Name;

                    string updateStatisticsDate = DateTime.Today.ToShortDateString();

                    var dbInfos = (await _pgInfo.GetDatabaseSizeAsync(serverConfig, cancellationToken))
                        .ToList();

                    // Получаем файл для хранения статистики
                    var spreadsheet = await _googleSheets.GetSpreadsheetAsync(_googleServiceConfig.SpreadsheetId, cancellationToken);
                    string spreadsheetId = spreadsheet.SpreadsheetId;

                    // Производим поиск листа
                    var serverSheet = spreadsheet.Sheets.FirstOrDefault(x => x.Properties.Title == serverName);

                    if (serverSheet == null)
                        // Если лист не найден, то добавляем новый
                        await _googleSheets.AddSheetAsync(spreadsheetId, serverName, cancellationToken);
                    else
                        // Иначе очищаем лист от устаревших данных
                        await _googleSheets.FillSheetAsync(spreadsheetId, serverName, 16, 64, string.Empty, cancellationToken);

                    // Подготавливаем данные для записи
                    // Заголовок
                    var values = new List<IList<object>> { new List<object> { "Сервер", "База данных", "Размер в ГБ", "Дата обновления" } };

                    // Информация о базах данных
                    decimal ConvertToGb(decimal bytes) => Math.Round(bytes / 1024m / 1024 / 1024, 2);

                    for (var i = 0; i < dbInfos.Count; i++)
                    {
                        var dbInfo = dbInfos[i];
                        values.Add
                        (
                            new List<object>
                            {
                                // Для первой записи записываем наименование сервера
                                i == 0 ? serverName : string.Empty,
                                dbInfo.Name,
                                ConvertToGb(dbInfo.Size),
                                updateStatisticsDate
                            }
                        );
                    }

                    // Дополнительная информация об оставшемся свободном месте
                    values.Add(new List<object>());
                    values.Add(new List<object> { string.Empty, "Свободно", serverConfig.TotalDiskSizeInGb - ConvertToGb(dbInfos.Sum(x => x.Size)) });

                    await _googleSheets.UpdateValuesAsync(spreadsheetId, serverName, values, cancellationToken);

                    Console.WriteLine("ok");
                }

                Console.WriteLine("Waiting...");
                await Task.Delay(_applicationConfig.RefreshDelay, cancellationToken);
            }
        }
    }
}
