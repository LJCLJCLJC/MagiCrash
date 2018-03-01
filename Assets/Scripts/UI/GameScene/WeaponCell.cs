using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCell : MonoBehaviour {

    public Button weapon1;
    public Button weapon2;
    public GameObject select;
    private PlayerData player;

    void Start () {
        player = GameRoot.Instance.GetNowPlayer();
        GameRoot.Instance.evt.AddListener(GameEventDefine.GET_WEAPON, OnWeaponUpdate);
        GameRoot.Instance.evt.AddListener(GameEventDefine.CHANGE_WEAPON, OnWeaponUpdate);
        weapon1.onClick.AddListener(delegate () { BtnClick(weapon1); });
        weapon2.onClick.AddListener(delegate () { BtnClick(weapon2); });
        OnWeaponUpdate(1);
    }
    private void OnWeaponUpdate(object obj)
    {
        int id = (int)obj;
        List<int> weapons = DataManager.Instance.GetWeaponsId(player);
        if (weapons.Contains(1))
        {
            weapon1.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            weapon1.transform.parent.gameObject.SetActive(false);
        }
        if (weapons.Contains(2))
        {
            weapon2.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            weapon2.transform.parent.gameObject.SetActive(false);
        }
        switch (id)
        {
            case 1:
                select.transform.localPosition = weapon1.transform.parent.localPosition;
                break;
            case 2:
                select.transform.localPosition = weapon2.transform.parent.localPosition;
                break;
        }
    }
    private void BtnClick(Button btn)
    {
        switch (btn.name)
        {
            case "weapon1":
                GameRoot.Instance.evt.CallEvent(GameEventDefine.CHANGE_WEAPON, 1);
                break;
            case "weapon2":
                GameRoot.Instance.evt.CallEvent(GameEventDefine.CHANGE_WEAPON, 2);
                break;
        }
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.GET_WEAPON, OnWeaponUpdate);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.CHANGE_WEAPON, OnWeaponUpdate);
    }
}
