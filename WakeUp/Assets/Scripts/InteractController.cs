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
    public Animator Transition;
    private bool InArea;
    // Start is called before the first frame update
    void Start()
    {
        InArea = false;

    }

    // Update is called once per frame
    void Update()
    {
        var isOpen = Transition.GetBool("Open");

        if (InArea)
        {

            if (IsPressingInteract && !isOpen)
            {
                // Debug.Log("User Interact");
                Transition.SetBool("Open", true);
                OnInteract?.Raise(true);
            }
            if (IsPressingEscape && isOpen)
            {
                // Debug.Log("User Interact");
                Transition.SetBool("Open", false);
                OnInteract?.Raise(false);
            }

            if (IsPressingEnter && GameManager.Instance.KeyObtained)
                //TODO: Close window and play animation
                Prompt.text = "Press X to Enter next level";

        }

        Prompt.enabled = !isOpen && InArea;
        //Prompt.SetActive(!isOpen && InArea);
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
}
