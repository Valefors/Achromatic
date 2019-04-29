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
    private void Start()
    {
        StartCoroutine(StaticFunctions.FadeOut(result => _FadeInOutPanel.GetComponent<CanvasGroup>().alpha = result, 0.8f, null));
        _currentPanel = _menuPanel;
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
        _introPanel.GetComponent<Image>().sprite = _introImages[0];
        StartCoroutine(IntroCoroutine());
    }

    IEnumerator IntroCoroutine()
    {
        for(int i =0; i < _introImages.Length; i++)
        {
            _introPanel.GetComponent<Image>().sprite = _introImages[i];
            yield return new WaitForSeconds(1f);
        }

        LoadScene();
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
