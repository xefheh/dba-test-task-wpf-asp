namespace Dba.TestTask.Desktop.Wpf.Models;

public class UpdateAbonentModel
{
    public int Id { get; set; }
    public string Surname { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public AddressModel Address { get; set; } = null!;
    public string? HomePhone { get; set; }
    public string? WorkPhone { get; set; }
    public string? MobilePhone { get; set; }
}