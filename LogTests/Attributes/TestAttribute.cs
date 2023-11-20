using System;
using System.Reflection;

namespace LogTests.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal abstract class TestAttribute : Attribute
{
    internal abstract string? Test(object instance, MethodBase method, Tester tester);
}
