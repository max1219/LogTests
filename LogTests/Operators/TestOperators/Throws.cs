namespace LogTests.Operators.TestOperators;

public class Throws<T> : IOperator where T : Exception
{
    private readonly Action _action;

    public Throws(Action action)
    {
        _action = action;
    }
    
    public string? Check()
    {
        try
        {
            _action.Invoke();
        }
        catch (T)
        {
            return null;
        }
        catch (Exception e)
        {
            return $"Fail Throws: expected {typeof(T)}, actual {e.GetType()}";
        }

        return $"Fail Throws({typeof(T)})";
    }

    public override string ToString()
    {
        return $"Throws({typeof(T)})";
    }
}