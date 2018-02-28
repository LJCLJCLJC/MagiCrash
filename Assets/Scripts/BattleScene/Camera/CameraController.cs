using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float smooth;

    public Vector3 v3;
    public Vector3 targetPosition;
    public Vector3 targetRotation;

    private bool canMove=true;

	void Start () {
        GameRoot.Instance.evt.AddListener(GameEventDefine.MOVE_CAMERA, PauseCam);
    }

	void LateUpdate () {
        if (canMove)
        {
            targetPosition = target.position + v3;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
            transform.rotation = Quaternion.Euler(targetRotation);
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

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.MOVE_CAMERA, PauseCam);
    }
}
