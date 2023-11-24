namespace LogTests.Operators.TestOperators;

public class True : IOperator
{
    private readonly bool _condition;

    public True(bool condition)
    {
        _condition = condition;
    }

    public string? Check()
    {
        if (_condition)
            return null;
        return "[Fail True]";
    }

    public override string ToString()
    {
        return $"True({_condition})";
    }
}