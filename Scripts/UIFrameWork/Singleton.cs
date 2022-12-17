using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 泛型单利
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton <T> where T : class, new()
{
    private static T ins;
    public static T Instance
    {
        get
        {
            if (ins == null)
            {
                ins = new T();
            }
            return ins;
        }
    }
}
