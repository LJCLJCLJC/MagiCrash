using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullets : PoolItem
{
    public Rigidbody rigidbody;
    public AudioSource audio;
    private int damage = 0;
    private bool owner = false;//true(player) false(enemy)
    private float flySpeed;
    private StaticBulletVo staticBulletVo;
    private ObjectPool effectPool;
    private bool addForce = false;
    private Vector3 speed = new Vector3();
    private int id;

    public override void Init()
    {
        base.Init();
        GameRoot.Instance.evt.AddListener(GameEventDefine.GAME_PAUSE, OnPause);
        GameRoot.Instance.evt.AddListener(GameEventDefine.GAME_RESUME, OnResume);
        audio.volume = DataManager.Instance.GetSettingData().effectVolume;
    }
    private void OnPause(object obj)
    {

    }
    private void OnResume(object obj)
    {

    }
    public override void ResetItem()
    {
        base.ResetItem();
        addForce = false;
    }
    public override void Hide()
    {
        base.Hide();
        rigidbody.velocity = Vector3.zero;
        TimeLine.GetInstance().RemoveTimeEvent(HideByTime);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.GAME_PAUSE, OnPause);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.GAME_RESUME, OnResume);
    }
    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        if (transform.position.y <= 0 && addForce == false && staticBulletVo.id == 3)
        {
            rigidbody.velocity += Vector3.up * 2f;
        }
    }
    public void Fly(float flySpeed, Quaternion quaternion, Vector3 pos, int damage, bool owner, int id, ObjectPool objectPool = null)
    {
        this.id = id;
        transform.position = pos;
        transform.rotation = quaternion;
        this.flySpeed = flySpeed;
        //rigidbody.velocity =(transform.forward.normalized*flySpeed);
        this.damage = damage;
        this.owner = owner;
        if (objectPool != null)
        {
            effectPool = objectPool;
        }
        staticBulletVo = StaticDataPool.Instance.staticBulletPool.GetStaticDataVo(id);
        switch (id)
        {
            case 1: Type_1(); break;
            case 2: Type_1(); break;
            case 3: Type_3(); break;
            case 4: Type_1(); break;
        }
        TimeLine.GetInstance().AddTimeEvent(HideByTime, staticBulletVo.destroyTime, null, gameObject);
    }
    private void Type_1()
    {
        rigidbody.velocity = (transform.forward.normalized * flySpeed);
    }
    private void Type_3()
    {
        rigidbody.velocity = (transform.forward.normalized * flySpeed);
        rigidbody.useGravity = true;
    }
    private void HideByTime(object obj)
    {
        Hide();
        if (effectPool != null)
        {
            effectPool.New().GetComponent<BulletEffect>().Create(transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall"|| other.tag == "Ground")
        {
            if (staticBulletVo.id == 3)
            {
                rigidbody.velocity += Vector3.up * 14f;
                return;
            }
            Hide();
            if (effectPool != null)
            {
                effectPool.New().GetComponent<BulletEffect>().Create(transform.position);
            }
        }
        else if (other.tag == "EnemyBody" && owner == true)
        {
            Hide();
            other.transform.root.GetComponentInParent<Enemy>().Hurt(damage);
            if (effectPool != null)
            {
                effectPool.New().GetComponent<BulletEffect>().Create(transform.position);
            }
        }
        else if (other.tag == "PlayerBody" && owner == false)
        {
            Hide();
            other.transform.root.GetComponent<Player>().Hurt(damage);
            if (effectPool != null)
            {
                effectPool.New().GetComponent<BulletEffect>().Create(transform.position);
            }
        }
        else if (other.tag == "StoneGate" && owner == true)
        {
            Hide();
            other.transform.parent.GetComponent<StoneGate>().Hurt(staticBulletVo.id);
            if (effectPool != null)
            {
                effectPool.New().GetComponent<BulletEffect>().Create(transform.position);
            }
        }
        else if (other.tag == "Shield" && owner == true)
        {
            Hide();
            other.GetComponent<Shield>().Hurt(staticBulletVo.id);
            if (effectPool != null)
            {
                effectPool.New().GetComponent<BulletEffect>().Create(transform.position);
            }
        }
    }
}
