using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    [SerializeField] bool _isRealeased = false;
    float _deltaTime;
    [SerializeField] Text fpsText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_isRealeased) CalculateFPS();
    }

    void CalculateFPS()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        float lFps = 1.0f / _deltaTime;
        fpsText.text = Mathf.Ceil(lFps).ToString();
    }
}
