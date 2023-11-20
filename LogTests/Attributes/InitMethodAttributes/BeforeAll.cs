namespace LogTests.Attributes.InitMethodAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class BeforeAll : Attribute, IInitMethodAttribute
{
}
