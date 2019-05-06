using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(Utils.INGAME_SCENE);
        AkSoundEngine.SetState("Navigation", "Ingame");
    }
}
