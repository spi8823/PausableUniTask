using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

internal static class ObjectPool<T> where T : class, new()
{
    private static Stack<T> poolStack = new Stack<T>();
    public static T Pop()
    {
        if (poolStack.Count == 0)
            return new T();
        var obj = poolStack.Pop();
        if (obj is IPoolable<T> poolable)
            poolable.Reset();

        return obj;
    }

    public static void Push(T obj)
    {
        poolStack.Push(obj);
    }
}

internal interface IPoolable<T> : IDisposable where T : class, new()
{
    public static T Get()
    {
        return ObjectPool<T>.Pop();
    }

    void Reset();

    void IDisposable.Dispose()
    {
        Reset();
        ObjectPool<T>.Push(this as T);
    }
}
