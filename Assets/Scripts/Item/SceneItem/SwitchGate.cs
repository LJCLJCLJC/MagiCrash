﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwitchGate : MonoBehaviour {
    public string GateId = "a1";
    public string OpenCode = "1";
    public float toPosY=8f;
    public float duration=3f;
    public string nowCode = "";
	void Start () {
        GameRoot.Instance.evt.AddListener(GameEventDefine.OPEN_GATE, OpenHelper);
	}

    private void OpenGate()
    {
        transform.DOMoveY(toPosY, duration);
    }
    private void OpenHelper(object obj)
    {
        KeyValuePair<string, string> pair = (KeyValuePair<string, string>)obj;
        if (GateId == pair.Key)
        {
            nowCode += pair.Value;
            if (OpenCode == nowCode)
            {
                OpenGate();
            }
            else if (nowCode.Length == OpenCode.Length)
            {
                TimeLine.GetInstance().AddTimeEvent(ResetSwitch, 3f, null, gameObject);
                
            }
        }
    }

    private void ResetSwitch(object obj)
    {
        nowCode = "";
        GameRoot.Instance.evt.CallEvent(GameEventDefine.RESET_SWITCH, GateId);
        TimeLine.GetInstance().RemoveTimeEvent(ResetSwitch);
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.OPEN_GATE, OpenHelper);

    }
}
