using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;

public class EnemyWolf : Enemy {


    private int weaponIndex=0;
    public override void Create(StaticEnemyVo enemyVo, int groupId, List<Transform> list)
    {
        base.Create(enemyVo, groupId, list);
        switch (enemyVo.state)
        {
            case EnemyOriginState.SLEEPING:
                TimeLine.GetInstance().AddTimeEvent(GoToSleep, 1, null, gameObject);
                break;
            case EnemyOriginState.WALKING:
                AI.target = waypointList[0];
                break;
            default:
                break;
        }
        SetSpeed(4, 5);
    }
    protected override void Die()
    {
        base.Die();
        GameRoot.Instance.evt.CallEvent(GameEventDefine.OUT_BATTLE, null);
        radius.enabled = false;
    }
    private void Update()
    {
        if (animal.Death == false && GameRoot.Instance.CanMove)
        {
            for(; weaponIndex < weapon.Count; weaponIndex++)
            {
                weapon[weaponIndex].cb();
            }
            weaponIndex = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBody")
        {
            if (!dead)
            {
                GameRoot.Instance.evt.CallEvent(GameEventDefine.IN_BATTLE, null);
            }

            SetSpeed(0, 0);
            AI.target = other.transform;
            for (int i=0; i < weapon.Count; i++)
            {
                weapon[i].shooting = true;
            }
            if (enemyVo.state == EnemyOriginState.SLEEPING)
            {
                animal.Attack1 = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerBody")
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.MAGIC_CHANGE, false);
            if (!dead)
            {
                GameRoot.Instance.evt.CallEvent(GameEventDefine.OUT_BATTLE, null);
            }
            SetSpeed(1, 2);
            AI.target = originTrans;
            for (int i = 0; i < weapon.Count; i++)
            {
                weapon[i].shooting = false;
            }

        }
    }

    private void GoToSleep(object obj)
    {
        animal.ActionEmotion(6);
        animal.EnableAction(true);
        TimeLine.GetInstance().RemoveTimeEvent(GoToSleep);

    }

}
