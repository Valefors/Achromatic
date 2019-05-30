using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour
{
    [SerializeField] Animator _animator;
    int counter = 0;
    int playRate = 0;
    int PLAY_RATE_MIN = 150;
    int PLAY_RATE_MAX = 1000;

    // Start is called before the first frame update
    void Start()
    {
        PlayAnimation();
    }

    // Update is called once per frame
    void PlayAnimation()
    {
        _animator.Play("CarLight", -1, 0f);
    }

    private void Update()
    {
        counter++;

        if(counter >= playRate)
        {
            PlayAnimation();
            counter = 0;
            playRate = Random.Range(PLAY_RATE_MIN, PLAY_RATE_MAX);
            print(playRate);
        }
    }
    
}
