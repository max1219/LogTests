using LogTests.Attributes.InitMethodAttributes;
using LogTests.Attributes.TestAttributes;
using LogTests.Operators;
using LogTests.Operators.LogOperators;
using LogTests.Operators.TestOperators;

namespace LogTests.Samples;

public class ReusableAttributeTests
{
    private Random random = new Random();
    private int number1;
    private int number2;

    [BeforeEachTime]
    public void InitMethod()
    {
        number1 = random.Next(2);
        number2 = random.Next(2);
    }

    [ReusableFact(100, 30)]
    public void TestOneSuccess(Tester tester)
    {
        IOperator @operator = new Equals(number1, 1);
        tester.Check(@operator);
    }

    [ReusableFact(100, 70)]
    public void TestOneNotSuccess(Tester tester)
    {
        IOperator @operator = new Equals(number1, 1);
        tester.Check(@operator);
    }

    [ReusableFact(100, 70)]
    public void TestTwoNotSuccess(Tester tester)
    {
        IOperator @operator = new Or(
            new And(
                new Equals(number1, 0),
                new Equals(number2, 0)),
            new And(
                new Equals(number1, 1),
                new Equals(number2, 1)));
        tester.Check(@operator);
    }

}
