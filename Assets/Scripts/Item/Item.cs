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
        if (other.tag == "Player" && eated == false)
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
                        GameRoot.Instance.evt.CallEvent(GameEventDefine.PLAYER_DAMAGE, null);
                    }
                    break;
                case 1:
                    if (staticVo.addType == 0)
                    {
                        player.maxHealth += (int)staticVo.value;
                        //todo callevent PlayerMaxHpChange
                        GameRoot.Instance.evt.CallEvent(GameEventDefine.PLAYER_DAMAGE, null);
                    }
                    break;
                case 2:
                    if (staticVo.addType == 0)
                    {
                        player.defence += (int)staticVo.value;
                    }
                    break;
                case 3:
                    if (staticVo.addType == 0)
                    {
                        player.powerPlus += (int)staticVo.value;
                    }
                    break;
                case 4:
                    if (staticVo.addType == 1)
                    {
                        player.shootSpeedPlus *= staticVo.value;
                    }
                    break;
            }
            Tools.CreateGameObject("Effects/ItemEffects/"+staticVo.effect, other.transform.root.GetComponent<Player>().tsBody, Vector3.zero, Vector3.one);
            Destroy(gameObject);
        }
    }
}
