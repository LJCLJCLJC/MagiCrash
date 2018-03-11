using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : BasePanel {

    public Button btnResume;
    public Button btnRestart;
    public Button btnSetting;
    public Button btnHome;
	public override void OnEnter()
    {
        base.OnEnter();
        btnRestart.onClick.AddListener(delegate () { BtnClick(btnRestart); });
        btnResume.onClick.AddListener(delegate () { BtnClick(btnResume); });
        btnSetting.onClick.AddListener(delegate () { BtnClick(btnSetting); });
        btnHome.onClick.AddListener(delegate () { BtnClick(btnHome); });
    }

    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnExit()
    {
        base.OnExit();
        btnRestart.onClick.RemoveAllListeners();
        btnResume.onClick.RemoveAllListeners();
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
            case "btnResume":
                UIManager.Instance.PopPanel();
                GameController.Instance.isPausing = false;
                GameRoot.Instance.evt.CallEvent(GameEventDefine.GAME_RESUME, null);
                break;
            case "btnSetting":
                UIManager.Instance.PushPanel(Panel_ID.SettingPanel);
                break;
            case "btnHome":
                LoadManager.Load(SceneName.StartScene);
                GameRoot.Instance.evt.CallEvent(GameEventDefine.GAME_RESUME, null);
                break;
        }
    }
}
