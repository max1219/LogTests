using LogTests.Attributes.TestAttributes;
using LogTests.Operators;
using LogTests.Operators.LogOperators;
using LogTests.Operators.TestOperators;

namespace LogTests.Samples;

public class MathTests
{
    [Fact]
    public void TestAbs(Tester tester)
    {
        int i = -4;

        int actual = Math.Abs(i);

        IOperator @operator = new Or(
            new Equals(i, actual),
            new Equals(-i, actual));
        tester.Check(@operator);
    }

    [Fact]
    public void WrongTestAbs(Tester tester)
    {
        int i = -4;

        int actual = Math.Abs(i);

        IOperator @operator = new And(
            new Equals(i, actual),
            new Equals(-i, actual));
        tester.Check(@operator);
    }

    [Fact]
    public void HardTree(Tester tester)
    {
        IOperator @operator = new Or(
            new And(
                new False(false),
                new Not(
                    new False(true))),
            new Or(
                new Equals(4, 5),
                new Not(
                    new And(
                        new True(true),
                        new Equals(3, 3)))));
        tester.Check(@operator);
    }

    [Fact]
    public void WrongHardTree(Tester tester)
    {
        IOperator @operator = new Or(
            new And(
                new False(false),
                new Not(
                    new False(false))),
            new Or(
                new Equals(4, 5),
                new Not(
                    new And(
                        new True(true),
                        new Equals(3, 3)))));
        tester.Check(@operator);
    }

    [Fact]
    // 3 wrongs
    public void SomeWrongChecks(Tester tester)
    {
        IOperator @operator = new Or(
            new False(true),
            new Equals(5, 4));
        tester.Check(@operator);

        @operator = new Or(
            new False(false),
            new Equals(5, 4));
        tester.Check(@operator);

        @operator = new Or(
            new False(true),
            new Equals(4, 4));
        tester.Check(@operator);

        @operator = new And(
            new False(true),
            new Equals(4, 4));
        tester.Check(@operator);
        @operator = new Not(
            new Or(
                new False(true),
                new Equals(4, 4)));
        tester.Check(@operator);
    }
}