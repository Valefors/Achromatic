using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    [SerializeField] RawImage _rawImage;
    [SerializeField] VideoPlayer _videoPlayer;

    #region Singleton
    public static StreamVideo instance {
        get { return _instance; }
    }

    private static StreamVideo _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Debug.LogError("AN INSTANCE ALREADY EXISTS");

    }
    #endregion

    // Start is called before the first frame update
    public void StartVideo()
    {
        StartCoroutine(PlayVideo());
        //_videoPlayer.loopPointReached += MenuManager.instance.OnLoading;
    }

    IEnumerator PlayVideo()
    {
        _videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);

        while (!_videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        _rawImage.texture = _videoPlayer.texture;
        _videoPlayer.Play();
    }
}
