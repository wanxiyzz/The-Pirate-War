using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightManager : Singleton<GlobalLightManager>
{
    private Light2D globalLight;
    private Coroutine changeLightIntensityCoroutine;
    protected override void Awake()
    {
        base.Awake();
        globalLight = GetComponent<Light2D>();
    }
    public void ChangeLightIntensity(float intensity)
    {
        if (changeLightIntensityCoroutine != null)
        {
            StopCoroutine(changeLightIntensityCoroutine);
            changeLightIntensityCoroutine = null;
        }
        changeLightIntensityCoroutine = StartCoroutine(IEChangeLightIntensity(intensity));
    }
    IEnumerator IEChangeLightIntensity(float intensity)
    {
        float currenIntensity = globalLight.intensity;
        for (int i = 1; i < 51; i++)
        {
            globalLight.intensity = Mathf.Lerp(currenIntensity, intensity, i / 50f);
            yield return Setting.waitForFixedUpdate;
        }
    }
}
