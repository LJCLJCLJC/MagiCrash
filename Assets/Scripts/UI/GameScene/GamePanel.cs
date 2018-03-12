using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamePanel : BasePanel
{

    public Heart[] heart;
    public GameObject saveIcon;
    public GameObject attackIcon;
    public GameObject defanceIcon;
    public GameObject speedIcon;
    private PlayerData player;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (UIManager.Instance.GetTopPanel().panelID != Panel_ID.MapPanel &&! GameController.Instance.isPausing)
            {
                UIManager.Instance.PushPanel(Panel_ID.MapPanel);
            }
            else if (UIManager.Instance.GetTopPanel().panelID == Panel_ID.MapPanel && !GameController.Instance.isPausing)
            {
                UIManager.Instance.PopPanel();
            }
        }
        if (Input.GetKeyUp(KeyCode.Backslash))
        {
            if (UIManager.Instance.GetTopPanel().panelID != Panel_ID.ConsolePanel && !GameController.Instance.isPausing)
            {
                UIManager.Instance.PushPanel(Panel_ID.ConsolePanel);
            }
            else if (UIManager.Instance.GetTopPanel().panelID == Panel_ID.ConsolePanel && !GameController.Instance.isPausing)
            {
                UIManager.Instance.PopPanel();
            }
        }

    }

    public override void OnEnter()
    {
        base.OnEnter();
        player = GameRoot.Instance.GetNowPlayer();
        for (int i = 0; i < heart.Length; i++)
        {
            heart[i].gameObject.SetActive(false);
        }
        saveIcon.SetActive(false);
        attackIcon.SetActive(false);
        defanceIcon.SetActive(false);
        speedIcon.SetActive(false);
        GameRoot.Instance.evt.AddListener(GameEventDefine.PLAYER_DAMAGE, OnHpUpdate);
        GameRoot.Instance.evt.AddListener(GameEventDefine.SHOW_TIP, OnShowTip);
        GameRoot.Instance.evt.AddListener(GameEventDefine.SAVE_GAME, OnSaveGame);
        OnHpUpdate(null);

    }
    public override void OnExit()
    {
        base.OnExit();
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.PLAYER_DAMAGE, OnHpUpdate);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.SHOW_TIP, OnShowTip);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.SAVE_GAME, OnSaveGame);

    }
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 1;
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

        List<int> item = DataManager.Instance.GetOwnedItem(player);
        if (item.Contains(4))
        {
            defanceIcon.SetActive(true);
        }
        if (item.Contains(5))
        {
            attackIcon.SetActive(true);
        }
        if (item.Contains(6))
        {
            speedIcon.SetActive(true);
        }

    }
    private void OnShowTip(object obj)
    {
        if(UIManager.Instance.GetTopPanel().panelID == Panel_ID.TipPanel)
        {
            UIManager.Instance.PopPanel();
            UIManager.Instance.PushPanel(Panel_ID.TipPanel, obj);
        }
        else
        {
            UIManager.Instance.PushPanel(Panel_ID.TipPanel, obj);
        }
        GameController.Instance.showingTip = true;
    }
    private void OnSaveGame(object obj)
    {
        saveIcon.SetActive(true);
        TimeLine.GetInstance().AddTimeEvent(HideSaveIcon, 1f, null, gameObject);

    }
    private void HideSaveIcon(object obj)
    {
        saveIcon.SetActive(false);
        TimeLine.GetInstance().RemoveTimeEvent(HideSaveIcon);
    }

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.PLAYER_DAMAGE, OnHpUpdate);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.SHOW_TIP, OnShowTip);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.SAVE_GAME, OnSaveGame);
    }
}
