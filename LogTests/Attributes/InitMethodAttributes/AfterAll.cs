namespace LogTests.Attributes.InitMethodAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class AfterAll : Attribute, IInitMethodAttribute
{
}
