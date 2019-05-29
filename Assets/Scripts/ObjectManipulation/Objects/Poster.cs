using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poster : MonoBehaviour
{
    [SerializeField] Material[] _postersMaterial;

    // Start is called before the first frame update
    void Start()
    {
        int lRandom = Random.Range(0, _postersMaterial.Length);
        GetComponent<MeshRenderer>().material = _postersMaterial[lRandom];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int lRandom = Random.Range(0, _postersMaterial.Length);
            GetComponent<MeshRenderer>().material = _postersMaterial[lRandom];
        }
    }
}
