using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;

public class BossLevel1_1 : Enemy {
    private bool active = false;
    public override void Create(StaticEnemyVo enemyVo, Transform origin, int groupId, List<Transform> list)
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.BOSS_BATTLE, InBossBattle);
        base.Create(enemyVo, origin, groupId, list);
        SetSpeed(5, 6);
    }

    private void Update()
    {
        if (animal.Death == false && GameRoot.Instance.CanMove && active == true)
        {
            weapon.cb();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
       
    }

    private void InBossBattle(object obj)
    {
        active = true;
        AI.target = waypointList[0];
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.BOSS_BATTLE, InBossBattle);


    }
}
