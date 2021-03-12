using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static WakeUp.Constants.PlayerInput;


public class InteractController : MonoBehaviour
{
    public GameObject Text;
    private bool InArea;
    // Start is called before the first frame update
    void Start()
    {
        InArea = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPressingInteract && InArea) {
            // Debug.Log("User Interact");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            Text.SetActive(true);
            InArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            Text.SetActive(false);
            InArea = false;
        }
    }
}
