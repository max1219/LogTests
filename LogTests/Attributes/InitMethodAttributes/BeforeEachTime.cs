namespace LogTests.Attributes.InitMethodAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class BeforeEachTime : Attribute, IInitMethodAttribute
{
}
