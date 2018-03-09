using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameOverPanel : BasePanel
{

    public Button btnRestart;
    public Button btnSetting;
    public Button btnHome;
    public override void OnEnter()
    {
        base.OnEnter();
        btnRestart.onClick.AddListener(delegate () { BtnClick(btnRestart); });
        btnSetting.onClick.AddListener(delegate () { BtnClick(btnSetting); });
        btnHome.onClick.AddListener(delegate () { BtnClick(btnHome); });
    }

    public override void OnExit()
    {
        base.OnExit();
        btnRestart.onClick.RemoveAllListeners();
        btnSetting.onClick.RemoveAllListeners();
        btnHome.onClick.RemoveAllListeners();
    }
    private void BtnClick(Button button)
    {
        switch (button.name)
        {
            case "btnRestart":
                LoadManager.Load(GameController.Instance.LevelName);
                GameRoot.Instance.evt.CallEvent(GameEventDefine.GAME_RESUME, null);
                break;
            case "btnSetting":
                break;
            case "btnHome":
                LoadManager.Load(SceneName.StartScene);
                GameRoot.Instance.evt.CallEvent(GameEventDefine.GAME_RESUME, null);
                break;
        }
    }
}
