using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMover : MonoBehaviour
{

    public Transform[] point;
    private void Start()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.MOVE_CAMERA, MoveCamera);
    }

    private void MoveCamera(object obj)
    {
        int index = (int)obj;
        if (index == -1)
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.GAME_RESUME, null);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            GameRoot.Instance.movingCamera = false;
            return;
        }
        for(int i = 0; i < point.Length; i++)
        {
            if (index == i)
            {
                GameRoot.Instance.movingCamera = true;
                transform.DOMove(point[i].position, 0.6f);
                transform.DORotate(point[i].rotation.eulerAngles, 0.6f).OnComplete<Tween>(delegate() { GameRoot.Instance.movingCamera = false; });
                if (index != 6)
                {
                    GameRoot.Instance.evt.CallEvent(GameEventDefine.GAME_PAUSE, null);
                }

            }
        }
    }

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.MOVE_CAMERA, MoveCamera);
    }
}
