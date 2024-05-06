using Dba.TestTask.Backend.Domain.Enumerations;

namespace Dba.TestTask.Backend.Domain.ValueTypes;

public class PhoneNumber
{
    public int Id { get; set; }
    public string Number { get; set; } = null!;
    public PhoneType Type { get; set; }
}