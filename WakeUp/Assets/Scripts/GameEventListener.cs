using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent<object> Response;
    public GameEventResponse[] GameEventResponses;

    private void OnEnable() { 
        Event?.RegisterListener(this);

        foreach (var gameEventResponse in GameEventResponses)
            gameEventResponse.Event.RegisterListener(this);
    }

    private void OnDisable(){
        Event?.UnregisterListener(this);

        foreach (var gameEventResponse in GameEventResponses)
            gameEventResponse.Event.UnregisterListener(this);
    }

    public void OnEventRaised(object obj){
        Response?.Invoke(obj);

        
    }

    public void OnEventRaised(GameEvent gameEvent, object obj)
    {
        foreach (var gameEventResponse in GameEventResponses.Where(x => x.Event == gameEvent))
            gameEventResponse.Response.Invoke(obj);
    }
}

[Serializable]
public class GameEventResponse
{
    public GameEvent Event;
    public UnityEvent<object> Response;
}