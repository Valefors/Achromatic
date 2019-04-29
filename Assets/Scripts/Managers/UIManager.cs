using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    bool _optionScreenEnabled = false;
    Player _player;
    [SerializeField] RectTransform _optionScreen = null;
    public bool onUI = false; //When an UI thing is shown on screen

    RectTransform _currentScreen;

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
    }
    // Update is called once per frame
    void Update()
    {
        GetInput();
        ProcessInput();
    }

    void GetInput()
    {
        _optionScreenEnabled = _player.GetButtonDown(Utils.REWIRED_OPTION_ACTION);
    }

    void ProcessInput()
    {
        if (_optionScreenEnabled)
        {
            _optionScreen.gameObject.SetActive(true);
            _currentScreen = _optionScreen;

            onUI = true;
            Cursor.visible = true;
            CursorUnlock();
        }
    }

    public void DisableCurrentScreen()
    {
        if (_currentScreen != null)
        {
            _currentScreen.gameObject.SetActive(false);
            onUI = false;
            Cursor.visible = true;
            CursorLock();
        }
    }

    void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
