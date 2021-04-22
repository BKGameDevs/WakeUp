using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneController : MonoBehaviour
{
    public PlayerController Player;
    public GameEvent KillzoneEvent;
    public GameEvent FadeoutEvent;

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            StartCoroutine(WaitAndReset());
        }
    }

    private IEnumerator WaitAndReset()
    {
        FadeoutEvent?.Raise(1.5f);
        yield return new WaitForSeconds(1f);
        KillzoneEvent?.Raise();

        // Player?.ResetPlayer();
    }
}
