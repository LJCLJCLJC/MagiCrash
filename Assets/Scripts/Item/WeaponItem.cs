using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour {

    public int id = 0;

    private void Start()
    {
        if (DataManager.Instance.GetWeaponsId(GameRoot.Instance.GetNowPlayer()).Contains(id))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBody")
        {
            GetComponent<Collider>().enabled = false;
            GetWeapon();
            gameObject.SetActive(false);
        }
    }
    private void GetWeapon()
    {
        if (!DataManager.Instance.GetWeaponsId(GameRoot.Instance.GetNowPlayer()).Contains(id))
        {
            GameRoot.Instance.GetNowPlayer().weapons += ("|" + id);
            GameRoot.Instance.evt.CallEvent(GameEventDefine.GET_WEAPON, id);
        }
    }
}
