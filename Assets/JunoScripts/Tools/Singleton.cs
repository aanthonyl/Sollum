// Singleton.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
{
    private static T _instance;
    public static T instance
    {
        get
        {
            return _instance;
        }
        set { _instance = value; }
    }

    // The child must call SingletonBuilder() with a reference to itself.
    protected void SingletonBuilder(T newInstance)
    {
        if (_instance == null)
        {
            _instance = newInstance;
            DontDestroyOnLoad(this.gameObject);
        }
    }

}