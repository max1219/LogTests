using System;
using System.Collections.Generic;
using System.Reflection;
using LogTests.Attributes.TestAttributes;
using LogTests.Attributes.InitMethodAttributes;

namespace LogTests;

internal static class Analyzer
{
    internal static (IEnumerable<(TestAttribute, MethodBase)> tests,
        (IEnumerable<MethodBase> beforeAll,
        IEnumerable<MethodBase> afterAll,
        IEnumerable<MethodBase> beforeEach,
        IEnumerable<MethodBase> afterEach,
        IEnumerable<MethodBase> beforeEachTime,
        IEnumerable<MethodBase> afterEachTime) initMethods)
        AnalyzeClass(Type classType)
    {
        List<(TestAttribute, MethodBase)> tests = new();

        var initMethods = GetInitMethods(classType);

        MethodInfo[] methods = classType.GetMethods();

        foreach (MethodInfo method in methods)
        {
            IEnumerable<Attribute> attributes = method.GetCustomAttributes();
            foreach (Attribute attribute in attributes)
            {
                if (attribute is TestAttribute testAttribute)
                {
                    tests.Add((testAttribute, method));
                    break;
                }
            }
        }

        return (tests, initMethods);
    }

    internal static (TestAttribute,
        (IEnumerable<MethodBase> beforeAll,
        IEnumerable<MethodBase> afterAll,
        IEnumerable<MethodBase> beforeEach,
        IEnumerable<MethodBase> afterEach,
        IEnumerable<MethodBase> beforeEachTime,
        IEnumerable<MethodBase> afterEachTime))
        AnalyzeTestMethod(MethodBase method)
    {
        IEnumerable<Attribute> attributes = method.GetCustomAttributes();

        TestAttribute? testAttribute = null;

        foreach (Attribute attribute in attributes)
        {
            if (attribute is TestAttribute newTestAttribute)
            {
                testAttribute = newTestAttribute;
                break;
            }
        }

        if (testAttribute is null)
        {
            throw new ArgumentException("Method hasn't got test attributes");
        }

        Type declaringClass = method.DeclaringType!;

        var initMethods = GetInitMethods(declaringClass);

        return (testAttribute, initMethods);
    }


    private static (IEnumerable<MethodBase> beforeAll,
        IEnumerable<MethodBase> afterAll,
        IEnumerable<MethodBase> beforeEach,
        IEnumerable<MethodBase> afterEach,
        IEnumerable<MethodBase> beforeEachTime,
        IEnumerable<MethodBase> afterEachTime)
        GetInitMethods(Type classType)
    {
        LinkedList<MethodBase> beforeAllMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> afterAllMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> beforeEachMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> afterEachMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> beforeEachTimeMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> afterEachTimeMethods = new LinkedList<MethodBase>();

        MethodInfo[] methods = classType.GetMethods();

        foreach (MethodInfo method in methods)
        {
            IEnumerable<Attribute> attributes = method.GetCustomAttributes();

            foreach (Attribute attribute in attributes)
            {
                if (attribute is InitMethodAttribute initMethodAttribute)
                {
                    switch (initMethodAttribute)
                    {
                        case BeforeAll:
                            beforeAllMethods.AddLast(method);
                            break;
                        case AfterAll:
                            afterAllMethods.AddLast(method);
                            break;
                        case BeforeEach:
                            beforeEachMethods.AddLast(method);
                            break;
                        case AfterEach:
                            afterEachMethods.AddLast(method);
                            break;
                        case BeforeEachTime:
                            beforeEachTimeMethods.AddLast(method);
                            break;
                        case AfterEachTime:
                            afterEachTimeMethods.AddLast(method);
                            break;
                    }
                }
            }
        }

        return (beforeAllMethods, afterAllMethods, beforeEachMethods, afterEachMethods, beforeEachTimeMethods,
            afterEachTimeMethods);
    }
}