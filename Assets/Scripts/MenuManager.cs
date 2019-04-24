using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    RectTransform _currentPanel;
    RectTransform _selectedPanel;
    [SerializeField] RectTransform _menuPanel;
    [SerializeField] RectTransform _creditsPanel;

    private void Start()
    {
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
