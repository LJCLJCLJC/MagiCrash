using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyVo
{
    public int id;
    public string name;
    public List<int> weapon=new List<int>();
    public float speed;
    public float radius;
    public string path;
    public int health;
    public EnemyOriginState state;
    public int item;

    public StaticEnemyVo(string[] al)
    {
        id = int.Parse(al[0]);
        name = al[1];
        string[] weaponStr = al[2].Split('|');
        for(int i = 0; i < weaponStr.Length; i++)
        {
            weapon.Add(int.Parse(weaponStr[i]));
        }
        speed = float.Parse(al[3]);
        radius = float.Parse(al[4]);
        path = al[5];
        health = int.Parse(al[6]);
        if (al[7].Contains("SLEEPING"))
        {
            state = EnemyOriginState.SLEEPING;
        }
        else if (al[7].Contains("WALKING"))
        {
            state = EnemyOriginState.WALKING;
        }
        else if (al[7].Contains("BOSS"))
        {
            state = EnemyOriginState.BOSS;
        }
        else
        {
            state = EnemyOriginState.IDLE;
        }
        item = int.Parse(al[8]);
    }
}
public class StaticEnemyGroupVo
{
    public int id;
    public List<int> enemies = new List<int>();
    public List<Vector3> unitPos = new List<Vector3>();
    public string level;
    public List<Vector3> wayPointList = new List<Vector3>();

    public StaticEnemyGroupVo(string[] al)
    {
        id = int.Parse(al[0]);
        string[] enemiesStr = al[1].Split('|');
        for(int i = 0; i < enemiesStr.Length; i++)
        {
            enemies.Add(int.Parse(enemiesStr[i]));
        }
        string[] unitPosStr = al[2].Split('|');
        for(int i = 0; i < unitPosStr.Length; i++)
        {
            string[] v3Str = unitPosStr[i].Split('#');
            unitPos.Add(new Vector3(float.Parse(v3Str[0]), float.Parse(v3Str[1]), float.Parse(v3Str[2])));
        }
        level = al[3];
        string[] posStr = al[4].Split('|');
        for (int i = 0; i < posStr.Length; i++)
        {
            string[] pos = posStr[i].Split('#');
            wayPointList.Add(new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2])));
        }
    }
}
public class StaticEnemyWeaponVo
{
    public int id;
    public string name;
    public float chargeTime;
    public float speed;
    public string path;
    public int bulletId;
    public int damage;
    public int type;
    public StaticEnemyWeaponVo(string[] al)
    {
        id = int.Parse(al[0]);
        name = al[1];
        chargeTime = float.Parse(al[2]);
        speed = float.Parse(al[3]);
        path = al[4];
        bulletId = int.Parse(al[5]);
        damage = int.Parse(al[6]);
        type = int.Parse(al[7]);
    }
}
public class StaticWeaponVo
{
    public int id;
    public string name;
    public float chargeTime;
    public float speed;
    public string icon;
    public string path;
    public int bulletId;
    public int damage;
    public string effect;
    public StaticWeaponVo(string[] al)
    {
        id = int.Parse(al[0]);
        name = al[1];
        chargeTime = float.Parse(al[2]);
        speed = float.Parse(al[3]);
        icon = al[4];
        path = al[5];
        bulletId = int.Parse(al[6]);
        damage = int.Parse(al[7]);
        effect = al[8];
    }
}
public class StaticBulletVo
{
    public int id;
    public string path;
    public string desc;
    public string effect;
    public float destroyTime;
    public int type;

    public StaticBulletVo(string[] al)
    {
        id = int.Parse(al[0]);
        path = al[1];
        desc = al[2];
        effect = al[3];
        destroyTime = float.Parse(al[4]);
    }
}
public class StaticItemVo
{
    public int id;
    public string path;
    public string desc;
    public int affect;
    public int addType;
    public float value;
    public string effect;

    public StaticItemVo(string[] al)
    {
        id = int.Parse(al[0]);
        path = al[1];
        desc = al[2];
        affect = int.Parse(al[3]);
        addType = int.Parse(al[4]);
        value = float.Parse(al[5]);
        effect = al[6];
    }
}
public class StaticTipVo
{
    public int id;
    public List<string> desc=new List<string>();
    public List<int> camPos=new List<int>();
    

    public StaticTipVo(string[] al)
    {
        id = int.Parse(al[0]);
        string[] descStr = al[1].Split('|');
        int i;
        for (i = 0; i < descStr.Length; i++)
        {
            desc.Add(descStr[i]);
        }
        string[] camStr = al[2].Split('|');
        for (i = 0; i < camStr.Length; i++)
        {
            camPos.Add(int.Parse(camStr[i]));
        }
    }
}