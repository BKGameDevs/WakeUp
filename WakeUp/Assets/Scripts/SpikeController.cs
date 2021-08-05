using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public float StartDelay = 1f;
    public float RepeatDelay = 1f;

    private Animator _Animator;
    private Collider2D _Collider;
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();

        this.StartTimedAction(null, 
            () => this.StartLoopingAction(
                () => _Animator.SetTrigger("Spike"),
                () => true,
                RepeatDelay),
            StartDelay);
    }

    public void UpdateCollider()
    {
        ClearCollider();
        _Collider = gameObject.AddComponent<PolygonCollider2D>();
        _Collider.isTrigger = true;
    }

    public void ClearCollider()
    {
        if (_Collider != null)
        {
            Destroy(_Collider);
            _Collider = null;
        }
    }
}
