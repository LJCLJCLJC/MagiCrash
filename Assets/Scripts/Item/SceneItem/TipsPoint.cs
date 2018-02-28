using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsPoint : MonoBehaviour {

    public int TipId;
    public bool repeat=true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player"&& !GameController.Instance.showingTip)
        {
            List<int> list = DataManager.Instance.GetTipList(GameRoot.Instance.GetNowPlayer());
            if (!list.Contains(TipId))
            {
                GameRoot.Instance.evt.CallEvent(GameEventDefine.SHOW_TIP, TipId);
            }
            if (repeat == false)
            {
                GameRoot.Instance.GetNowPlayer().tipList += (TipId + "|");
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.CLOSE_TIP, TipId);
        }
    }
}
