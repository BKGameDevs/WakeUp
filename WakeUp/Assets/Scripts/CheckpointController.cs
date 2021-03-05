using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointController : MonoBehaviour
{
    public GameEvent CheckpointEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            CheckpointEvent?.Raise(transform.position);
            // var player = other.gameObject.GetComponent<PlayerController>();   
            // player.UpdateCheckpoint(transform.position); 
        }
    }
}
