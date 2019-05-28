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

    //Difficulty level
    [SerializeField] Slider _sliderDifficulty;
    [SerializeField] TextMeshProUGUI _difficultyText;

    //Screen Resolution
    [SerializeField] Toggle _fullscreenToggle;

    //Events
    public delegate void UpdateAction();
    public static event UpdateAction OnUpdate;

    public delegate void UpdateLight();
    public static event UpdateLight OnLightUpdate;

    public void Start()
    {
        _volume.profile.TryGetSettings(out _colorGradingLayer);

        if (Utils.DIFFICULTY_MODE == Utils.EASY_MODE)
        {
            _sliderDifficulty.value = 0;
            _difficultyText.text = Utils.EASY_TEXT_MODE;
        }

        if (Utils.DIFFICULTY_MODE == Utils.NORMAL_MODE)
        {
            _sliderDifficulty.value = 1;
            _difficultyText.text = Utils.NORMAL_TEXT_MODE;
        }

        if (Utils.DIFFICULTY_MODE == Utils.DIFFICULT_MODE)
        {
            _sliderDifficulty.value = 2;
            _difficultyText.text = Utils.DIFFICULT_TEXT_MODE;
        }
    }

    #region Controls
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
    #endregion

    #region Mouse Sensibility
    public void UpdateSensibilityValue()
    {
        if (_sliderMouseValue != null) _sliderMouseValue.text = _sliderMouse.value.ToString();
        Utils.MOUSE_SENSIBILITY = _sliderMouse.value;
    }
    #endregion

    #region Quality
    public void SetQuality(Dropdown pDropDown)
    {
        int value = pDropDown.value;

        Utils.QUALITY_LEVEL = value;
        QualitySettings.SetQualityLevel(value);

        if(OnLightUpdate != null) OnLightUpdate();
    }
    #endregion

    #region Screen Resolution

    /*public void SetResolution(Dropdown pDropDown)
    {
        int value = pDropDown.value;
        int width = (int)Mathf.Round(Utils.resolutionArray[value].x);
        int height = (int)Mathf.Round(Utils.resolutionArray[value].y);

        if (value != Utils.resolutionArray.Count - 1 && Utils.FULLSCREEN)
        {
            _fullscreenToggle.isOn = false;
            Utils.FULLSCREEN = false;
        }

        Screen.SetResolution(width, height, Utils.FULLSCREEN);
    }*/

    public void SetFullScreen()
    {
        bool isFullScreen = _fullscreenToggle.isOn;

        Utils.FULLSCREEN = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    #endregion

    #region Difficulty

    public void SetDifficulty(int pDifficultyLevel = 0)
    {
        int value = _sliderDifficulty == null ? pDifficultyLevel : (int)_sliderDifficulty.value;

        switch (value)
        {
            case 0:
                Utils.DIFFICULTY_MODE = Utils.EASY_MODE;
                _difficultyText.text = Utils.EASY_TEXT_MODE;

                break;

            case 1:
                Utils.DIFFICULTY_MODE = Utils.NORMAL_MODE;
                _difficultyText.text = Utils.NORMAL_TEXT_MODE;

                break;

            case 2:
                Utils.DIFFICULTY_MODE = Utils.DIFFICULT_MODE;
                _difficultyText.text = Utils.DIFFICULT_TEXT_MODE;

                break;
        }

        if (OnUpdate != null) OnUpdate();
    }

    #endregion

    #region Brightness

    public void UpdateBrightnessValue()
    {
        float value = Mathf.Round(_sliderBrightness.value * 10.0f) * 0.1f;

        if (_sliderBrightness != null) _sliderBrightnessValue.text = value.ToString();
        _colorGradingLayer.postExposure.value = value;
        Utils.BRIGHTNESS_LEVEL = value;
    }

    #endregion
}
