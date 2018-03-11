using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelClearPanel : BasePanel {

    public Button btnSetting;
    public Button btnHome;
    public override void OnEnter()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 0.5f);
        gameObject.SetActive(true);
        btnSetting.onClick.AddListener(delegate () { BtnClick(btnSetting); });
        btnHome.onClick.AddListener(delegate () { BtnClick(btnHome); });
    }

    public override void OnExit()
    {
        base.OnExit();
        btnSetting.onClick.RemoveAllListeners();
        btnHome.onClick.RemoveAllListeners();
    }
    private void BtnClick(Button button)
    {
        switch (button.name)
        {
            case "btnSetting":
                break;
            case "btnHome":
                LoadManager.Load(SceneName.StartScene);
                GameRoot.Instance.evt.CallEvent(GameEventDefine.GAME_RESUME, null);
                break;
        }
    }
}
