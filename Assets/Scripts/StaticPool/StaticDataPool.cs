using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDataPool
{
    public StaticEnemyPool staticEnemyPool;
    public StaticEnemyGroupPool staticEnemyGroupPool; 
    public StaticWeaponPool staticWeaponPool;
    public StaticEnemyWeaponPool staticEnemyWeaponPool;
    public StaticBulletPool staticBulletPool;
    public StaticItemPool staticItemPool;
    public StaticTipPool staticTipPool;
    private static StaticDataPool _instance;

    public static StaticDataPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StaticDataPool();
            }
            return _instance;
        }
    }

    public void CreateData()
    {
        staticEnemyPool = new StaticEnemyPool();
        staticEnemyGroupPool = new StaticEnemyGroupPool();
        staticWeaponPool = new StaticWeaponPool();
        staticEnemyWeaponPool = new StaticEnemyWeaponPool();
        staticBulletPool = new StaticBulletPool();
        staticItemPool = new StaticItemPool();
        staticTipPool = new StaticTipPool();
        ResourceManager.Instance.StartLoadResource("Data/Enemy", LoadAsset, staticEnemyPool);
        ResourceManager.Instance.StartLoadResource("Data/EnemyGroup", LoadAsset, staticEnemyGroupPool);
        ResourceManager.Instance.StartLoadResource("Data/Weapon", LoadAsset, staticWeaponPool);
        ResourceManager.Instance.StartLoadResource("Data/EnemyWeapon", LoadAsset, staticEnemyWeaponPool);
        ResourceManager.Instance.StartLoadResource("Data/Bullet", LoadAsset, staticBulletPool);
        ResourceManager.Instance.StartLoadResource("Data/Item", LoadAsset, staticItemPool);
        ResourceManager.Instance.StartLoadResource("Data/Tips", LoadAsset, staticTipPool);
    }

    private void LoadAsset(TextAsset obj,object loadPool)
    {
        TextAsset binAsset = obj;
        if (loadPool == staticEnemyPool)
            staticEnemyPool.AddData(LoadData(binAsset));
        else if (loadPool == staticEnemyGroupPool)
            staticEnemyGroupPool.AddData(LoadData(binAsset));
        else if (loadPool == staticWeaponPool)
            staticWeaponPool.AddData(LoadData(binAsset));
        else if (loadPool == staticEnemyWeaponPool)
            staticEnemyWeaponPool.AddData(LoadData(binAsset));
        else if (loadPool == staticBulletPool)
            staticBulletPool.AddData(LoadData(binAsset));
        else if (loadPool == staticItemPool)
            staticItemPool.AddData(LoadData(binAsset));
        else if (loadPool == staticTipPool)
            staticTipPool.AddData(LoadData(binAsset));
    }

    private string[] LoadData(TextAsset binAsset)
    {
        
        string[] lineArray = binAsset.text.Split("\n"[0]);
        return lineArray;
    }
}

public class StaticEnemyPool
{
    private List<StaticEnemyVo> _datapool;
    public StaticEnemyPool()
    {
        _datapool = new List<StaticEnemyVo>();
    }
    public void AddData(string[] lineArray)
    {
        for(int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticEnemyVo vo = new StaticEnemyVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticEnemyVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
}

public class StaticEnemyGroupPool
{
    private List<StaticEnemyGroupVo> _datapool;
    public StaticEnemyGroupPool()
    {
        _datapool = new List<StaticEnemyGroupVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticEnemyGroupVo vo = new StaticEnemyGroupVo(strArray);
            _datapool.Add(vo);
        }
    }

    public List<StaticEnemyGroupVo> GetStaticDataPool()
    {
        return _datapool;
    }
    public StaticEnemyGroupVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
}

public class StaticWeaponPool
{
    private List<StaticWeaponVo> _datapool;
    public StaticWeaponPool()
    {
        _datapool = new List<StaticWeaponVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticWeaponVo vo = new StaticWeaponVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticWeaponVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
}


public class StaticEnemyWeaponPool
{
    private List<StaticEnemyWeaponVo> _datapool;
    public StaticEnemyWeaponPool()
    {
        _datapool = new List<StaticEnemyWeaponVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticEnemyWeaponVo vo = new StaticEnemyWeaponVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticEnemyWeaponVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
}


public class StaticBulletPool
{
    private List<StaticBulletVo> _datapool;
    public StaticBulletPool()
    {
        _datapool = new List<StaticBulletVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticBulletVo vo = new StaticBulletVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticBulletVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
}

public class StaticItemPool
{
    private List<StaticItemVo> _datapool;
    public StaticItemPool()
    {
        _datapool = new List<StaticItemVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticItemVo vo = new StaticItemVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticItemVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
}

public class StaticTipPool
{
    private List<StaticTipVo> _datapool;
    public StaticTipPool()
    {
        _datapool = new List<StaticTipVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 1; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            string[] strArray = lineArray[i].Split(","[0]);
            StaticTipVo vo = new StaticTipVo(strArray);
            _datapool.Add(vo);
        }
    }
    public StaticTipVo GetStaticDataVo(int id)
    {
        return _datapool.Find(p => p.id == id);
    }
}


