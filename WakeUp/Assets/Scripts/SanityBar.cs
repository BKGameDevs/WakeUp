using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    private Slider _Slider;
    public Gradient SanityBarGradient;
    public Image Fill;
    public FloatVariable PlayerHp;
    // Start is called before the first frame update
    void Start()
    {
        _Slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHp != null)
            SetSanity(PlayerHp.Value);
    }

    public void SetSanity(float sanity)
    {
        _Slider.value = sanity;
        Fill.color = SanityBarGradient.Evaluate(_Slider.normalizedValue);
    }
}
