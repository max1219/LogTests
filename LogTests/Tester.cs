using System.Collections.Generic;
using System.Linq;
using LogTests.Operators;

namespace LogTests;

public class Tester
{
    private readonly LinkedList<(string? result, IOperator @operator)> _results;

    internal Tester()
    {
        _results = new LinkedList<(string? result, IOperator @operator)>();
    }

    internal bool IsAllSuccess()
    {
        return _results.All(tuple => tuple.result is null);
    }

    internal IEnumerable<(string result, IOperator @operator)> GetWrong()
    {
        return _results.Where(tuple => tuple.result is not null)!;
    }

    public void Check(IOperator @operator)
    {
        string? result = @operator.Check();
        _results.AddLast((result, @operator));
    }

    internal void Clear()
    {
        _results.Clear();
    }
}