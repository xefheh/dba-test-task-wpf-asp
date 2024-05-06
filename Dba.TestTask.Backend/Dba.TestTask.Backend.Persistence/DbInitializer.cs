using System.Data;
using Dapper;

namespace Dba.TestTask.Backend.Persistence;

public static class DbInitializer
{
    public static void InitializeDb(IDbConnection connection)
    {
        var createStreetTableQuery = @"
            CREATE TABLE IF NOT EXISTS Street (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT UNIQUE
            );
        ";

        var createAddressTableQuery = @"
            CREATE TABLE IF NOT EXISTS Address (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                HouseNumber TEXT,
                FlatNumber TEXT,
                StreetId INTEGER,
                FOREIGN KEY (StreetId) REFERENCES Street (Id)
            );
        ";

        var createPhoneNumberTableQuery = @"
            CREATE TABLE IF NOT EXISTS PhoneNumber (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Number TEXT,
                PhoneType INTEGER,
                AbonentId INTEGER,
                FOREIGN KEY (AbonentId) REFERENCES Abonent (Id)
            );
        ";

        var createAbonentTableQuery = @"
            CREATE TABLE IF NOT EXISTS Abonent (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Surname TEXT,
                Name TEXT,
                MiddleName TEXT,
                AddressId INTEGER,
                FOREIGN KEY (AddressId) REFERENCES Address (Id)
            );
        ";

        connection.Execute(createStreetTableQuery);
        connection.Execute(createAddressTableQuery);
        connection.Execute(createPhoneNumberTableQuery);
        connection.Execute(createAbonentTableQuery);
    }

    public static void CreateTestRows(IDbConnection connection)
    {
        connection.Execute("INSERT INTO Street (Name) VALUES " +
                           "('Хорошая')," +
                           "('Прекрасная')," +
                           "('Отличная')");

        connection.Execute("INSERT INTO Address (HouseNumber, FlatNumber, StreetId) VALUES" +
                           "(1, 2, 1)," +
                           "(2, 2, 1)," +
                           "(2, '1А', 2)," +
                           "(3, 3, 3)");

        connection.Execute("INSERT INTO Abonent (Surname, Name, MiddleName, AddressId) VALUES " +
                           "('Серов', 'Александр', 'Владимирович', 1)," +
                           "('Никитин','Никита','Никитьевич', 2)," +
                           "('Александров', 'Александр', 'Александрович', 3)," +
                           "('Митсубиши', 'Паджеро', 'Яматович', 4)");

        connection.Execute("INSERT INTO PhoneNumber (Number, PhoneType, AbonentId) VALUES " +
                           "('+1 (111) 111 11-11', 0, 1)," +
                           "('+2 (111) 111 11-11', 1, 2)," +
                           "('+3 (111) 111 11-11', 2, 3)," +
                           "('+4 (111) 111 11-11', 0, 4)");
    }
}