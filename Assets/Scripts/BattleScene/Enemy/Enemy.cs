using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;

public enum EnemyOriginState
{
    SLEEPING,
    WALKING,
    IDLE,
    BOSS
}

public class Enemy : MonoBehaviour
{
    public StaticEnemyVo enemyVo;
    public SphereCollider radius;
    public AnimalAIControl AI;
    public Animal animal;
    protected EnemyWeapon weapon;
    protected Transform originTrans;
    protected bool shoot;
    protected Vector3 shootDirect =new Vector3();
    protected GameObject spawn;
    protected float shootBegin = 0;
    protected int nowHealth = 0;
    protected int groupID = -1;
    protected List<Transform> waypointList;

    public virtual void Create(StaticEnemyVo enemyVo,Transform origin,int groupId, List<Transform> list)
    {
        this.enemyVo = enemyVo;
        radius.radius = enemyVo.radius;
        animal = GetComponent<Animal>();
        waypointList = list;
        originTrans = waypointList[0];
        for(int i = 0; i < enemyVo.weapon.Count; i++)
        {
            spawn = Tools.CreateGameObject("Models/Weapon/" + StaticDataPool.Instance.staticEnemyWeaponPool.GetStaticDataVo(enemyVo.weapon[i]).path);
            weapon = spawn.GetComponent<EnemyWeapon>();
            weapon.Create(enemyVo.weapon[i], transform);
        }
        nowHealth = enemyVo.health;
        groupID = groupId;
       
    }
    public virtual void Hurt(int i)
    {
        Debug.Log("damage");
        nowHealth -= i;
        //animal.Damaged = true;
        if (nowHealth <= 0 && !animal.Death)
        {
            Die();
        }

    }
    protected void Die()
    {
        animal.Death = true;
        shoot = false;
        weapon.DestroyObj();
        TimeLine.GetInstance().AddTimeEvent(DestroyObj, 5f, null, gameObject);
    }
    protected void DestroyObj(object obj)
    {
        GameObject effect = Tools.CreateGameObject("Effects/zibao", transform.parent, transform.position, Vector3.one);
        Destroy(gameObject);
        ShowItem();
        GameRoot.Instance.evt.CallEvent(GameEventDefine.ENEMY_DIE, groupID);
    }
    protected void ShowItem()
    {
        if (enemyVo.item != 0)
        {
            StaticItemVo itemVo = StaticDataPool.Instance.staticItemPool.GetStaticDataVo(enemyVo.item);
            GameObject itemObj = Tools.CreateGameObject("Models/Items/" + itemVo.path, transform.parent, transform.position, Vector3.one);

            itemObj.GetComponent<Item>().Create(itemVo);
        }
    }
    protected void SetSpeed(float trot, float run)
    {
        AI.ToTrot = trot;
        AI.ToRun = run;
    }
    private void OnDestroy()
    {
        TimeLine.GetInstance().RemoveTimeEvent(DestroyObj);
    }
}
