﻿using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    bool _screenEnabled = false;
    Player _player;
    [SerializeField] RectTransform _pauseScreen = null;
    [SerializeField] RectTransform _optionsScreen = null;
    [SerializeField] RectTransform _typeWriterScreen = null;
    [SerializeField] RectTransform _choiceScreen = null;
    [SerializeField] RectTransform _endScreen = null;

    [HideInInspector] public bool onUI = false; //When an UI thing is shown on screen

    RectTransform _currentScreen = null;

    //End Screen
    [SerializeField] VideoPlayer _videoPlayer;
    [SerializeField] Button[] _buttons;
    [SerializeField] RawImage _rawImage;

    #region Singleton
    public static UIManager instance {
        get { return _instance; }
    }

    private static UIManager _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Debug.LogError("AN INSTANCE ALREADY EXISTS");

        _player = ReInput.players.GetPlayer(0);
    }
    #endregion

    private void Start()
    {
        Cursor.visible = false;
        CursorLock();

        AkSoundEngine.PostEvent("Play_Ambient", gameObject);
        _videoPlayer.loopPointReached += ShowEndButtons;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        ProcessInput();
    }

    void GetInput()
    {
        _screenEnabled = _player.GetButtonDown(Utils.REWIRED_OPTION_ACTION);
    }

    void ProcessInput()
    {
        if (_screenEnabled && _currentScreen == null)
        {
            TutorialManager.instance.DisableOptionsImage();

            _pauseScreen.gameObject.SetActive(true);
            _currentScreen = _pauseScreen;

            SetUIMode();
        }
    }

    void ShowEndButtons(VideoPlayer e)
    {
        for(int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].gameObject.SetActive(true);
        }
    }

    void SetUIMode()
    {
        onUI = true;
        Cursor.visible = true;
        CursorUnlock();
    }

    public void DisableCurrentScreen()
    {
        if (_currentScreen != null)
        {
            _currentScreen.gameObject.SetActive(false);
            _currentScreen = null;
            onUI = false;
            Cursor.visible = false;
            CursorLock();
        }
    }

    public void OnTypewritterScreen()
    {
        SetUIMode();

        _currentScreen = _typeWriterScreen;
        _currentScreen.gameObject.SetActive(true);
    }

    public void OnChoiceScreen()
    {
        if (_currentScreen != null) _currentScreen.gameObject.SetActive(false);

        _currentScreen = _choiceScreen;
        _currentScreen.gameObject.SetActive(true);

        SetUIMode();
    }

    public void OnEndScreen()
    {
        _currentScreen.gameObject.SetActive(false);
        _currentScreen = _endScreen;
        _currentScreen.gameObject.SetActive(true);
        StartCoroutine(PlayVideo());
    }

    public void OnOptions()
    {
        _currentScreen.gameObject.SetActive(false);
        _currentScreen = _optionsScreen;
        _currentScreen.gameObject.SetActive(true);
    }

    public void OnBack()
    {
        _currentScreen.gameObject.SetActive(false);
        _currentScreen = _pauseScreen;
        _currentScreen.gameObject.SetActive(true);
    }

    public void OnBackMenu()
    {
        SceneManager.LoadScene(Utils.MENU_SCENE);
    }

    public void OnQuit()
    {
        StaticFunctions.QuitGame();
    }

    void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator PlayVideo()
    {
        _videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);

        while (!_videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        _rawImage.texture = _videoPlayer.texture;
        _videoPlayer.Play();
    }
}
