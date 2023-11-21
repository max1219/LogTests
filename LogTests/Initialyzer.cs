﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace LogTests;

internal class Initialyzer
{
    private readonly (IEnumerable<MethodBase> beforeAll, 
                      IEnumerable<MethodBase> afterAll, 
                      IEnumerable<MethodBase> beforeEach, 
                      IEnumerable<MethodBase> afterEach, 
                      IEnumerable<MethodBase> beforeEachTime, 
                      IEnumerable<MethodBase> afterEachTime) _initMethods;
    internal object TestClass { get; }

    internal Initialyzer(Type t, 
                         (IEnumerable<MethodBase> beforeAll,
                          IEnumerable<MethodBase> afterAll,
                          IEnumerable<MethodBase> beforeEach,
                          IEnumerable<MethodBase> afterEach,
                          IEnumerable<MethodBase> beforeEachTime,
                          IEnumerable<MethodBase> afterEachTime) initMethods)
    {
        TestClass = Activator.CreateInstance(t)!;

        _initMethods.beforeAll = initMethods.beforeAll;
        _initMethods.afterAll = initMethods.afterAll;
        _initMethods.beforeEach = initMethods.beforeEach;
        _initMethods.afterEach = initMethods.afterEach;
        _initMethods.beforeEachTime = initMethods.beforeEachTime;
        _initMethods.afterEachTime = initMethods.afterEachTime;
    }

    internal void UseBeforeAll()
    {
        foreach (MethodBase method in _initMethods.beforeAll)
        {
            method.Invoke(TestClass, parameters: null);
        }
    }

    internal void UseAfterAll()
    {
        foreach (MethodBase method in _initMethods.afterAll)
        {
            method.Invoke(TestClass, parameters: null);
        }
    }

    internal void UseBeforeEach()
    {
        foreach (MethodBase method in _initMethods.beforeEach)
        {
            method.Invoke(TestClass, parameters: null);
        }
    }

    internal void UseAfterEach()
    {
        foreach (MethodBase method in _initMethods.afterEach)
        {
            method.Invoke(TestClass, parameters: null);
        }
    }

    internal void UseBeforeEachTime()
    {
        foreach (MethodBase method in _initMethods.beforeEachTime)
        {
            method.Invoke(TestClass, parameters: null);
        }
    }

    internal void UseAfterEachTime()
    {
        foreach (MethodBase method in _initMethods.afterEachTime)
        {
            method.Invoke(TestClass, parameters: null);
        }
    }
}
