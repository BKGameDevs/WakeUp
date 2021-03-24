using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerController Player;
    public SanityBar SanityBar;

    internal bool KeyObtained { get; private set;}
    // private Vector3 _SoftCheckpoint;
    // private Vector3 _HardCheckpoint;

    // Start is called before the first frame update

    public GameEvent OnLevelStart;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        OnLevelStart?.Raise();
        // if (Player != null) 
        //     Player.SanityReduced += (player, currentSanity) => {
        //         if (SanityBar != null)
        //             SanityBar.SetSanity(currentSanity);
        //     };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void UpdateHardCheckpoint(object newPos) {
    //     // needs to be Instance._SoftCheckpoint because this is a singleton and only has one instance
    //     _HardCheckpoint = (Vector3) newPos;
    //     Debug.Log((Vector3) newPos);
    // }

    // public void UpdateSoftCheckpoint(object newPos) {
    //     // needs to be Instance._SoftCheckpoint because this is a singleton and only has one instance
    //     Instance._SoftCheckpoint = (Vector3) newPos;
    // }

    // public void ResetPlayer(){
    //     Player.ResetPlayer(_SoftCheckpoint);
    // }

    public void SetKeyObtained()
    {
        KeyObtained = true;
    }
}
