using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MalbersAnimations.Utilities;

public class PlayerCell : MonoBehaviour
{
    public int cellIndex;
    public Image head;
    public Text playerName;
    public Button btnAdd;
    public Button btnDelete;
    public Button btnGo;
    public GameObject tsCharactor;
    public GameObject tsAttack;
    public GameObject tsDefance;
    public GameObject tsSpeed;
    public GameObject[] keys;
    public Heart[] heart;
    private PlayerData player;
    private ActiveMeshes mesh;
    private MaterialChanger material;
    private void Start()
    {
        btnAdd.onClick.AddListener(delegate () { BtnClick(btnAdd); });
        btnDelete.onClick.AddListener(delegate () { BtnClick(btnDelete); });
        btnGo.onClick.AddListener(delegate () { BtnClick(btnGo); });
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player").transform.root.gameObject;
        mesh = GameObject.FindGameObjectWithTag("Player").GetComponent<ActiveMeshes>();
        material = GameObject.FindGameObjectWithTag("Player").GetComponent<MaterialChanger>();
    }
    public void Create(PlayerData pd)
    {
        player = pd;
        if (player == null)
        {
            btnAdd.gameObject.SetActive(true);
            tsCharactor.SetActive(false);
            return;
        }
        if (player.open == false)
        {
            btnAdd.gameObject.SetActive(true);
            tsCharactor.SetActive(false);
            return;
        }
        else
        {
            btnAdd.gameObject.SetActive(false);
            tsCharactor.SetActive(true);
        }
        playerName.text = player.name;

        if (DataManager.Instance.GetOwnedItem(player).Contains(4)) tsDefance.SetActive(true);
        if (DataManager.Instance.GetOwnedItem(player).Contains(5)) tsAttack.SetActive(true);
        if (DataManager.Instance.GetOwnedItem(player).Contains(6)) tsSpeed.SetActive(true);
        string[] keyStr = player.hasKey.Split('|');
        List<int> hasKey = new List<int>();
        int i = 0;
        for (i = 1; i < keyStr.Length; i++)
        {
            hasKey.Add(int.Parse(keyStr[i]));
        }
        for (i = 0; i < hasKey.Count; i++)
        {
            keys[i].SetActive(true);
        }
        for (i = 0; i < heart.Length; i++)
        {
            heart[i].gameObject.SetActive(false);
        }
        for (i = 0; i < player.maxHealth; i++)
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
                heart[i].Create(player.nowHealth - i * 4);
            }
        }
    }


    private void BtnClick(Button button)
    {
        switch (button.name)
        {
            case "btnAdd":
                UIManager.Instance.PushPanel(Panel_ID.CreatePlayerPanel, cellIndex);
                break;
            case "btnDelete":
                UIManager.Instance.CreateConfirmPanel("你要删除这个存档吗？", delegate (object obj1)
                {
                    UIManager.Instance.PopPanel();
                    UIManager.Instance.CreateConfirmPanel("真的要删除这个存档吗", delegate (object obj2)
                    {
                        UIManager.Instance.PopPanel();
                        UIManager.Instance.CreateConfirmPanel("删除了这个存档就不能还原了！", delegate (object obj3)
                        {
                            UIManager.Instance.PopPanel();
                            UIManager.Instance.CreateConfirmPanel("那好吧再见！", delegate (object obj4)
                            {
                                player.open = false;
                                DataManager.Instance.Save(player);
                                UIManager.Instance.PopPanel();
                            });
                        });
                    });
                });
                break;
            case "btnGo":
                UIManager.Instance.CreateConfirmPanel("用这个角色开始游戏？", GoGame, null, OnCancel);
                material.SetMaterial(0, player.skin);
                mesh.ChangeMesh(0, player.antler);
                mesh.ChangeMesh(2, player.spot);
                GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, 0);
                break;
        }
    }

    private void GoGame(object obj)
    {
        GameRoot.Instance.SetNowPlayer(cellIndex);
        LoadManager.Load(GameRoot.Instance.GetNowPlayer().nowLevel);
        GameRoot.Instance.evt.CallEvent(GameEventDefine.INIT_DATA, null);
        GameRoot.Instance.evt.CallEvent(GameEventDefine.GAME_RESUME, null);

    }
    private void OnCancel(object obj)
    {
        GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, 3);
    }
}
