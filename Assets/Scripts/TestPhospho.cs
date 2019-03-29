using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPhospho : MonoBehaviour
{
    [SerializeField] GameObject _textPhospho;
    [SerializeField] GameObject _lightPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _textPhospho.SetActive(false);
        _lightPlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _textPhospho.SetActive(true);
            _lightPlayer.SetActive(true);
            ChangeLightSettings();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _textPhospho.SetActive(false);
            _lightPlayer.SetActive(false);
            PutSettingsBack();
        }
    }

    void ChangeLightSettings()
    {
        RenderSettings.reflectionIntensity = 0;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientSkyColor = Color.black;
    }

    void PutSettingsBack()
    {
        RenderSettings.reflectionIntensity = 1;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
        RenderSettings.ambientSkyColor = Color.white;
    }
}
