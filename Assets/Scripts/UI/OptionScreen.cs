using Rewired;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _sliderValue;
    [SerializeField] Slider _slider;

    public delegate void UpdateAction();
    public static event UpdateAction OnUpdate;

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
        if (_sliderValue != null) _sliderValue.text = _slider.value.ToString();
        Utils.MOUSE_SENSIBILITY = _slider.value;
    }

    public void SetQuality(Dropdown pDropDown)
    {
        int value = pDropDown.value;

        Utils.QUALITY_LEVEL = value;
        QualitySettings.SetQualityLevel(value);
    }

    public void SetFullScreen(bool pIsFullScreen)
    {
        Utils.FULLSCREEN = pIsFullScreen;
        Screen.fullScreen = pIsFullScreen;
    }

    public static void SetDifficulty(bool pIsDifficult)
    {
        if (!pIsDifficult) Utils.DIFFICULTY_MODE = Utils.EASY_MODE;
        else Utils.DIFFICULTY_MODE = Utils.DIFFICULT_MODE;

        if (OnUpdate != null) OnUpdate();
    }
}
