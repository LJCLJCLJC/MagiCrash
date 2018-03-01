using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;

public class EnemyWolf : Enemy {


    public override void Create(StaticEnemyVo enemyVo, Transform origin, int groupId, List<Transform> list)
    {
        base.Create(enemyVo, origin, groupId, list);
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
        SetSpeed(5, 6);
    }

    private void Update()
    {
        if (animal.Death == false && GameRoot.Instance.CanMove)
        {
            weapon.cb();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.MAGIC_CHANGE, true);
            SetSpeed(1, 2);
            AI.target = other.transform;
            weapon.shooting = true;
            if (enemyVo.state == EnemyOriginState.SLEEPING)
            {
                animal.GetComponent<Animator>().SetBool(HashIDsAnimal.standHash, false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.MAGIC_CHANGE, false);
            SetSpeed(1, 2);
            AI.target = originTrans;
            weapon.shooting = false;

        }
    }

    private void GoToSleep(object obj)
    {
        animal.ActionEmotion(6);
        animal.EnableAction(true);
        TimeLine.GetInstance().RemoveTimeEvent(GoToSleep);

    }

}
