using System.Reflection;

namespace Dba.TestTask.Backend.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(MemberInfo t, object key) : base($"{t.Name} with key {key} not found!") { }
}