using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisappearingPlatformController : MonoBehaviour
{
    public float DisappearTime = 2f;
    public float AppearTime = 2f;
    public GameObject Platform;

    public UnityEvent Disappearing;
    public UnityEvent Disappeared;

    private MovingObjectController _MovingObjectController;
    // Start is called before the first frame update
    void Start()
    {
        _MovingObjectController = GetComponentInChildren<MovingObjectController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController.IsGrounded && playerController.CurrentGround == gameObject)
            {
                this.StartTimedAction(
                    () =>
                    {
                        _MovingObjectController.SetIsActive(true);
                        Disappearing?.Invoke();
                    },
                    () =>
                    {
                        Disappeared?.Invoke();
                        _MovingObjectController.SetIsActive(false);
                        Platform.SetActive(false);
                        this.StartTimedAction(null, () => Platform.SetActive(true), AppearTime);
                    },
                    DisappearTime);
            }
        }
    }
}
