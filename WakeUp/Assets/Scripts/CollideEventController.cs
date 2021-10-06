using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollideEventController : MonoBehaviour
{
    public UnityEvent PlayerCollided;
    public bool FireOnce;

    private bool _Fired;

    public void ResetFire() => _Fired = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (FireOnce && _Fired)
            return;

        if (other.CompareTag("Player"))
        {
            PlayerCollided?.Invoke();
            _Fired = true;
        }
    }
}
