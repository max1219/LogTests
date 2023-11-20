namespace LogTests.Operators.TestOperators;

public class Equals : IOperator
{
    private readonly object _expected;
    private readonly object? _actual;

    public Equals(object expected, object? actual)
    {
        _expected = expected;
        _actual = actual;
    }

    public string? Check()
    {
        if (_expected.Equals(_actual))
            return null;
        return $"Fail Equals: expected {_expected}, actual {_actual}";
    }

    public override string ToString()
    {
        return $"Equals(expected {_expected}, actual {_actual})";
    }
}