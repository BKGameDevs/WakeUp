using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneController : MonoBehaviour
{
    public PlayerController Player;

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            StartCoroutine(WaitAndReset());
        }
    }

    private IEnumerator WaitAndReset(){
        yield return new WaitForSeconds(2);
        Player?.ResetPlayer();
    }
}
