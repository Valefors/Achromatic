using System.Collections;
using System.Collections.Generic;
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
        _hiddenCross.SetActive(false);
        _hiddenText.SetActive(false);
    }

    #region Puzzle 1

    public void OpenBlinds()
    {
        _hiddenNumbers.SetActive(false);
        _hiddenCross.SetActive(false);
        _hiddenText.SetActive(false);
        StaticFunctions.ChangeLightSettings(_ambientLight, 1, AmbientMode.Skybox);
    }

    public void CloseBlinds()
    {
        _hiddenNumbers.SetActive(true);
        _hiddenCross.SetActive(true);
        _hiddenText.SetActive(true);
        StaticFunctions.ChangeLightSettings(_offLight, 0, AmbientMode.Flat);
    }
    #endregion

    #region Puzzle 2
    public void UpdateCorrectSpots(bool pUpOrNot)
    {
        if (_correctSpots >= MAX_SPOTS) return;

        if (pUpOrNot) _correctSpots++;
        else _correctSpots--;

        if(_correctSpots >= 4)
        {
            _correctSpots = 4;
            UnlockDrawer();
        }
    }

    void UnlockDrawer()
    {
        InteractableManager.instance.DisableMovableInteraction();
        _drawer.GetComponent<Animator>().SetBool("isOpen", true);
    }
    #endregion
}
