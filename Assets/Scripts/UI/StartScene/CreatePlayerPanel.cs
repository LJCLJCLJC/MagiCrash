using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MalbersAnimations.Utilities;
using DG.Tweening;

public class CreatePlayerPanel : BasePanel
{
    public GameObject[] windows;
    public Button btnUp;
    public Button btnNext;
    public Button btnFinish;
    public InputField inputName;
    public Dropdown dropSkin;
    public Dropdown dropAntler;
    public Dropdown dropSpot;
    public Dropdown dropColor;  

    private int cellIndex;
    private GameObject deer;
    private ActiveMeshes mesh;
    private MaterialChanger material;
    int index;
    private new void Start()
    {
        base.Start();
        btnUp.onClick.AddListener(delegate () { BtnClick(btnUp); });
        btnNext.onClick.AddListener(delegate () { BtnClick(btnNext); });
        btnFinish.onClick.AddListener(delegate () { BtnClick(btnFinish); });
        dropSkin.onValueChanged.AddListener(OnSkinChanged);
        dropAntler.onValueChanged.AddListener(OnAntlerChanged);
        dropSpot.onValueChanged.AddListener(OnSpotChanged);
    }
    public override void OnEnter(object param)
    {
        base.OnEnter(param);
        Init(param);
    }
    public override void OnExit()
    {
        base.OnExit();
        GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, 0);
    }

    private void Init(object param)
    {
        cellIndex = (int)param;
        index = 0;
        btnUp.gameObject.SetActive(false);
        btnNext.gameObject.SetActive(true);
        btnFinish.gameObject.SetActive(false);
        windows[0].SetActive(true);
        for(int i = 1; i < 5; i++)
        {
            windows[i].SetActive(false);
        }
        deer = GameObject.FindGameObjectWithTag("Player");
        mesh = deer.GetComponent<ActiveMeshes>();
        material = deer.GetComponent<MaterialChanger>();
    }
    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "btnUp":
                index--;
                if (index !=4)
                {
                    btnNext.gameObject.SetActive(true);
                }
                if (index == 0)
                {
                    btnUp.gameObject.SetActive(false);
                }
                btnFinish.gameObject.SetActive(false);
                windows[index].SetActive(true);
                windows[index + 1].SetActive(false);

                break;
            case "btnNext":
                index++;
                btnUp.gameObject.SetActive(true);
                if (index == 4)
                {
                    btnNext.gameObject.SetActive(false);
                    btnFinish.gameObject.SetActive(true);
                }
                windows[index].SetActive(true);
                windows[index - 1].SetActive(false);
                break;
            case "btnFinish":
                UIManager.Instance.CreateConfirmPanel("是否用这个角色开始游戏？", CreateFinished);
                break;
        }
        if (index == 1||index == 3)
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, 1);
        }
        else if (index == 2)
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, 2);
        }
        else
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.MOVE_CAMERA, 0);
        }
    }

    private void OnSkinChanged(int index)
    {
        material.SetMaterial(0, index);
    }
    private void OnAntlerChanged(int index)
    {
        mesh.ChangeMesh(0, index);
    }
    private void OnSpotChanged(int index)
    {
        mesh.ChangeMesh(2, index);
    }

    private void CreateFinished(object obj)
    {
        PlayerData player = new PlayerData();
        player.id = cellIndex;
        player.name = inputName.text == "" ? "Player": inputName.text ;
        player.open = true;
        player.nowHealth = 16;
        player.maxHealth = 16;
        player.defence = 0;
        player.shootSpeedPlus = 1.0f;
        player.powerPlus = 0;
        player.weapons = "0";
        player.clearedEnemyGroup = "";
        player.nowLevel = SceneName.Level1_1;
        player.skin = dropSkin.value;
        player.antler = dropAntler.value;
        player.spot = dropSpot.value;
        player.color = dropColor.value;
        player.startPosition = "48#1#22";
        player.hasKey = "Level1_1";
        player.guideList = "";
        player.tipList = "";
        player.ownedItem = "";
        player.showMapPoint = false;
        DataManager.Instance.Save(player);
        GameRoot.Instance.SetNowPlayer(cellIndex);
        LoadManager.Load(SceneName.Level1_1);
        GameRoot.Instance.evt.CallEvent(GameEventDefine.INIT_DATA, null);
        GameRoot.Instance.evt.CallEvent(GameEventDefine.GAME_RESUME, null);
    }
}
