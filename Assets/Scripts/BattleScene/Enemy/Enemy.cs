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
    private StepTrigger[] step;
    public AudioSource audio;
    public StaticEnemyVo enemyVo;
    public SphereCollider radius;
    public AnimalAIControl AI;
    public Animal animal;
    protected List<EnemyWeapon> weapon=new List<EnemyWeapon>();
    protected Transform originTrans;
    protected bool shoot;
    protected Vector3 shootDirect =new Vector3();
    protected GameObject spawn;
    protected float shootBegin = 0;
    protected int nowHealth = 0;
    protected int groupID = -1;
    protected List<Transform> waypointList;
    protected bool dead = false;

    public virtual void Create(StaticEnemyVo enemyVo,int groupId, List<Transform> list)
    {
        step = GetComponentsInChildren<StepTrigger>();
        if (step != null)
        {
            for (int i = 0; i < step.Length; i++)
            {
                step[i].volume = DataManager.Instance.GetSettingData().effectVolume;
            }
        }
        audio.volume = DataManager.Instance.GetSettingData().effectVolume;
        this.enemyVo = enemyVo;
        if (radius != null)
        {
            radius.radius = enemyVo.radius;
        }
        animal = GetComponent<Animal>();
        waypointList = list;
        originTrans = waypointList[0];
        for(int i = 0; i < enemyVo.weapon.Count; i++)
        {
            spawn = Tools.CreateGameObject("Models/Weapon/" + StaticDataPool.Instance.staticEnemyWeaponPool.GetStaticDataVo(enemyVo.weapon[i]).path);
            weapon.Add(spawn.GetComponent<EnemyWeapon>());
            weapon[i].Create(enemyVo.weapon[i], transform);
        }
        nowHealth = enemyVo.health;
        groupID = groupId;
        GameRoot.Instance.evt.AddListener(GameEventDefine.SET_EFFECT_VOLUME, OnSetEffectVolume);
    }
    private void OnSetEffectVolume(object obj)
    {
        if (step != null)
        {
            for (int i = 0; i < step.Length; i++)
            {
                step[i].volume = DataManager.Instance.GetSettingData().effectVolume;
            }
        }
        audio.volume = DataManager.Instance.GetSettingData().effectVolume;
    }
    public virtual void Hurt(int i)
    {
        if (!GameController.Instance.isPausing)
        {
            if (dead == true) return;
            nowHealth -= i;
            //animal.Damaged = true;
            if (nowHealth <= 0 && !animal.Death)
            {
                Die();
            }
        }
    }
    protected virtual void Die()
    {
        AI.target = null;
        animal.Death = true;
        shoot = false;
        dead = true;
        for(int i = 0; i < weapon.Count; i++)
        {
            weapon[i].DestroyObj();
        }

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
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.SET_EFFECT_VOLUME, OnSetEffectVolume);
    }
}
