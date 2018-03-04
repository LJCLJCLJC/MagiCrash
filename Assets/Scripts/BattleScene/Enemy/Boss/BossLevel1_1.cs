using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;

public class BossLevel1_1 : Enemy {

    private int weaponIndex = 0;
    public GameObject shield;
    private Transform[] wayPointStart;
    private bool active = false;
    private int state = 1;
    public override void Create(StaticEnemyVo enemyVo, Transform origin, int groupId, List<Transform> list)
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.BOSS_BATTLE, InBossBattle);
        base.Create(enemyVo, origin, groupId, list);
        SetSpeed(5, 6);
        shield.SetActive(false);
        weapon[1].gameObject.SetActive(false);
        wayPointStart = GameController.Instance.BossWayPoint;
    }

    private void Update()
    {
        if (animal.Death == false && GameRoot.Instance.CanMove && active == true)
        {
            for (; weaponIndex < weapon.Count; weaponIndex++)
            {
                weapon[weaponIndex].cb();
            }
            weaponIndex = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
       
    }


    public override void Hurt(int i)
    {
        base.Hurt(i);
        if ((nowHealth <= enemyVo.health*2 / 3)&& (nowHealth >= enemyVo.health / 3))
        {
            State_1();
        }
        else if(nowHealth <= enemyVo.health / 3)
        {
            State_2();
        }
    }
    private void InBossBattle(object obj)
    {
        active = true;
        AI.target = wayPointStart[0];
    }

    private void State_1()
    {
        AI.target = wayPointStart[1];
        shield.SetActive(true);
        weapon[0].gameObject.SetActive(false);
        weapon[1].gameObject.SetActive(true);
    }
    private void State_2()
    {
        AI.target = wayPointStart[2];
        weapon[0].gameObject.SetActive(true);
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.BOSS_BATTLE, InBossBattle);


    }
}
