using Dba.TestTask.Backend.Application.Informations;

namespace Dba.TestTask.Backend.Application.Requests.Abonents;

public class UpdateAbonentRequest
{
    public int Id { get; set; }
    public string Surname { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public AddressInformation Address { get; set; } = null!;
    public string? HomePhone { get; set; }
    public string? WorkPhone { get; set; }
    public string? MobilePhone { get; set; }
}