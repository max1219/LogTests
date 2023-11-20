namespace LogTests.Operators.LogOperators;

public class Not : IOperator
{
    private readonly IOperator _operator;

    public Not(IOperator @operator)
    {
        _operator = @operator;
    }


    public string? Check()
    {
        string? result = _operator.Check();
        if (result is not null)
        {
            return null;
        }
        return "Fail Not";
    }

    public override string ToString()
    {
        return $"!({_operator})";
    }
}