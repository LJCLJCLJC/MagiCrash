using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public float smooth;

    public Vector3 v3;
    public Vector3 targetPosition;
    public Vector3 targetRotation;
    public float maxSensitivityX = 10f;
    private float sensitivityX;

    private bool canMove = true;

    void Start()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.MOVE_CAMERA, PauseCam);
        GameRoot.Instance.evt.AddListener(GameEventDefine.SET_VIEW_SENSITIVE, OnSetViewSensitivity);
        sensitivityX = DataManager.Instance.GetSettingData().viewSensitive * maxSensitivityX;
    }

    void LateUpdate()
    {
        if (canMove)
        {
            targetPosition = target.position + v3;
            transform.parent.position = Vector3.Lerp(transform.position, targetPosition, smooth);
            //transform.rotation = Quaternion.Euler(targetRotation);
            if (Input.GetMouseButton(1))
            {
                transform.RotateAround(target.position,Vector3.up, Input.GetAxis("Mouse X") * sensitivityX);
            }
            

        }
    }

    private void PauseCam(object obj)
    {
        int index = (int)obj;
        if (index == -1)
        {
            canMove = true;
            GameRoot.Instance.evt.CallEvent(GameEventDefine.GAME_RESUME, null);
        }
        else
        {
            canMove = false;
        }
    }

    private void OnSetViewSensitivity(object obj)
    {
        sensitivityX = (float)obj * maxSensitivityX;
    }

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.MOVE_CAMERA, PauseCam);
    }
}
