using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    int _correctSpots = 0; //Number of correct objects put for puzzle 2
    const int MAX_SPOTS = 4;

    [SerializeField] GameObject _drawer;

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

    public void UpdateCorrectSpots(bool pUpOrNot)
    {
        print(_correctSpots);
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
        print("KEY");
        InteractableManager.instance.DisableMovableInteraction();
        _drawer.GetComponent<Animator>().SetBool("isOpen", true);
    }
}
