using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerController Player;

    internal bool KeyObtained { get; private set;}

    // Start is called before the first frame update

    public GameEvent OnLevelStart;
    void Start()
    {
        OnLevelStart?.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetKeyObtained()
    {
        KeyObtained = true;
    }
}
