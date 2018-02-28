using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel {

    public Button btnTest;
    public Button btnCreateCube;
    public Button btnSave;
    public Button btnLoad;
    public Dropdown dpChangeWeapen;
    private PlayerData player;
    private void Start()
    {

        btnTest.onClick.AddListener(delegate() { OnClick(btnTest); });
        btnCreateCube.onClick.AddListener(delegate () { OnClick(btnCreateCube); });
        btnSave.onClick.AddListener(delegate () { OnClick(btnSave); });
        btnLoad.onClick.AddListener(delegate () { OnClick(btnLoad); });
        dpChangeWeapen.onValueChanged.AddListener(OnValueChanged);
    }
    private void OnClick(Button btn)
    {
        switch (btn.gameObject.name)
        {
            case "btnSave":
                player = new PlayerData();
                player.id = 0;
                player.open = true;
                player.name = "111";
                player.weapons = "0|1|2";
                player.clearedEnemyGroup = "";
                DataManager.Instance.Save(player);
                break;
            case "btnLoad":            
                player = DataManager.Instance.GetPlayer(0);
                Debug.Log(player.name);
                break;
        }

    }
    private void OnValueChanged(int index)
    {
        switch (index)
        {
            case 0:
                GameRoot.Instance.evt.CallEvent(GameEventDefine.CHANGE_WEAPON, 1);
                break;
            case 1:
                GameRoot.Instance.evt.CallEvent(GameEventDefine.CHANGE_WEAPON, 2);
                break;
        }
    }
    private void Test(object obj)
    {
        Debug.Log("Test");
    }

    private void OnDestroy()
    {

    }
}
