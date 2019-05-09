using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPhospho : MonoBehaviour
{
    //[SerializeField] GameObject _textPhospho;
    //[SerializeField] GameObject _lightPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //_textPhospho.SetActive(false);
        //_lightPlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //_textPhospho.SetActive(true);
           // _lightPlayer.SetActive(true);
            //ChangeLightSettings();
            AkSoundEngine.PostEvent("test", gameObject);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //_textPhospho.SetActive(false);
            //_lightPlayer.SetActive(false);
            //PutSettingsBack();
        }
    }

    
}
