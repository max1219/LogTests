namespace LogTests.Operators.LogOperators;

public class Or : IOperator
{
    private readonly IOperator _left;
    private readonly IOperator _right;

    public Or(IOperator left, IOperator right)
    {
        _left = left;
        _right = right;
    }

    public string? Check()
    {
        string? resultLeft = _left.Check();
        if (resultLeft is null)
        {
            return null;
        }

        string? resultRight = _right.Check();
        if (resultRight is null)
        {
            return null;
        }

        return $"Fail Or({resultLeft}, {resultRight})";
    }

    public override string ToString()
    {
        return $"({_left}) || ({_right})";
    }
}