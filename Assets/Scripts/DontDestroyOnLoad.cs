using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    static bool created = false;
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            AkSoundEngine.PostEvent("StartMusic", gameObject);
            print("yahgo");
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
