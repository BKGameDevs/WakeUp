using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneController : MonoBehaviour
{
    public PlayerController Player;
    public GameEvent KillzoneEvent;

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            StartCoroutine(WaitAndReset());
        }
    }

    private IEnumerator WaitAndReset(){
        yield return new WaitForSeconds(0.25f);
        KillzoneEvent?.Raise();
        // Player?.ResetPlayer();
    }
}
