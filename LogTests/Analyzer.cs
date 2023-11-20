using System;
using System.Collections.Generic;
using System.Reflection;
using LogTests.Attributes;
using LogTests.Attributes.InitMethodAttributes;

namespace LogTests;

internal static class Analyzer
{
    // todo написать doc (добавив описание того, что у метода не может быть более одного тестового атрибута
    // и не может быть сразу тестового и д-и атрибута
    internal static (IEnumerable<(TestAttribute, MethodBase)> tests, InitMethods initMethods) AnalyzeClass(
        Type classname)
    {
        List<(TestAttribute, MethodBase)> tests = new();

        LinkedList<MethodBase> beforeAllMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> afterAllMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> beforeEachMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> afterEachMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> beforeEachTimeMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> afterEachTimeMethods = new LinkedList<MethodBase>();
        MethodInfo[] methods = classname.GetMethods();

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

        InitMethods initMethods = new InitMethods(beforeAllMethods, afterAllMethods, beforeEachMethods,
            afterEachMethods, beforeEachTimeMethods, afterEachTimeMethods);
        return (tests, initMethods);
    }

    internal static (TestAttribute, InitMethods) AnalyzeTestMethod(MethodBase method)
    {
        Type declaringClass = method.DeclaringType!;
        LinkedList<MethodBase> beforeAllMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> afterAllMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> beforeEachMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> afterEachMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> beforeEachTimeMethods = new LinkedList<MethodBase>();
        LinkedList<MethodBase> afterEachTimeMethods = new LinkedList<MethodBase>();


        IEnumerable<Attribute> attributes = method.GetCustomAttributes();
        TestAttribute? testAttribute = null;
        foreach (Attribute attribute in attributes)
        {
            if (attribute is TestAttribute newTestAttribute)
            {
                testAttribute = newTestAttribute;
            }
        }

        MethodInfo[] methods = declaringClass.GetMethods();

        foreach (MethodInfo classMethod in methods)
        {
            attributes = classMethod.GetCustomAttributes();
            foreach (Attribute attribute in attributes)
            {
                if (attribute is InitMethodAttribute initMethodAttribute)
                {
                    switch (initMethodAttribute)
                    {
                        case BeforeAll:
                            beforeAllMethods.AddLast(classMethod);
                            break;
                        case AfterAll:
                            afterAllMethods.AddLast(classMethod);
                            break;
                        case BeforeEach:
                            beforeEachMethods.AddLast(classMethod);
                            break;
                        case AfterEach:
                            afterEachMethods.AddLast(classMethod);
                            break;
                        case BeforeEachTime:
                            beforeEachTimeMethods.AddLast(classMethod);
                            break;
                        case AfterEachTime:
                            afterEachTimeMethods.AddLast(classMethod);
                            break;
                    }
                }
            }
        }

        InitMethods initMethods = new InitMethods(beforeAllMethods, afterAllMethods, beforeEachMethods,
            afterEachMethods, beforeEachTimeMethods, afterEachTimeMethods);

        if (testAttribute is not null)
        {
            return (testAttribute, initMethods);
        }

        throw new ArgumentException("Method hasn't got test attributes");
    }
}