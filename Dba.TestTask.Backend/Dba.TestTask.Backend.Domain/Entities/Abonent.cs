using Dba.TestTask.Backend.Domain.Enumerations;
using Dba.TestTask.Backend.Domain.ValueTypes;

namespace Dba.TestTask.Backend.Domain.Entities;

public class Abonent
{
    public int Id { get; set; }
    public string Surname { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public Address Address { get; set; } = null!;
    public PhoneNumber? HomePhone { get; set; }
    public PhoneNumber? WorkPhone { get; set; }
    public PhoneNumber? MobilePhone { get; set; }
}