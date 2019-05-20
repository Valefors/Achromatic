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
        _hiddenNumbers.SetActive(false);
        //HideNumbers();
        _hiddenCross.SetActive(false);
        _hiddenText.SetActive(false);
    }

    #region Puzzle 1

    public void OpenBlinds()
    {
        _hiddenNumbers.SetActive(false);
        //ShowNumbers();
        _hiddenCross.SetActive(false);
        _hiddenText.SetActive(false);
        
    }

    public void CloseBlinds()
    {
        _hiddenNumbers.SetActive(true);
        //HideNumbers();
        _hiddenCross.SetActive(true);
        _hiddenText.SetActive(true);
    }

    void ShowNumbers()
    {
        int children = _hiddenNumbers.transform.childCount;

        for (int i = 0; i < children; i++)
        {
            Color colorText = _hiddenNumbers.transform.GetChild(i).GetComponentInChildren<TextMeshPro>().color;
            StaticFunctions.FadeIn(result => colorText.a = result, Utils.TURN_ON_LIGHT_DELAY);
            _hiddenNumbers.transform.GetChild(i).GetComponentInChildren<TextMeshPro>().color = colorText;
        }

        Color colorOtherText = _hiddenCross.GetComponentInChildren<TextMeshPro>().color;
        StaticFunctions.FadeIn(result => colorOtherText.a = result, Utils.TURN_ON_LIGHT_DELAY);

        _hiddenCross.GetComponentInChildren<TextMeshPro>().color = colorOtherText;
        _hiddenText.GetComponentInChildren<TextMeshPro>().color = colorOtherText;
    }

    void HideNumbers()
    {
        int children = _hiddenNumbers.transform.childCount;
        
        /*for(int i = 0; i < children; i++)
        {
            Color colorText = _hiddenNumbers.transform.GetChild(i).GetComponentInChildren<TextMeshPro>().faceColor;
            print(colorText.a);
            //Color colorOutlineText = _hiddenNumbers.transform.GetChild(i).GetComponentInChildren<TextMeshPro>().outlineColor;
            //colorText.a = 0f;
            StartCoroutine(StaticFunctions.FadeOut(result => colorText.a = result, 0.1f, null));
            print(colorText.a);
            //StaticFunctions.FadeOut(result => colorOutlineText.a = result, Utils.TURN_OFF_LIGHT_DELAY);
            _hiddenNumbers.transform.GetChild(i).GetComponentInChildren<TextMeshPro>().faceColor = colorText;
            _hiddenNumbers.transform.GetChild(i).GetComponentInChildren<TextMeshPro>().outlineColor = colorText;
        }*/
        Color colorOtherText = _hiddenCross.GetComponentInChildren<TextMeshPro>().faceColor;

        StartCoroutine(StaticFunctions.FadeOut(result => colorOtherText.a = result, 1f, null));

        _hiddenCross.GetComponentInChildren<TextMeshPro>().faceColor = colorOtherText;
        _hiddenCross.GetComponentInChildren<TextMeshPro>().outlineColor = colorOtherText;
        _hiddenText.GetComponentInChildren<TextMeshPro>().faceColor = colorOtherText;
        _hiddenText.GetComponentInChildren<TextMeshPro>().outlineColor = colorOtherText;
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
