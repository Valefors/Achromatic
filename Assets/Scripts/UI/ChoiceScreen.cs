using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceScreen : MonoBehaviour
{
    #region Singleton
    public static ChoiceScreen instance {
        get { return _instance; }
    }

    private static ChoiceScreen _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Debug.LogError("AN INSTANCE ALREADY EXISTS");
    }
    #endregion

    public int test = 3;

    public void OnCulpritSelected(bool pIsCorrect)
    {
        if (pIsCorrect) print("Marco best husbando");
        else print("Bayonetta best waifu");

        UIManager.instance.OnEndScreen();
    }
}
