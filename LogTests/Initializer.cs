using System;
using System.Collections.Generic;
using System.Reflection;

namespace LogTests;

internal class Initializer
{
    private readonly (IEnumerable<MethodBase> beforeAll,
        IEnumerable<MethodBase> afterAll,
        IEnumerable<MethodBase> beforeEach,
        IEnumerable<MethodBase> afterEach,
        IEnumerable<MethodBase> beforeEachTime,
        IEnumerable<MethodBase> afterEachTime) _initMethods;

    internal object Instance { get; }

    internal Initializer(Type t,
        (IEnumerable<MethodBase> beforeAll,
            IEnumerable<MethodBase> afterAll,
            IEnumerable<MethodBase> beforeEach,
            IEnumerable<MethodBase> afterEach,
            IEnumerable<MethodBase> beforeEachTime,
            IEnumerable<MethodBase> afterEachTime) initMethods)
    {
        Instance = Activator.CreateInstance(t)!;
        _initMethods = initMethods;
    }

    internal void UseBeforeAll()
    {
        foreach (MethodBase method in _initMethods.beforeAll)
        {
            method.Invoke(Instance, parameters: null);
        }
    }

    internal void UseAfterAll()
    {
        foreach (MethodBase method in _initMethods.afterAll)
        {
            method.Invoke(Instance, parameters: null);
        }
    }

    internal void UseBeforeEach()
    {
        foreach (MethodBase method in _initMethods.beforeEach)
        {
            method.Invoke(Instance, parameters: null);
        }
    }

    internal void UseAfterEach()
    {
        foreach (MethodBase method in _initMethods.afterEach)
        {
            method.Invoke(Instance, parameters: null);
        }
    }

    internal void UseBeforeEachTime()
    {
        foreach (MethodBase method in _initMethods.beforeEachTime)
        {
            method.Invoke(Instance, parameters: null);
        }
    }

    internal void UseAfterEachTime()
    {
        foreach (MethodBase method in _initMethods.afterEachTime)
        {
            method.Invoke(Instance, parameters: null);
        }
    }
}