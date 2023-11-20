using System;

namespace LogTests.Attributes.InitMethodAttributes;

[AttributeUsage(AttributeTargets.Method)]
public abstract class InitMethodAttribute : Attribute
{
}