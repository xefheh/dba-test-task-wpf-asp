namespace Dba.TestTask.Backend.Domain.Enumerations;

public class Address
{
    public int Id { get; set; }
    public string HouseNumber { get; set; } = null!;
    public string FlatNumber { get; set; } = null!;
    public Street Street { get; set; } = null!;
}