using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool nowWeapon = false;
    public StaticWeaponVo weaponVo;
    private ObjectPool bulletsPool;
    private GameObject bulletTrans;
    private ObjectPool effectPool;

    public void Create(int id)
    {
        weaponVo = StaticDataPool.Instance.staticWeaponPool.GetStaticDataVo(id);
        string path = StaticDataPool.Instance.staticBulletPool.GetStaticDataVo(weaponVo.bulletId).path;
        GameObject bullet = Resources.Load("Models/Bullets/"+path) as GameObject;
        bulletTrans = new GameObject("bulletTrans");
        bulletsPool = new ObjectPool(bullet, bulletTrans.transform, 1);
        GameObject effect = Resources.Load("Effects/Explosion/" + StaticDataPool.Instance.staticBulletPool.GetStaticDataVo(weaponVo.bulletId).effect) as GameObject;
        effectPool = new ObjectPool(effect, bulletTrans.transform, 1);
    }
    public void Shot()
    {
        Debug.Log("shot");
        int damage = weaponVo.damage + GameRoot.Instance.GetNowPlayer().powerPlus;
        bulletsPool.New().GetComponent<Bullets>().Fly(weaponVo.speed, transform.parent.parent.rotation, transform.position, damage,true,weaponVo.bulletId, effectPool);

    }
    public void Show()
    {
        gameObject.SetActive(true);
        enabled = true;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        enabled = false;
    }
    public void DestroyObj()
    {
        Destroy(bulletTrans);
        Destroy(gameObject);
    }
}
