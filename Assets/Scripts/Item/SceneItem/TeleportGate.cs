using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGate : MonoBehaviour {

    private bool Cleared = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBody"&& Cleared == false)
        {
            Cleared = true;
            UIManager.Instance.PushPanel(Panel_ID.LevelClearPanel);
            GameRoot.Instance.evt.CallEvent(GameEventDefine.GAME_PAUSE, null);
            GameController.Instance.isPausing = true;
        }
    }
}
