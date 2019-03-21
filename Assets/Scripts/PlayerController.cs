﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _mouseSensivity = 250.0f;

    float rotX = 0;
    float rotY = 0;

    float _speed = 5f;
    float posX = 0;
    float posZ = 0;

    [SerializeField] Camera _cam;

    // Start is called before the first frame update
    void Start()
    {
        CursorLock();
    }

    void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
        Move();
    }

    void Move()
    {
        transform.position += transform.forward * Time.deltaTime * _speed * Input.GetAxis("Vertical");
        transform.position += transform.right * Time.deltaTime * _speed * Input.GetAxis("Horizontal");
    }

    void CameraRotation()
    {
        rotX += Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;
        //rotX = Mathf.Clamp(rotX, -80f, 80f);
        rotY = Mathf.Clamp(rotY, -10f, 10f);

        transform.rotation = Quaternion.Euler(-rotY, rotX, 0f);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 10, 10), "");
    }
}
