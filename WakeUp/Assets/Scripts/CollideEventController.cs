using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideEventController : MonoBehaviour
{
    public GameEvent GameEvent;
    public bool FireOnce;

    private bool _Fired;

    public void Reset() => _Fired = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (FireOnce && _Fired)
            return;

        if (other.CompareTag("Player"))
        {
            GameEvent?.Raise();
            _Fired = true;
        }
    }
}
