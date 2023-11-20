namespace LogTests.Attributes.InitMethodAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class AfterEachTime : Attribute, IInitMethodAttribute
{
}
