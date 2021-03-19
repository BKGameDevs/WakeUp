using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractOverlayController : MonoBehaviour
{
    public TextMeshProUGUI MainText;
    public TextMeshProUGUI ClosingText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMainText(string mainText) {
        MainText.text = mainText;
    }

    public void UpdateClosingText(string mainText)
    {
        ClosingText.text = mainText;
    }
}
