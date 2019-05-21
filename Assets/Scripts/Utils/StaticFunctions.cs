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

    public static IEnumerator FadeInAlpha(Action<Color> myVariableResult, Color startColor, float fadeTime)
    {
        float progress = 0f;
        float alpha = 0f;
        Color color = startColor;

        while (alpha < 1f)
        {
            alpha = Mathf.Lerp(0f, 1f, progress);
            color = new Color(color.r, color.g, color.b, alpha);
            myVariableResult(color);
            progress += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    public static IEnumerator FadeOutAlpha(Action<Color> myVariableResult, Color startColor, float fadeTime)
    {
        float progress = 0f;
        float alpha = 1f;
        Color color = startColor;

        while (alpha > 0f)
        {
            alpha = Mathf.Lerp(1f, 0f, progress);
            color = new Color(color.r, color.g, color.b, alpha);
            myVariableResult(color);
            progress += Time.deltaTime / fadeTime;
            yield return null;
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

    public static IEnumerator ChangeLightSettings(Light pLight, Color pLightColor, float fadeTime, float pLightIntensity = 1,  AmbientMode pAmbientMode = AmbientMode.Flat)
    {
        //RenderSettings.reflectionIntensity = pLightIntensity;
        //RenderSettings.ambientMode = pAmbientMode;
        //Color light = pLight == null ? RenderSettings.ambientLight : pLight;
        Color lightCurrentColor = pLight == null ? RenderSettings.ambientLight : pLight.color;
        float progress = 0f;

        while (lightCurrentColor != pLightColor)
        {
            lightCurrentColor = Color.Lerp(lightCurrentColor, pLightColor, progress);
            progress += Time.deltaTime / fadeTime;

            if (pLight != null) pLight.color = lightCurrentColor;
            else RenderSettings.ambientLight = lightCurrentColor;

            yield return null;
        }
        //RenderSettings.ambientSkyColor = pLightColor;
        lightCurrentColor = pLightColor;

    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
