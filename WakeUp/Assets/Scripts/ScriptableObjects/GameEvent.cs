using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
	private List<GameEventListener> listeners = new List<GameEventListener>();

     public void Raise(object obj = null){
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(obj);
            listeners[i].OnEventRaised(this, obj);
        }
     }

     public void RegisterListener(GameEventListener listener){
          listeners.Add(listener); 
     }

     public void UnregisterListener(GameEventListener listener){
          listeners.Remove(listener); 
     }
}