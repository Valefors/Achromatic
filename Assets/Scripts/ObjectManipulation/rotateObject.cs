﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
    [SerializeField] float _rotateSpeed = 200;

    // Start is called before the first frame update
    private void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * _rotateSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * _rotateSpeed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.up, -rotX, Space.World);
        transform.Rotate(Vector3.right, rotY, Space.World);
    }

    public void ResetPosition()
    {
        transform.rotation = Quaternion.identity;
    }
}
