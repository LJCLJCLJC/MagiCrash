using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {
    private bool inBossBattle = false;
    private void OnTriggerExit(Collider other)
    {
        if (!inBossBattle)
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.BOSS_BATTLE, null);
            GameRoot.Instance.evt.CallEvent(GameEventDefine.MAGIC_CHANGE, true);
            inBossBattle = true;
        }
    }
}
