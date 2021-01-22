using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public PlayerController Player;
    public SanityBar SanityBar;


    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (Player != null) 
            Player.SanityReduced += (player, currentSanity) => {
                if (SanityBar != null)
                    SanityBar.SetSanity(currentSanity);
            };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
