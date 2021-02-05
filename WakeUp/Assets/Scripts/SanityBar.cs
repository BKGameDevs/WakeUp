using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : PropertyChangedListener
{
    private Slider _Slider;
    public Gradient SanityBarGradient;
    public Image Fill;
    //public FloatVariable PlayerSanity;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //    //if (PlayerSanity != null)
    //    //    PlayerSanity.PropertyChanged += (s, e) => SetSanity(PlayerSanity.RuntimeValue);
    //}

    protected override void Initialize()
    {
        base.Initialize();
        _Slider = GetComponent<Slider>();
    }

    protected override void OnPropertyChanged(PropertyChangedScriptableObject propertyChangeSO)
    {
        if (propertyChangeSO.name == "PlayerSanity")
        {
            var playerSanity = propertyChangeSO as FloatVariable;
            SetSanity(playerSanity.RuntimeValue);
        }
    }

    //private void OnDestroy()
    //{
    //    if (PlayerHp != null)
    //        PlayerHp.PropertyChanged -= PlayerHpChanged;
    //}

    //private void PlayerHpChanged(object sender, EventArgs eventArgs) => SetSanity(PlayerHp.RuntimeValue);

    public void SetSanity(float sanity)
    {
        _Slider.value = sanity;
        Fill.color = SanityBarGradient.Evaluate(_Slider.normalizedValue);
    }
}
