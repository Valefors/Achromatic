using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Rendering;

public static class StaticFunctions
{
    public static IEnumerator FadeIn(Action<float> myVariableResult, float fadeTime, Action callback = null)
    {
        float progress = 0f;
        float newValue = 0f;

        while (newValue < 1f)
        {
            newValue = Mathf.Lerp(0f, 1f, progress);
            myVariableResult(newValue);
            progress += Time.deltaTime / fadeTime;
            yield return null;
        }

        // Allow to call a method at the end of the coroutine
        if (callback != null)
        {
            callback();
        }
    }

    public static IEnumerator FadeOut(Action<float> myVariableResult, float fadeTime, Action callback = null)
    {
        float progress = 0f;
        float newValue = 1f;

        while (newValue > 0f)
        {
            newValue = Mathf.Lerp(1f, 0f, progress);
            myVariableResult(newValue);
            progress += Time.deltaTime / fadeTime;
            yield return null;
        }

        // Allow to call a method at the end of the coroutine
        if (callback != null)
        {
            callback();
        }
    }

    public static IEnumerator FadeInWwise(string RTPCname, float duration)
    {
        float progress = 0f;
        int newValue = 0;

        while (newValue < 100)
        {
            newValue = (int)Mathf.Lerp(0f, 100f, progress);
            AkSoundEngine.SetRTPCValue(RTPCname, newValue);
            progress += Time.unscaledDeltaTime / duration;
            yield return null;
        }
    }

    public static IEnumerator FadeOutWwise(string RTPCname, float duration)
    {
        float progress = 0f;
        int newValue = 100;

        while (newValue > 0)
        {
            newValue = (int)Mathf.Lerp(100f, 0f, progress);
            AkSoundEngine.SetRTPCValue(RTPCname, newValue);
            progress += Time.unscaledDeltaTime / duration;
            yield return null;
        }
    }

    public static void ChangeLightSettings(Color pLightColor, float pLightIntensity = 1,  AmbientMode pAmbientMode = AmbientMode.Flat)
    {
        RenderSettings.reflectionIntensity = pLightIntensity;
        RenderSettings.ambientMode = pAmbientMode;
        //RenderSettings.ambientSkyColor = pLightColor;
       RenderSettings.ambientLight = pLightColor;

    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
