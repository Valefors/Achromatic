using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
    [SerializeField] float minX = -360.0f;
    [SerializeField] float maxX = 360.0f;

    [SerializeField] float minY = -180.0f;
    [SerializeField] float maxY = 180.0f;

    [SerializeField] float sensX = 500.0f;
    [SerializeField] float sensY = 500.0f;

    [SerializeField] float rotationY = 0.0f;
    [SerializeField] float rotationX = 0.0f;

    bool _isManipulating = false;

    void Update()
    {
        if (!_isManipulating) return;

        rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);
        transform.localEulerAngles = new Vector3(-rotationY, -rotationX, 0);
    }

    public void SetManipulationMode()
    {
        _isManipulating = true;
    }

    public void SetNormalMode()
    {
        _isManipulating = false;
    }

}
