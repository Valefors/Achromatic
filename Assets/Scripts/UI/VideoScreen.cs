using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoScreen : MonoBehaviour
{
    #region Singleton
    public static VideoScreen instance {
        get { return _instance; }
    }

    private static VideoScreen _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Debug.LogError("AN INSTANCE ALREADY EXISTS");
    }
    #endregion

   [SerializeField] Animator _animator;

    public void PlayIntro()
    {
        _animator.SetBool("isIntro", true);
    }

    public void EndIntro()
    {
        print("here");
        MenuManager.instance.OnLoading();
        _animator.SetBool("isIntro", false);
    }

    public void PlayEnding(bool pIsCorrect)
    {
        if(pIsCorrect) _animator.SetBool("isGoodEnding", true);
        else _animator.SetBool("isBadEnding", true);
    }

    public void EndEnding()
    {
        _animator.SetBool("isGoodEnding", false);
        _animator.SetBool("isBadEnding", false);
    }
}
