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
        SetSpeed(0, 0);
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
        if ((nowHealth <= enemyVo.health*2 / 3)&& (nowHealth >= enemyVo.health / 3)&&state!=2)
        {
            State_2();
        }
        else if(nowHealth <= enemyVo.health / 3&nowHealth>0&&state!=3)
        {
            State_3();
        }
        if (nowHealth <= 0)
        {
            Die();
        }
    }
    private void InBossBattle(object obj)
    {
        active = true;
        AI.target = wayPointStart[0];
        gameObject.GetComponent<Animator>().SetBool(HashIDsAnimal.standHash, false);
        weapon[0].shooting = true;
        weapon[1].shooting = false;
    }
    private void State_2()
    {
        state = 2;
        AI.target = wayPointStart[1];
        shield.SetActive(true);
        weapon[0].gameObject.SetActive(false);
        weapon[1].gameObject.SetActive(true);
        weapon[0].shooting = false;
        weapon[1].shooting = true;

    }
    private void State_3()
    {
        state = 3;
        //AI.target = wayPointStart[1];
        weapon[0].gameObject.SetActive(true);
        weapon[0].shooting = true;
        weapon[1].shooting = true;
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.BOSS_BATTLE, InBossBattle);


    }
}
