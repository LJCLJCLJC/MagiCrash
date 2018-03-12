using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float turningSmooth = 5f;
    public float rayLength = 100f;
    public Transform shootSpwan;
    public Transform player;
    public Transform mousePoint;

    private PlayerData nowPlayer;
    private List<StaticWeaponVo> weaponList = new List<StaticWeaponVo>();
    private List<Weapon> weapons;
    private int nowWeapon;
    private Weapon shotWeapon;
    private int floorMask;
    private float shotBegin = 0;

    private bool canMove = true;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Default");
    }
    private void Start()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.LOAD_GAME, OnUpdate);
        GameRoot.Instance.evt.AddListener(GameEventDefine.CHANGE_WEAPON, SetWeapon);//界面按钮点击切换武器
        GameRoot.Instance.evt.AddListener(GameEventDefine.GET_WEAPON, GetWeapon);//获得新武器
        OnUpdate(null);
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && nowWeapon != 0 && GameRoot.Instance.CanMove)
        {
            if (shotBegin > shotWeapon.weaponVo.chargeTime * nowPlayer.shootSpeedPlus)
            {
                shotWeapon.Shot();
                shotBegin = 0;
            }
            shotBegin += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Alpha1) && weaponList.Contains(StaticDataPool.Instance.staticWeaponPool.GetStaticDataVo(1)))
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.CHANGE_WEAPON, 1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) && weaponList.Contains(StaticDataPool.Instance.staticWeaponPool.GetStaticDataVo(2)))
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.CHANGE_WEAPON, 2);
        }
    }

    private void FixedUpdate()
    {
        if (GameRoot.Instance.CanMove)
        {
            TuringSpawn();
        }
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.LOAD_GAME, OnUpdate);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.CHANGE_WEAPON, SetWeapon);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.GET_WEAPON, GetWeapon);
    }

    private void OnUpdate(object obj)
    {
        Init();
    }

    private void Init()
    {
        Tools.ClearChildFromParent(shootSpwan);
        nowPlayer = GameRoot.Instance.GetNowPlayer();
        weapons = new List<Weapon>();
        weaponList = DataManager.Instance.GetWeapons(nowPlayer);
        if (weaponList.Count == 1)
        {
            nowWeapon = weaponList[0].id;
        }
        else
        {
            nowWeapon = weaponList[1].id;
        }
        if (nowWeapon == 0)
        {
            return;
        }
        for (int i = 1; i < weaponList.Count; i++)
        {
            Weapon weapon = Tools.CreateGameObject("Models/Weapon/" + weaponList[i].path, shootSpwan, Vector3.zero, Vector3.one).GetComponent<Weapon>();
            weapon.gameObject.SetActive(false);
            weapon.Create(weaponList[i].id);
            if (weapon.weaponVo.id == nowWeapon)
            {
                weapon.nowWeapon = true;
                shotWeapon = weapon;
            }
            else
            {
                weapon.nowWeapon = false;
            }
            weapons.Add(weapon);
        }
        ChangeWeapon();
    }
    private void SetWeapon(object obj)
    {
        int id = (int)obj;
        nowWeapon = id;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].weaponVo.id == nowWeapon)
            {
                weapons[i].nowWeapon = true;
                shotWeapon = weapons[i];
            }
            else
            {
                weapons[i].nowWeapon = false;
            }
        }
        ChangeWeapon();
    }
    private void ChangeWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].nowWeapon)
            {
                weapons[i].Show();
                shotBegin = weapons[i].weaponVo.chargeTime;
                GameRoot.Instance.evt.CallEvent(GameEventDefine.CHANGE_ANTLER_MAGIC, weapons[i].weaponVo.color);
            }
            else
            {
                weapons[i].Hide();
            }
        }

    }
    private void GetWeapon(object obj)
    {
        int id = (int)obj;
        Init();
        SetWeapon(id);
    }
    private void TuringSpawn()
    {
        if (weaponList[nowWeapon].bulletId == 3)
        {
            transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x, player.position.y - 1f, player.position.z);
        }
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, rayLength, floorMask))
        {
            Vector3 target = floorHit.point;
            if (weaponList[nowWeapon].bulletId == 3)
            {
                target.y = player.position.y;
            }
            else
            {
                target.y = player.position.y - 1f;
            }

            transform.LookAt(target);
            if (mousePoint != null)
            {
                mousePoint.position = Vector3.Lerp(mousePoint.position, floorHit.point, 0.2f);

            }
        }
    }


}
