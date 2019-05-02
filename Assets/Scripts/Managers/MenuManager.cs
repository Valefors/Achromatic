using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    RectTransform _currentPanel;
    RectTransform _selectedPanel;
    [SerializeField] RectTransform _menuPanel;
    [SerializeField] RectTransform _creditsPanel;
    [SerializeField] RectTransform _introPanel;

    [SerializeField] RectTransform _FadeInOutPanel;
    [SerializeField] Sprite[] _introImages;

    bool _inIntro = false;
    int _introIndex = 0;

    private void Start()
    {
        StartCoroutine(StaticFunctions.FadeOut(result => _FadeInOutPanel.GetComponent<CanvasGroup>().alpha = result, 0.8f, null));
        _currentPanel = _menuPanel;
    }

    private void Update()
    {
        if (!_inIntro) return;

        if(Input.GetMouseButtonDown(0))
        {
            UpdateIntro();
        }   
    }

    public void OnBack()
    {
        _selectedPanel = _menuPanel;
        DisableCurrentScreen();
    }

    public void OnCredits()
    {
        _selectedPanel = _creditsPanel;
        DisableCurrentScreen();
    }

    public void OnPlay()
    {
        StartCoroutine(StaticFunctions.FadeIn(result => _FadeInOutPanel.GetComponent<CanvasGroup>().alpha = result, 1f, Intro));
    }

    void Intro()
    {
        _introPanel.gameObject.SetActive(true);
        StartCoroutine(StaticFunctions.FadeOut(result => _FadeInOutPanel.GetComponent<CanvasGroup>().alpha = result, 0.1f));
        _introPanel.GetComponent<Image>().sprite = _introImages[_introIndex];
        _inIntro = true;
    }

    void UpdateIntro()
    {
        if(_introIndex >= _introImages.Length - 1)
        {
            _inIntro = false;
            LoadScene();
            return;
        }

        _introIndex++;
        _introPanel.GetComponent<Image>().sprite = _introImages[_introIndex];
    }

    void OnLoading()
    {
        StartCoroutine(StaticFunctions.FadeIn(result => _FadeInOutPanel.GetComponent<CanvasGroup>().alpha = result, 1f, LoadScene));
    }

    void LoadScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    void DisableCurrentScreen()
    {
        StartCoroutine(StaticFunctions.FadeOut(result => _currentPanel.GetComponent<CanvasGroup>().alpha = result, 1f, ActivateCurrentScreen));
    }

    void ActivateCurrentScreen()
    {
        _currentPanel.gameObject.SetActive(false);
        _currentPanel = _selectedPanel;
        _selectedPanel.gameObject.SetActive(true);
        StartCoroutine(StaticFunctions.FadeIn(result => _currentPanel.GetComponent<CanvasGroup>().alpha = result, 1f, null));
        _selectedPanel = null;
    }

    // Start is called before the first frame update
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
