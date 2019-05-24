using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] VolumetricLight[] _volumetricLights;

    // Start is called before the first frame update
    void Awake()
    {
        OptionScreen.OnLightUpdate += UpdateLights;
    }

    // Update is called once per frame
    void UpdateLights()
    {
        for (int i =0; i < _volumetricLights.Length; i++)
        {
            if(Utils.QUALITY_LEVEL == 0) _volumetricLights[i].enabled = false;
            else _volumetricLights[i].enabled = true;
        }
    }
}
