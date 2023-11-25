using LogTests;
using LogTests.Samples;

void OnTestFail(object? invoker, OnFailEventArgs args)
{
    Console.WriteLine(args.Message);
}

Runner runner = new Runner();
runner.OnFailTest += OnTestFail;

Type type = typeof(MathTests);
runner.TestClass(type);

Type t = typeof(ReusableAttributeTests);
runner.TestClass(t);