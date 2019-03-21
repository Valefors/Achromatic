using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCinematic : MonoBehaviour
{
    [SerializeField] Transform[] positionArray;

    delegate void DoAction();
    DoAction doAction;

    #region Cinematic Variables
    Transform _startPos; 
    Transform _finalPos; 
    float _speed = 0.5f;

    float _startTime;
    float _totalLenght = 0;
    #endregion

    private void Start()
    {
        doAction = DoActionVoid;
        SetModeCinematic(positionArray[0], positionArray[1], 2);
    }

    void Update()
    {
        if (doAction != null) doAction();
    }

    void DoActionVoid()
    {
        return;
    }

    void SetModeCinematic(Transform pStartPos, Transform pFinalPos, float pSpeed = 0.5f)
    {
        if (pStartPos == null) pStartPos = transform;
        if(pFinalPos == null)
        {
            Debug.LogError("FINAL MOVEMENT NOT ASSIGNED");
            return;
        }

        _startPos = pStartPos;
        _finalPos = pFinalPos;
        _speed = pSpeed;

        _startTime = Time.time;
        _totalLenght = Vector3.Distance(_startPos.position, _finalPos.position);

        doAction = DoActionCinematic;
    }
     void DoActionCinematic()
     {
        /*if (Mathf.Abs(transform.position.magnitude - _finalPos.position.magnitude) < 0.0001)
        {
            transform.position = _finalPos.position;
            _startPos = null;
            _finalPos = null;
            _speed = 0.5f;

            doAction = DoActionVoid;
            return;
        }*/

        float _distanceCovered = (Time.time - _startTime) * _speed;
        float fracDistance = _distanceCovered / _totalLenght;

        transform.position = Vector3.Lerp(_startPos.position, _finalPos.position, fracDistance);

        //transform.position = Vector3.Lerp(_startPos.position, _finalPos.position, Time.deltaTime * _speed);

        Vector3 currentAngle = new Vector3(Mathf.Lerp(_startPos.rotation.eulerAngles.x, _finalPos.rotation.eulerAngles.x, fracDistance),
                                           Mathf.Lerp(_startPos.rotation.eulerAngles.y, _finalPos.rotation.eulerAngles.y, fracDistance),
                                           Mathf.Lerp(_startPos.rotation.eulerAngles.z, _finalPos.rotation.eulerAngles.z, fracDistance));

        transform.eulerAngles = currentAngle;
     }
}
