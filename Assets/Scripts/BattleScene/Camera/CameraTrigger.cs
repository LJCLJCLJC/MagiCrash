using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, 6);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, -1);
        }

    }
}
