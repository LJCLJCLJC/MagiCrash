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
            btnDelete.gameObject.SetActive(false);
            head.gameObject.SetActive(false);
            playerName.gameObject.SetActive(false);
            btnGo.gameObject.SetActive(false);
            return;
        }
        if (player.open == false)
        {
            btnAdd.gameObject.SetActive(true);
            btnDelete.gameObject.SetActive(false);
            head.gameObject.SetActive(false);
            playerName.gameObject.SetActive(false);
            btnGo.gameObject.SetActive(false);
            return;
        }
        else
        {
            btnAdd.gameObject.SetActive(false);
            btnDelete.gameObject.SetActive(true);
            head.gameObject.SetActive(true);
            playerName.gameObject.SetActive(true);
            btnGo.gameObject.SetActive(true);
        }
        playerName.text = player.name;
        
    }

    private void BtnClick(Button button)
    {
        switch (button.name)
        {
            case "btnAdd":
                UIManager.Instance.PushPanel(Panel_ID.CreatePlayerPanel,cellIndex);
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
                            UIManager.Instance.CreateConfirmPanel("那好吧再见！", delegate (object obj4) {
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
