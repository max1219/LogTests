using System;
using System.Linq;
using System.Reflection;

namespace LogTests.Attributes.TestAttributes;

public class FactAttribute : TestAttribute
{
    internal override string? Test(object instance, MethodBase method, Tester tester, Action useBeforeEachTime,
        Action useAfterEachTime)
    {
        useBeforeEachTime.Invoke();
        method.Invoke(instance, new object?[] { tester });
        useAfterEachTime.Invoke();
        if (tester.IsAllSuccess())
        {
            return null;
        }

        string result = "Fail Fact: " + String.Concat(tester.GetWrong().Select(tuple => tuple.result), ", ");
        return result;
    }
}