using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class SpotLightController : PropertyChangedListener
{
    private Light2D _SpotLight;
    private float _OuterMax;
    private float _InnerMax;

    protected override void Initialize()
    {
        base.Initialize();
        
        _SpotLight = GetComponent<Light2D>();
        _InnerMax = _SpotLight.pointLightInnerRadius;
        _OuterMax = _SpotLight.pointLightOuterRadius;
    }

    protected override void OnPropertyChanged(PropertyChangedScriptableObject propertyChangeSO)
    {
        if (propertyChangeSO.name == "PlayerSanity")
        {
            var playerSanity = propertyChangeSO as FloatVariable;
            AdjustLight(playerSanity.RuntimeValue);
        }
    }

    private void AdjustLight(float percentage) {
        var innerPercentage = (percentage - 50f) / 50f;
        var outerPercentage = percentage < 50f ? percentage / 50f : 1f;
        
        innerPercentage = innerPercentage < 0 ? 0 : innerPercentage;
        outerPercentage = outerPercentage < 0 ? 0 : outerPercentage;

        _SpotLight.pointLightInnerRadius = _InnerMax * innerPercentage;
        _SpotLight.pointLightOuterRadius = _OuterMax * outerPercentage;
    }
}
