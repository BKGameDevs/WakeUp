using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonInjector : MonoBehaviour
{
    public GameObject[] SingletonPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var singletonPrefab in SingletonPrefabs)
        {
            if (singletonPrefab.TryGetComponent(out Singleton singleton) && 
                !FindObjectOfType(singleton.GetType()))
                Instantiate(singletonPrefab);
        }
    }
}
