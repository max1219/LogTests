using System;

namespace LogTests.Attributes.InitMethodAttributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public abstract class InitMethodAttribute : Attribute
{ 
}