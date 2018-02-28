using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button btnStart;
    public Button btnSetting;

    private void Start()
    {
        Init();
        btnStart.onClick.AddListener(delegate () { BtnClick(btnStart); });
        btnSetting.onClick.AddListener(delegate () { BtnClick(btnSetting); });
    }

    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnStart":
                UIManager.Instance.PushPanel(Panel_ID.ChoosePlayerPanel);
                GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, 3);
                break;
            case "btnSetting":
                UIManager.Instance.PushPanel(Panel_ID.SettingPanel);
                break;
                
        }
    }

    private void Init()
    {
        //DataManager.Instance.Delete(0);
        //DataManager.Instance.Delete(1);
        //DataManager.Instance.Delete(2);
       
    }

}
