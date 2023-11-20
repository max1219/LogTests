namespace LogTests.Operators.LogOperators;

public class And : IOperator
{
    private readonly IOperator _left;
    private readonly IOperator _right;

    public And(IOperator left, IOperator right)
    {
        _left = left;
        _right = right;
    }

    public string? Check()
    {
        string? result = _left.Check();
        if (result is not null)
        {
            return $"Fail And({result}, ~)";
        }

        result = _right.Check();
        if (result is not null)
        {
            return $"Fail And(~, {result})";
        }

        return null;
    }

    public override string ToString()
    {
        return $"({_left}) && ({_right})";
    }
}