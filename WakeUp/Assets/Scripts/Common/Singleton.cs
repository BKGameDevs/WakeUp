using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : Singleton where T : Component
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }

        OnAwake();
    }

    protected virtual void OnAwake() { }
}

public abstract class Singleton : MonoBehaviour
{

}
