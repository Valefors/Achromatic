using Rewired;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class OptionScreen : MonoBehaviour
{
    //Mouse sensibility
    [SerializeField] TextMeshProUGUI _sliderMouseValue;
    [SerializeField] Slider _sliderMouse;

    //Brightness level
    [SerializeField] TextMeshProUGUI _sliderBrightnessValue;
    [SerializeField] Slider _sliderBrightness;
    [SerializeField] PostProcessVolume _volume;
    ColorGrading _colorGradingLayer = null;

    public delegate void UpdateAction();
    public static event UpdateAction OnUpdate;

    public delegate void UpdateLight();
    public static event UpdateLight OnLightUpdate;

    public void Start()
    {
        _volume.profile.TryGetSettings(out _colorGradingLayer); 
    }

    public void OnClickSwitchControllersEn()
    {
        ReInput.players.GetPlayer(0).controllers.maps.SetMapsEnabled(false, 0);
        ReInput.players.GetPlayer(0).controllers.maps.SetMapsEnabled(true, 1);

        Utils.LANGUAGE = Enums.ELanguage.ENGLISH;
    }

    public void OnClickSwitchControllersFr()
    {
        ReInput.players.GetPlayer(0).controllers.maps.SetMapsEnabled(false, 1);
        ReInput.players.GetPlayer(0).controllers.maps.SetMapsEnabled(true, 0);

        Utils.LANGUAGE = Enums.ELanguage.FRENCH;
    }

    public void UpdateSensibilityValue()
    {
        if (_sliderMouseValue != null) _sliderMouseValue.text = _sliderMouse.value.ToString();
        Utils.MOUSE_SENSIBILITY = _sliderMouse.value;
    }

    public void SetQuality(Dropdown pDropDown)
    {
        int value = pDropDown.value;

        Utils.QUALITY_LEVEL = value;
        QualitySettings.SetQualityLevel(value);

        if(OnLightUpdate != null) OnLightUpdate();
    }

    public void SetFullScreen(Toggle pToggle)
    {
        bool isFullScreen = pToggle.isOn;

        Utils.FULLSCREEN = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public static void SetDifficulty(bool pIsDifficult)
    {
        if (!pIsDifficult) Utils.DIFFICULTY_MODE = Utils.EASY_MODE;
        else Utils.DIFFICULTY_MODE = Utils.DIFFICULT_MODE;

        if (OnUpdate != null) OnUpdate();
    }

    public void UpdateBrightnessValue()
    {
        float value = Mathf.Round(_sliderBrightness.value * 10.0f) * 0.1f;

        if (_sliderBrightness != null) _sliderBrightnessValue.text = value.ToString();
        _colorGradingLayer.postExposure.value = value;
        Utils.BRIGHTNESS_LEVEL = value;
    }
}
