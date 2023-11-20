using System.Reflection;
using LogTests.Attributes;
using LogTests.Attributes.InitMethodAttributes;

namespace LogTests;


internal static class Analyzer
{
    internal static (IEnumerable<(ITestAttribute, MethodBase)>, InitMethods) AnalyzeClass(Type classname)
    {
        var collection = new List<(ITestAttribute, MethodBase)>();

        InitMethods initMethods = GetInitMethods(classname);

        var methodsClass = classname.GetMethods();

        foreach(var method in methodsClass)
        {
            var attributes = method.CustomAttributes;

            foreach(var attribute in attributes)
            {
                var interfaces = attribute.AttributeType.GetInterfaces();

                foreach(var interfacesType in interfaces)
                {
                    if(interfacesType.Name.Equals(nameof(ITestAttribute)))
                    {
                        collection.Add(((ITestAttribute)attribute, method)!);
                    }
                }
            }
        }

        return (collection, initMethods);
    }

    internal static (ITestAttribute, InitMethods) AnalyzeTestMethod(MethodBase method)
    {
        var clazz = method.DeclaringType!;

        InitMethods initMethods = GetInitMethods(clazz);

        var attributes = method.CustomAttributes;

        foreach(var attribute in attributes)
        {
            bool isTestAttribute = false;

            var interfaces = attribute.AttributeType.GetInterfaces();

            foreach(var i in interfaces)
            {
                if(i.Name.Equals(typeof(ITestAttribute).Name))
                {
                    isTestAttribute = true;
                }
            }

            if(isTestAttribute)
            {
                return ((ITestAttribute)attribute, initMethods);
            }
        }

        throw new ArgumentException("Method hasn't got test attributes");
    }

    private static InitMethods GetInitMethods(Type classname)
    {
        var beforeAll = new List<MethodBase>();
        var afterAll = new List<MethodBase>();
        var beforeEach = new List<MethodBase>();
        var afterEach = new List<MethodBase>();
        var beforeEachTime = new List<MethodBase>();
        var afterEachTime = new List<MethodBase>();

        var methodsClass = classname.GetMethods();

        foreach(var method in methodsClass)
        {
            var attributes = method.CustomAttributes;

            foreach(var at in attributes)
            {
                switch (at.AttributeType.Name)
                {
                    case nameof(BeforeAll):
                        beforeAll.Add(method);
                        break;

                    case nameof(AfterAll):
                        afterAll.Add(method);
                        break;

                    case nameof(BeforeEach):
                        beforeEach.Add(method);
                        break;

                    case nameof(AfterEach):
                        afterEach.Add(method);
                        break;

                    case nameof(BeforeEachTime):
                        beforeEachTime.Add(method);
                        break;

                    case nameof(AfterEachTime):
                        afterEachTime.Add(method);
                        break;
                }
            }
        }

        return new InitMethods(beforeAll, afterAll, beforeEach, afterEach, beforeEachTime, afterEachTime);
    }
}
