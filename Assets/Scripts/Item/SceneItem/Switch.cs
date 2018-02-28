using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Switch : MonoBehaviour {

    public string gateId = "a1";
    public string code = "1";
    public bool opened = false;
    private KeyValuePair<string, string> pair;
	void Start ()
    {
        pair = new KeyValuePair<string, string>(gateId, code);
        GameRoot.Instance.evt.AddListener(GameEventDefine.RESET_SWITCH, ResetSwitch);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && opened == false)
        {
            opened = true;
            GameRoot.Instance.evt.CallEvent(GameEventDefine.OPEN_GATE, pair);
            transform.DOMoveY(-1f, 1f);
        }
    }
    private void ResetSwitch(object obj)
    {
        string id = (string)obj;
        if (id == gateId)
        {
            transform.DOMoveY(-0.8f, 1f);
            opened = false;
        }
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.RESET_SWITCH, ResetSwitch);


    }
}
