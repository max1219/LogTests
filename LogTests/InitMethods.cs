using System.Collections.Generic;
using System.Reflection;

namespace LogTests;

public class InitMethods(IEnumerable<MethodBase>? beforeAll, IEnumerable<MethodBase>? afterAll, IEnumerable<MethodBase>? beforeEach, IEnumerable<MethodBase>? afterEach, IEnumerable<MethodBase>? beforeEachTime, IEnumerable<MethodBase>? afterEachTime)
{
    public IEnumerable<MethodBase>? BeforeAll = beforeAll;
    public IEnumerable<MethodBase>? AfterAll = afterAll;
    public IEnumerable<MethodBase>? BeforeEach = beforeEach;
    public IEnumerable<MethodBase>? AfterEach = afterEach;
    public IEnumerable<MethodBase>? BeforeEachTime = beforeEachTime;
    public IEnumerable<MethodBase>? AfterEachTime = afterEachTime;
}