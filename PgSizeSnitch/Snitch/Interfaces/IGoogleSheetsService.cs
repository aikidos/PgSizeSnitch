using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4.Data;

namespace PgSizeSnitch.Snitch.Interfaces
{
    interface IGoogleSheetsService
    {
        /// <summary>
        /// True, если приложение было авторизованно.
        /// </summary>
        bool IsAuthorized { get; }

        /// <summary>
        /// Производит авторизацию приложения в аккаунте пользователя.
        /// </summary>
        Task AuthorizeAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает таблицу по указанному Id.
        /// </summary>
        Task<Spreadsheet> GetSpreadsheetAsync(string spreadsheetId, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет лист в указанную таблицу.
        /// </summary>
        Task AddSheetAsync(string spreadsheetId, string sheetName, CancellationToken cancellationToken);

        /// <summary>
        /// Заполняет указанный квадрат в листе определенным значением.
        /// </summary>
        Task FillSheetAsync(string spreadsheetId, string sheetName, int width, int height, object value, CancellationToken cancellationToken);

        /// <summary>
        /// Записывает указанные значения в лист.
        /// </summary>
        Task UpdateValuesAsync(string spreadsheetId, string sheetName, List<IList<object>> values, CancellationToken cancellationToken);
    }
}
