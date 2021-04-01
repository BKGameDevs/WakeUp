using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static WakeUp.Constants.PlayerInput;
using System;

public class InteractController : MonoBehaviour
{
    public GameEvent OnInteract;
    //public GameObject Prompt;
    public TextMeshProUGUI Prompt;
    public BoolVariable OverlayOpen;
    private bool InArea;
    // Start is called before the first frame update
    void Start()
    {
        InArea = false;

    }

    // Update is called once per frame
    void Update()
    {
        var isOpen = OverlayOpen.RuntimeValue;

        if (InArea)
        {

            if (IsPressingInteract)
            {
                //if (GameManager.Instance.KeyObtained)
                //{
                //    //Unlock exit by triggering event that DoorController picks up
                //    //Once DoorController finishes trigger interact

                //    Prompt.text = "Press X to Enter next level";
                //}


                //else 
                if (!isOpen)
                    // Debug.Log("User Interact");
                    OnInteract?.Raise(true);


            }
            if (IsPressingEscape && isOpen)
            {
                // Debug.Log("User Interact");
                OnInteract?.Raise(false);

                if (GameManager.Instance.KeyObtained)
                {
                    Prompt.text = "Press X to Enter next section";
                }
            }

        }

        Prompt.enabled = !isOpen && InArea;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            InArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            InArea = false;
        }
    }

    public void SetPromptText(string text)
    {
        Prompt.text = text;
    }
}
