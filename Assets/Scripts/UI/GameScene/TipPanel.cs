using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TipPanel : BasePanel {
    public Text txtDesc;
    public Button btnDesc;
    private int id;
    private List<string> desc;
    private List<int> camPos;
    private int index;
    public override void OnEnter(object param)
    {
        base.OnEnter(param);
        id = (int)param;
        desc = StaticDataPool.Instance.staticTipPool.GetStaticDataVo(id).desc;
        camPos = StaticDataPool.Instance.staticTipPool.GetStaticDataVo(id).camPos;
        index = 0;
        txtDesc.text = desc[index];
        GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, camPos[index]);
        btnDesc.onClick.AddListener(delegate () { BtnClick(btnDesc); });
    }
    public override void OnExit()
    {
        base.OnExit();
        //GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, -1);
        btnDesc.onClick.RemoveAllListeners();
    }
    private void BtnClick(Button btn)
    {
        if (GameRoot.Instance.movingCamera == true) return;
        index++;
        if (index < desc.Count)
        {
            txtDesc.text = desc[index];
        }
        if (index < camPos.Count)
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, camPos[index]);
        }
        if (index == desc.Count)
        {
            UIManager.Instance.PopPanel();
            GameController.Instance.showingTip = false;
            GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, -1);
        }
    }
}
