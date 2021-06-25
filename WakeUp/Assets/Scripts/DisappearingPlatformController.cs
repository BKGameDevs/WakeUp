using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatformController : MonoBehaviour
{
    public float DisappearTime = 2f;
    public float AppearTime = 2f;
    public GameObject Platform;


    // Start is called before the first frame update
    void Start()
    {
        
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

            if (playerController.IsGrounded)
            {
                this.StartTimedAction(
                    null,
                    () =>
                    {
                        Platform.SetActive(false);
                        this.StartTimedAction(null, () => Platform.SetActive(true), AppearTime);
                    },
                    DisappearTime);
            }
        }
    }
}
