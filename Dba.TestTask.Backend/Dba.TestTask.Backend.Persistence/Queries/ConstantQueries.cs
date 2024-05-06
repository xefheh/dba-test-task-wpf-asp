namespace Dba.TestTask.Backend.Persistence.Queries;

public static class ConstantQueries
{
    public const string GetStreetByName = @"SELECT Id FROM Street WHERE Name = @Name;";

    public const string InsertStreet = @"INSERT INTO Street (Name) VALUES (@Name); SELECT last_insert_rowid();";
    
    public const string InsertAddress = @"INSERT INTO Address (HouseNumber, FlatNumber, StreetId) VALUES (@HouseNumber, @FlatNumber, @StreetId); SELECT last_insert_rowid();";

    public const string InsertAbonent =
        @"INSERT INTO Abonent(Surname, Name, MiddleName, AddressId) VALUES (@Surname, @Name, @MiddleName, @AddressId); SELECT last_insert_rowid();";

    public const string InsertPhoneNumber =
        @"INSERT INTO PhoneNumber (Number, PhoneType, AbonentId) VALUES (@Number, @PhoneType, @AbonentId);";

    public const string RemoveAbonent = @"DELETE FROM Abonent WHERE Id = @Id;";

    public const string RemovePhonesByAbonentId = @"DELETE FROM PhoneNumber WHERE AbonentId = @AbonentId;";
    
    public const string RemoveConcretePhoneByAbonentId = @"DELETE FROM PhoneNumber WHERE AbonentId = @AbonentId AND PhoneType = @PhoneType;";

    public const string UpdateAddress = @"UPDATE Address 
                                            SET
                                            StreetId = @StreetId,
                                            HouseNumber = @HouseNumber,
                                            FlatNumber = @FlatNumber
                                            WHERE
                                            Id = @Id;";

    public const string UpdateAbonent = @"UPDATE Abonent 
                                            SET
                                            Surname = @Surname,
                                            MiddleName = @MiddleName,
                                            Name = @Name
                                            WHERE
                                            Id = @Id;";

    public const string GetAbonentsByPhoneNumber =
        "SELECT a.Id, a.Surname, a.Name, a.MiddleName, a.AddressId as Id, addr.HouseNumber, addr.FlatNumber, st.Id as Id, " +
        "st.Name AS Name, pn.Id as Id, " +
        "pn.Number as Number, pn1.Id as Id, pn1.Number as Number, pn2.Id as Id, pn2.Number as Number " +
        "FROM Abonent a " +
        "LEFT JOIN Address addr ON a.AddressId = addr.Id " +
        "LEFT JOIN Street st ON addr.StreetId = st.Id " +
        "LEFT JOIN PhoneNumber pn ON a.Id = pn.AbonentId AND pn.PhoneType = 0 " +
        "LEFT JOIN PhoneNumber pn1 ON a.Id = pn1.AbonentId AND pn1.PhoneType = 1 " +
        "LEFT JOIN PhoneNumber pn2 ON a.Id = pn2.AbonentId AND pn2.PhoneType = 2 " +
        "WHERE pn.Number LIKE @Number OR " +
        "      pn1.Number LIKE @Number OR " +
        "      pn2.Number LIKE @Number;";

    public const string GetAbonents =
        "SELECT a.Id, a.Surname, a.Name, a.MiddleName, a.AddressId as Id, addr.HouseNumber, addr.FlatNumber, st.Id as Id, " +
        "st.Name AS Name, pn.Id as Id, " +
        "pn.Number as Number, pn1.Id as Id, pn1.Number as Number, pn2.Id as Id, pn2.Number as Number " +
        "FROM Abonent a " +
        "LEFT JOIN Address addr ON a.AddressId = addr.Id " +
        "LEFT JOIN Street st ON addr.StreetId = st.Id " +
        "LEFT JOIN PhoneNumber pn ON a.Id = pn.AbonentId AND pn.PhoneType = 0 " +
        "LEFT JOIN PhoneNumber pn1 ON a.Id = pn1.AbonentId AND pn1.PhoneType = 1 " +
        "LEFT JOIN PhoneNumber pn2 ON a.Id = pn2.AbonentId AND pn2.PhoneType = 2;";
    
    public const string GetAbonentsById =
        "SELECT a.Id, a.Surname, a.Name, a.MiddleName, a.AddressId as Id, addr.HouseNumber, addr.FlatNumber, st.Id as Id, " +
        "st.Name AS Name, pn.Id as Id, " +
        "pn.Number as Number, pn1.Id as Id, pn1.Number as Number, pn2.Id as Id, pn2.Number as Number " +
        "FROM Abonent a " +
        "LEFT JOIN Address addr ON a.AddressId = addr.Id " +
        "LEFT JOIN Street st ON addr.StreetId = st.Id " +
        "LEFT JOIN PhoneNumber pn ON a.Id = pn.AbonentId AND pn.PhoneType = 0 " +
        "LEFT JOIN PhoneNumber pn1 ON a.Id = pn1.AbonentId AND pn1.PhoneType = 1 " +
        "LEFT JOIN PhoneNumber pn2 ON a.Id = pn2.AbonentId AND pn2.PhoneType = 2 " +
        "WHERE a.Id = @Id;";
}