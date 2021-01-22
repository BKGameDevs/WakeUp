using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    private Slider _Slider;
    public Gradient SanityBarGradient;
    public Image Fill;
    // Start is called before the first frame update
    void Start()
    {
        _Slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void SetSanity(int sanity)
    {
        _Slider.value = sanity;
        Fill.color = SanityBarGradient.Evaluate(_Slider.normalizedValue);
    }
}
