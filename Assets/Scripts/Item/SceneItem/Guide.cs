using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public int id;
    public int tipId;
    public Transform target;

    private List<int> guideList;
    private void Start()
    {
        guideList = DataManager.Instance.GetGuideList(GameRoot.Instance.GetNowPlayer());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (id != 0 && !guideList.Contains(id) && other.tag == "Player")
        {
            if (target != null)
            {
                GameRoot.Instance.evt.CallEvent(GameEventDefine.GUIDE_OPEN, target.position);
            }
            GameRoot.Instance.evt.CallEvent(GameEventDefine.SHOW_TIP, tipId);
            GameRoot.Instance.GetNowPlayer().guideList += (id + "|");
            guideList = DataManager.Instance.GetGuideList(GameRoot.Instance.GetNowPlayer());
        }
        else if (id == 0 && other.tag == "Player")
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.GUIDE_OFF,null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (target != null)
        {
            Gizmos.DrawWireSphere(target.position, 1);
        }

    }
}
