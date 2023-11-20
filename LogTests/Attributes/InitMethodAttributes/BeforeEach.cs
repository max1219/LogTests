namespace LogTests.Attributes.InitMethodAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class BeforeEach : Attribute, IInitMethodAttribute
{
}
