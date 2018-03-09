using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Switch : MonoBehaviour {

    public string gateId = "a1";
    public string code = "1";
    public bool opened = false;
    public float startY = -0.7f;
    public float endY = -0.9f;
    public GameObject effect;
    private KeyValuePair<string, string> pair;
	void Start ()
    {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        pair = new KeyValuePair<string, string>(gateId, code);
        GameRoot.Instance.evt.AddListener(GameEventDefine.RESET_SWITCH, ResetSwitch);
        effect.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBody" && opened == false)
        {
            opened = true;
            GameRoot.Instance.evt.CallEvent(GameEventDefine.OPEN_GATE, pair);
            transform.DOMoveY(endY, 1f).OnComplete(delegate() { effect.SetActive(true); });
        }
    }
    private void ResetSwitch(object obj)
    {
        string id = (string)obj;
        if (id == gateId)
        {
            transform.DOMoveY(startY, 1f).OnComplete(delegate () { effect.SetActive(false); }); 
            opened = false;
        }
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.RESET_SWITCH, ResetSwitch);


    }
}
