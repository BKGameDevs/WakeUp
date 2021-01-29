using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class SpotLightController : MonoBehaviour
{
    public FloatVariable PlayerHp;
    private Light2D _SpotLight;
    private float _OuterMax;
    private float _InnerMax;
    // Start is called before the first frame update
    void Start()
    {
        _SpotLight = GetComponent<Light2D>();
        _InnerMax = _SpotLight.pointLightInnerRadius;
        _OuterMax = _SpotLight.pointLightOuterRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHp != null)
            AdjustLight(PlayerHp.Value);
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
