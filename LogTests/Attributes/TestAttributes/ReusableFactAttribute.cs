using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LogTests.Operators;

namespace LogTests.Attributes.TestAttributes;

public class ReusableFactAttribute : TestAttribute
{
    private readonly int _runsCount;
    private readonly int _requiredSuccessCount;

    public ReusableFactAttribute(int runsCount, int requiredSuccessCount)
    {
        _runsCount = runsCount;
        _requiredSuccessCount = requiredSuccessCount;
    }


    internal override string? Test(object instance, MethodBase method, Tester tester, Action useBeforeEachTime,
        Action useAfterEachTime)
    {
        for (int i = 0; i < _runsCount; i++)
        {
            useBeforeEachTime.Invoke();
            method.Invoke(instance, new object?[] { tester });
            useAfterEachTime.Invoke();
        }

        List<(string? result, IOperator @operator)> wrongs = tester.GetWrong().ToList()!;
        int successCount = _runsCount - wrongs.Count;
        if (successCount >= _requiredSuccessCount)
        {
            return null;
        }

        // Наиболее частая ошибка
        IGrouping<string, string> mostCommonResult = wrongs
            .Select(tuple => tuple.result!)
            .GroupBy(i => i)
            .OrderByDescending(grp => grp.Count())
            .First();

        string result =
            $"Fail ReusableFact: required {_requiredSuccessCount}, " +
            $"actual {successCount} success count, " +
            $"most common fail {mostCommonResult.Key} ({mostCommonResult.Count()} times)";
        return result;
    }
}