using System;
using System.Collections.Generic;
using System.Reflection;
using LogTests.Attributes;
using LogTests.Attributes.TestAttributes;

namespace LogTests;

public class Runner
{
    public event EventHandler<OnFailEventArgs> OnFailTest;

    // todo написать doc про наличие конструктора по умолчанию
    // todo обновить uml добавив передачу чего то, за что дергать BET и AET в ITestAttribute
    public void TestClass(Type classType)
    {
        (IEnumerable<(TestAttribute, MethodBase)> tests,
            (IEnumerable<MethodBase> beforeAll,
            IEnumerable<MethodBase> afterAll,
            IEnumerable<MethodBase> beforeEach,
            IEnumerable<MethodBase> afterEach,
            IEnumerable<MethodBase> beforeEachTime,
            IEnumerable<MethodBase> afterEachTime) initMethods
            ) analyzerTuple = Analyzer.AnalyzeClass(classType);

        Initializer initializer = new Initializer(classType, analyzerTuple.initMethods);
        object instance = initializer.Instance;
        Tester tester = new Tester();

        initializer.UseBeforeAll();

        foreach ((TestAttribute, MethodBase) test in analyzerTuple.tests)
        {
            initializer.UseBeforeEach();
            string? result = test.Item1.Test(instance, test.Item2, tester, initializer.UseBeforeEachTime,
                initializer.UseAfterEachTime);
            initializer.UseAfterEach();
            tester.Clear();
            if (result is not null)
            {
                OnFailTest.Invoke(this, new OnFailEventArgs(result));
            }
        }

        initializer.UseAfterAll();
    }


    public void TestMethod(MethodBase method)
    {
        (TestAttribute testAttribute,
            (IEnumerable<MethodBase> beforeAll,
            IEnumerable<MethodBase> afterAll,
            IEnumerable<MethodBase> beforeEach,
            IEnumerable<MethodBase> afterEach,
            IEnumerable<MethodBase> beforeEachTime,
            IEnumerable<MethodBase> afterEachTime) initMethods
            ) analyzerTuple = Analyzer.AnalyzeTestMethod(method);

        Initializer initializer = new Initializer(method.DeclaringType!, analyzerTuple.initMethods);
        object instance = initializer.Instance;
        Tester tester = new Tester();

        initializer.UseBeforeAll();

        initializer.UseBeforeEach();
        string? result = analyzerTuple.testAttribute.Test(instance, method, tester,
            initializer.UseBeforeEachTime, initializer.UseAfterEachTime);
        initializer.UseAfterEach();
        tester.Clear();
        
        if (result is not null)
        {
            OnFailTest.Invoke(this, new OnFailEventArgs(result));
        }

        initializer.UseAfterAll();
    }
}