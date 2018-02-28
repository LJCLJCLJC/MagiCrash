using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyWeapon : MonoBehaviour {
    public delegate void WeaponTypeCallBack();
    public bool shooting = false;
    public bool moving = false;
    public StaticEnemyWeaponVo weaponVo;
    public WeaponTypeCallBack cb;
    private Transform owner;
    private GameObject player;
    private float shootBegin;
    private Vector3 direction;
    private ObjectPool bulletsPool;
    private ObjectPool effectPool;
    private GameObject bulletTrans;

    public void Create(int id,Transform owner)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        this.owner = owner;
        transform.position = owner.position + new Vector3(1, 1, 1);
        weaponVo = StaticDataPool.Instance.staticEnemyWeaponPool.GetStaticDataVo(id);
        switch (weaponVo.type)
        {
            case 0:
                break;
            case 1:
                cb = FollowedWeapon;
                break;
            case 2:
                GameObject weaponSpawn = new GameObject("weaponSpwan");
                weaponSpawn.transform.position = owner.position;
                weaponSpawn.transform.rotation = Quaternion.Euler(Vector3.zero);
                transform.parent = weaponSpawn.transform;
                transform.localPosition =new Vector3(0.5f, 0.5f, 0.5f);
                cb = RollWeapon;
                break;
        }
        shootBegin = 0;
        string path = StaticDataPool.Instance.staticBulletPool.GetStaticDataVo(weaponVo.bulletId).path;
        GameObject bullet = Resources.Load("Models/Bullets/" + path) as GameObject;
        bulletTrans = new GameObject("bulletTrans");
        bulletsPool = new ObjectPool(bullet, bulletTrans.transform, 1);

        GameObject effect = Resources.Load("Effects/Explosion/" + StaticDataPool.Instance.staticBulletPool.GetStaticDataVo(weaponVo.bulletId).effect) as GameObject;
        effectPool = new ObjectPool(effect, bulletTrans.transform, 1);
    }

    private void Shot()
    {
        int damage = weaponVo.damage;
        bulletsPool.New().GetComponent<Bullets>().Fly(weaponVo.speed, transform.rotation, transform.position, damage, false,weaponVo.bulletId,effectPool);
    }
    private void FollowedWeapon()//永远指向玩家的武器
    {
        
        if (shootBegin > weaponVo.chargeTime)
        {
            direction = (player.transform.position-owner.position).normalized;
            transform.DOMove(new Vector3(direction.x * 3, 1f, direction.z * 3) + owner.position, 1);
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            shootBegin = 0;
            if (shooting == true)
            {
                Shot();
            }

        }
        shootBegin += Time.deltaTime;
    }
    private void RollWeapon()//按照一定角速度运动的武器
    {
        transform.parent.position = owner.position;
        if (shooting)
        {
            transform.parent.Rotate(transform.parent.up, 180 * Time.deltaTime);
            if (shootBegin > weaponVo.chargeTime)
            {
                shootBegin = 0;
                Shot();

            }
        }
        shootBegin += Time.deltaTime;
    }

    public void DestroyObj()
    {
        bulletsPool.Clear();
        Destroy(bulletTrans.gameObject);
        Destroy(gameObject);
    }
}
