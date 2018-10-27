## Введение
В данном репозитории содержится решение тестового задания для компании "БАРС Груп".  
*Цель задания: разработать консольное приложение, которое определяет размер баз данных PostgreSQL на диске и записывает статистику в Google Таблицах.*

## Для решения были использованы
- [.NET Core 2.1](https://dotnet.github.io/)
- [Dapper](https://github.com/StackExchange/Dapper)
- [Google.Apis.Sheets.v4](https://github.com/googleapis/google-api-dotnet-client)
- [Npgsql](http://www.npgsql.org/)

## Настройка `appsettings.json`
1. Создаём приложение в Google API;  
*Самый быстрый способ: перейти на страницу официальной документации [.NET Quickstart](https://developers.google.com/sheets/api/quickstart/dotnet) (кнопка **"Enable the Google Sheets API"**).*
1. Записываем полученный `ClientId`, `ClientSecret`;
1. [Создаём таблицу, в которую приложение будет записывать статистику](https://docs.google.com/spreadsheets/); 
1. Записываем код таблицы в параметр `SpreadsheetId`;
1. Настраиваем подключения к серверам БД, промежуток времени обновления статистики.