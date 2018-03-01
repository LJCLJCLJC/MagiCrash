using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamePanel : BasePanel
{

    public Heart[] heart;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (UIManager.Instance.GetTopPanel().panelID != Panel_ID.MapPanel)
            {
                UIManager.Instance.PushPanel(Panel_ID.MapPanel);
            }
            else if (UIManager.Instance.GetTopPanel().panelID == Panel_ID.MapPanel)
            {
                UIManager.Instance.PopPanel();
            }
        }
        
    }
    private PlayerData player;
    private void Start()
    {

    }
    public override void OnEnter()
    {
        base.OnEnter();
        player = GameRoot.Instance.GetNowPlayer();
        for (int i = 0; i < heart.Length; i++)
        {
            heart[i].gameObject.SetActive(false);
        }
        OnHpUpdate(null);
        GameRoot.Instance.evt.AddListener(GameEventDefine.PLAYER_DAMAGE, OnHpUpdate);
        GameRoot.Instance.evt.AddListener(GameEventDefine.SHOW_TIP, OnShowTip);
        
    }
    public override void OnExit()
    {
        base.OnExit();
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.PLAYER_DAMAGE, OnHpUpdate);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.SHOW_TIP, OnShowTip);
        
    }
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 1;
        Debug.Log("OnPause:" + panelID);
    }
    private void OnHpUpdate(object obj)
    {
        int i;
        for(i = 0; i < player.maxHealth; i++)
        {
            if ((i + 1) % 4 == 0)
            {
                heart[(i + 1) / 4 - 1].gameObject.SetActive(true);
                
            }
        }
        for (i = 0; i < heart.Length; i++)
        {
            if (heart[i].gameObject.activeSelf == false)
            {
                break;
            }
            if (player.nowHealth >= (1 + i) * 4)
            {
                heart[i].Create(4);
            }
            else
            {
                heart[i].Create(player.nowHealth-i*4);
            }
        }

    }
    private void OnShowTip(object obj)
    {
        if(UIManager.Instance.GetTopPanel().panelID == Panel_ID.TipPanel)
        {
            UIManager.Instance.GetTopPanel().OnEnter(obj);
        }
        else
        {
            UIManager.Instance.PushPanel(Panel_ID.TipPanel, obj);
        }
        GameController.Instance.showingTip = true;
    }
}
