namespace LogTests.Attributes.InitMethodAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class AfterEach : Attribute, IInitMethodAttribute
{
}
