using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class PuzzleManager : MonoBehaviour
{
    int _correctSpots = 0; //Number of correct objects put for puzzle 2
    const int MAX_SPOTS = 4;

    [SerializeField] GameObject _drawer;
    [SerializeField] GameObject _hiddenNumbers;
    //Bad stuff, need an array
    [SerializeField] GameObject _hiddenCross;
    [SerializeField] GameObject _hiddenText;

    [SerializeField] MovableInteractable[] _movableObjects;

    Color _offLight = new Color(0, 0, 0);
    Color _ambientLight = Utils.lightColor;

    #region Singleton
    public static PuzzleManager instance {
        get { return _instance; }
    }

    static PuzzleManager _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Debug.LogError("AN INSTANCE ALREADY EXISTS");
    }
    #endregion

    private void Start()
    {
        HideNumbers();
    }

    #region Puzzle 1

    public void OpenBlinds()
    {
        HideNumbers(Utils.TURN_ON_LIGHT_DELAY / 5);
    }

    public void CloseBlinds()
    {
        ShowNumbers(Utils.TURN_OFF_LIGHT_DELAY);
    }

    void ShowNumbers(float pDelay = 2)
    {
        int children = _hiddenNumbers.transform.childCount;

        for (int i = 0; i < children; i++)
        {
            TextMeshPro child = _hiddenNumbers.transform.GetChild(i).GetComponentInChildren<TextMeshPro>();
            StartCoroutine(StaticFunctions.FadeInAlpha(result => child.color = result, child.color, pDelay));
        }

        TextMeshPro hiddenCross = _hiddenCross.transform.GetComponentInChildren<TextMeshPro>();
        StartCoroutine(StaticFunctions.FadeInAlpha(result => hiddenCross.color = result, hiddenCross.color, pDelay));

        TextMeshPro hiddenText = _hiddenText.transform.GetComponentInChildren<TextMeshPro>();
        StartCoroutine(StaticFunctions.FadeInAlpha(result => hiddenText.color = result, hiddenText.color, pDelay));
    }

    void HideNumbers(float pDelay = 0.5f)
    {
        int children = _hiddenNumbers.transform.childCount;

        for (int i = 0; i < children; i++)
        {
            TextMeshPro child = _hiddenNumbers.transform.GetChild(i).GetComponentInChildren<TextMeshPro>();
            StartCoroutine(StaticFunctions.FadeOutAlpha(result => child.color = result, child.color, pDelay));
        }

        TextMeshPro hiddenCross = _hiddenCross.transform.GetComponentInChildren<TextMeshPro>();
        StartCoroutine(StaticFunctions.FadeOutAlpha(result => hiddenCross.color = result, hiddenCross.color, pDelay));

        TextMeshPro hiddenText = _hiddenText.transform.GetComponentInChildren<TextMeshPro>();
        StartCoroutine(StaticFunctions.FadeOutAlpha(result => hiddenText.color = result, hiddenText.color, pDelay));
    }

    #endregion

    #region Puzzle 2
    public void CheckCorrectPosition()
    {
        if (_correctSpots >= MAX_SPOTS) return;

        _correctSpots = 0;

        for(int i = 0; i < _movableObjects.Length; i++)
        {
            if (_movableObjects[i].putLocation == _movableObjects[i].correctLocation) _correctSpots++;
        }

        print(_correctSpots);
        if (_correctSpots == MAX_SPOTS) UnlockDrawer();
    }

    void UnlockDrawer()
    {
        InteractableManager.instance.DisableMovableInteraction();
        _drawer.GetComponent<Animator>().SetBool("isOpen", true);
        //CORENTIN: OUVRIR TIRROIR
    }
    #endregion
}
