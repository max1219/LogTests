using System.Reflection;

namespace LogTests;

public class InitMethods
{
    public IEnumerable<MethodBase>? BeforeAll;
    public IEnumerable<MethodBase>? AfterAll;
    public IEnumerable<MethodBase>? BeforeEach;
    public IEnumerable<MethodBase>? AfterEach;
    public IEnumerable<MethodBase>? BeforeEachTime;
    public IEnumerable<MethodBase>? AfterEachTime;

    public InitMethods(IEnumerable<MethodBase>? beforeAll, IEnumerable<MethodBase>? afterAll, IEnumerable<MethodBase>? beforeEach, IEnumerable<MethodBase>? afterEach, IEnumerable<MethodBase>? beforeEachTime, IEnumerable<MethodBase>? afterEachTime)
    {
        BeforeAll = beforeAll;
        AfterAll = afterAll;
        BeforeEach = beforeEach;
        AfterEach = afterEach;
        BeforeEachTime = beforeEachTime;
        AfterEachTime = afterEachTime;
    }
}