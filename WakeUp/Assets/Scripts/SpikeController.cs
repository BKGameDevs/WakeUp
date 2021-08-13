using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO: Add AudioPlayerController to call AudioPlayer and send AudioPlayerSettings with Transform and other metadata
public class SpikeController : MonoBehaviour
{
    public float StartDelay = 1f;
    public float RepeatDelay = 1f;

    public UnityEvent SpikeUp;
    public UnityEvent SpikeDown;

    private bool _Up = true;

    private Animator _Animator;
    private Collider2D _Collider;
    // Start is called before the first frame update
    void Start()
    {
        SpikeUp?.Invoke();

        _Animator = GetComponent<Animator>();

        this.StartTimedAction(null, 
            () => this.StartLoopingAction(
                () => 
                {
                    _Up = !_Up;

                    if (_Up)
                        SpikeUp?.Invoke();
                    else
                        SpikeDown?.Invoke();

                    _Animator.SetTrigger("Spike");
                },
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
