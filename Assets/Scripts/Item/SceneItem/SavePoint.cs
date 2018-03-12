using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {

    public Vector3 startPoint;
    public bool ifNowPos=true;
	void Start ()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBody")
        {
            if (ifNowPos)
            {
                GameRoot.Instance.GetNowPlayer().startPosition = GameController.Instance.tsPlayer.position.x + "#" + GameController.Instance.tsPlayer.position.y + "#" + GameController.Instance.tsPlayer.position.z;
            }
            else
            {
                GameRoot.Instance.GetNowPlayer().startPosition = startPoint.x + "#" + startPoint.y + "#" + startPoint.z;
            }
            GameRoot.Instance.evt.CallEvent(GameEventDefine.SAVE_GAME,null);
        }
    }
    private void OnDrawGizmos()
    {
        if (!ifNowPos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(startPoint, 1);
            Gizmos.DrawLine(transform.position, startPoint);
        }

    }
}
