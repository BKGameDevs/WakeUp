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
        SectionManager.PlayerSpawned += SectionManager_PlayerSpawned;
    }

    private void SectionManager_PlayerSpawned(object sender, System.EventArgs e)
    {
        KeyObtained = false;
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
