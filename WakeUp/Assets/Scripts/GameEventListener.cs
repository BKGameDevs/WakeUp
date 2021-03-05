using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
     public GameEvent Event;
     public UnityEvent<object> Response;

     private void OnEnable() { 
          Event.RegisterListener(this); 
     }

     private void OnDisable(){
          Event.UnregisterListener(this); 
     }

     public void OnEventRaised(object obj){
          Response.Invoke(obj);
     }
}
