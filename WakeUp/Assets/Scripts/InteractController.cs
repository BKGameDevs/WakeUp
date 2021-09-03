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

    public bool DisablePrompt;
    private bool InArea;
    private bool _IsInteracting;
    public bool IsInteracting
    {
        get { return _IsInteracting; }
        protected set
        {
            if (_IsInteracting != value)
            {
                _IsInteracting = value;
                OnInteractChanged();
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        InArea = false;
    }

    protected virtual bool GetStopInteractionTrigger()
    {
        return IsPressingEscape && InArea;
    }

    protected virtual bool GetStartInteractionTrigger()
    {
        return IsPressingInteract && InArea;
    }

    public virtual void StopInteraction()
    {
        IsInteracting = false;
    }

    protected virtual void StartInteraction()
    {
        IsInteracting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetStartInteractionTrigger())
            StartInteraction();
        else if (GetStopInteractionTrigger())
            StopInteraction();

        Prompt.enabled = !DisablePrompt && !IsInteracting && InArea;
    }

    protected virtual void OnInteractChanged()
    {
        OnInteract?.Raise(IsInteracting);
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
