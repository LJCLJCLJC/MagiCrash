using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    StaticItemVo staticVo;
    PlayerData player;
    bool eated = false;
    public void Create(StaticItemVo staticVo)
    {
        this.staticVo = staticVo;
        player = GameRoot.Instance.GetNowPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBody" && eated == false)
        {
            eated = true;
            switch (staticVo.affect)
            {
                case 0:
                    if (staticVo.addType == 0)
                    {
                        player.nowHealth += (int)staticVo.value;
                        if (player.nowHealth > player.maxHealth)
                        {
                            player.nowHealth = player.maxHealth;
                        }
                        //todo callevent PlayerHpChange
                    }
                    break;
                case 1:
                    if (!DataManager.Instance.GetOwnedItem(player).Contains(3))
                    {
                        if (staticVo.addType == 0)
                        { 

                            player.maxHealth += (int)staticVo.value;
                            player.nowHealth = player.maxHealth;
                            //todo callevent PlayerMaxHpChange
                            player.ownedItem += "3|";
                        }
                    }
                    break;
                case 2:
                    if (!DataManager.Instance.GetOwnedItem(player).Contains(4))
                    {
                        if (staticVo.addType == 0)
                        {
                            player.defence += (int)staticVo.value;
                            player.nowHealth = player.maxHealth;
                            player.ownedItem += "4|";
                        }
                    }
                    break;
                case 3:
                    if (!DataManager.Instance.GetOwnedItem(player).Contains(5))
                    {
                        if (staticVo.addType == 0)
                        {
                            player.powerPlus += (int)staticVo.value;
                            player.nowHealth = player.maxHealth;
                            player.ownedItem += "5|";
                        }
                    }
                    break;
                case 4:
                    if (!DataManager.Instance.GetOwnedItem(player).Contains(6))
                    {
                        if (staticVo.addType == 1)
                        {
                            player.shootSpeedPlus -= staticVo.value;
                            player.nowHealth = player.maxHealth;
                            player.ownedItem += "6|";
                        }
                    }
                    break;
            }
            GameRoot.Instance.evt.CallEvent(GameEventDefine.PLAYER_DAMAGE, null);
            Tools.CreateGameObject("Effects/ItemEffects/"+staticVo.effect, other.transform.root.GetComponent<Player>().tsBody, Vector3.zero, Vector3.one);
            Destroy(gameObject);
        }
    }
}
