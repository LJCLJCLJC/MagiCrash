using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ConsolePanel : BasePanel {

    public InputField input;
    public Text tip;

    public override void OnEnter()
    {
        base.OnEnter();
        input.onValueChanged.AddListener(OnSubmit);
        input.ActivateInputField();
        input.gameObject.SetActive(true);
        tip.gameObject.SetActive(false);
        input.text = "";
    }

    private void OnSubmit(string value)
    {
        if (value.Contains("\n"))
        {
            switch (value)
            {
                case "unstoppable\n":
                    GameController.Instance.UnStoppable = true;
                    ShowTip();
                    break;
                case "normal\n":
                    GameController.Instance.UnStoppable = false;
                    ShowTip();
                    break;
                case "blind\n":
                    GameRoot.Instance.GetNowPlayer().tipList += "8|9|10|";
                    ShowTip();
                    break;
                case "warp to key1\n":
                    GameController.Instance.tsPlayer.position = new Vector3(131.59f, 12.39f, -91.66f); 
                    ShowTip();
                    break;
                case "warp to key2\n":
                    GameController.Instance.tsPlayer.position = new Vector3(451.77f, 6.82f, -150.86f);
                    ShowTip();
                    break;
                case "warp to key3\n":
                    GameController.Instance.tsPlayer.position = new Vector3(498.44f, 8.8f, 383.31f);
                    ShowTip();
                    break;
                case "warp to weapon02\n":
                    GameController.Instance.tsPlayer.position = new Vector3(302.41f, 1.22f, -219.92f);
                    ShowTip();
                    break;
                case "warp to boss\n":
                    GameController.Instance.tsPlayer.position = new Vector3(253.7908f,85.7655f,262.0953f);
                    ShowTip();
                    break;
            }
            input.text = "";
        }
    }
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;

    }
    public override void OnExit()
    {
        base.OnExit();
        input.onValueChanged.RemoveAllListeners();
    }
    private void ShowTip()
    {
        input.gameObject.SetActive(false);
        tip.gameObject.SetActive(true);
        tip.DOFade(0, 2).OnComplete<Tween>(delegate() {
            tip.color=new Color(tip.color.r, tip.color.g, tip.color.b,255);
            tip.gameObject.SetActive(false);
            UIManager.Instance.PopPanel();
        });
    }
}
