using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayerPanel : BasePanel
{
    public PlayerCell[] cells;

    private new void Start()
    {
        base.Start();
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Init();
    }
    public override void OnPause()
    {
        base.OnPause();
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].gameObject.SetActive(false);
        }
    }
    public override void OnResume()
    {
        base.OnResume();
        Init();
    }
    public override void OnExit()
    {
        base.OnExit();
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].gameObject.SetActive(false);
        }
        GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, 0);
    }
    private void Init()
    {
        for (int i= 0; i < cells.Length; i++)
        {
            PlayerData pd = DataManager.Instance.GetPlayer(i);
            cells[i].Create(pd);
            cells[i].gameObject.SetActive(true);
        }
    }
}
