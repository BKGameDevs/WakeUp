using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillZoneController : MonoBehaviour
{
    //public PlayerController Player;
    public GameEvent KillzoneEvent;
    public UnityEvent OnKillzoneEntered;
    public float Damage = 25f;
    //public GameEvent FadeoutEvent;

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            KillzoneEvent?.Raise(Damage);
            OnKillzoneEntered?.Invoke();
        }
    }

    //private IEnumerator WaitAndReset()
    //{
    //    FadeoutEvent?.Raise(1.5f);
    //    yield return new WaitForSeconds(1f);
    //    KillzoneEvent?.Raise();

    //    // Player?.ResetPlayer();
    //}
}
