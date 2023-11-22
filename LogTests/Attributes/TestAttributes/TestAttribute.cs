using System;
using System.Reflection;

namespace LogTests.Attributes.TestAttributes;

[AttributeUsage(AttributeTargets.Method)]
public abstract class TestAttribute : Attribute
{
    internal abstract string? Test(
        object instance,
        MethodBase method,
        Tester tester,
        Action useBeforeEachTime,
        Action useAfterEachTime);
}