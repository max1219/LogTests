using System.Reflection;

namespace LogTests.Attributes;

internal interface ITestAttribute
{
    internal string? Test(object instance, MethodBase method, Tester tester);
}
