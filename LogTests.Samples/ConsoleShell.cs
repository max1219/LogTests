
using LogTests;

void OnTestFail(OnFailEventArgs args)
{
    Console.WriteLine(args.Message);
}

Runner runner = new Runner();

//runner.TestClass(*SomeType*);