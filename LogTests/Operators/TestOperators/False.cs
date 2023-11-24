namespace LogTests.Operators.TestOperators;

public class False : IOperator
{
    private readonly bool _condition;

    public False(bool condition)
    {
        _condition = condition;
    }

    public string? Check()
    {
        if (!_condition)
            return null;
        return "[Fail False]";
    }

    public override string ToString()
    {
        return $"False({_condition})";
    }
}