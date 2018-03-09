using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }
            return _instance;
        }
    }

    private PlayerList playerList=new PlayerList();

    public void Save(PlayerData player)
    {
        if (playerList.playerList == null)
        {
            playerList.playerList.Add(player);
        }
        if (player.id >= playerList.playerList.Count)
        {
            playerList.playerList.Add(player);
        }
        else
        {
            playerList.playerList[player.id] = player;
        }
        string jsonInfo = JsonUtility.ToJson(playerList);

        StreamWriter sw;
        FileInfo t = new FileInfo(Application.persistentDataPath + "//playerData.json");
        if (!t.Exists)
        {
            sw = t.CreateText();
        }
        else
        {
            sw = t.CreateText();
            
        }
        sw.Write(jsonInfo);
        sw.Close();
        sw.Dispose();

    }

    public PlayerData GetPlayer(int i)
    {
        if (i >= playerList.playerList.Count || i < 0)
        {
            Debug.Log("不存在存档");
            return null;
        }
        else if(playerList.playerList[i].open != false)
        {
            return playerList.playerList[i];
        }
        else
        {
            return null;
        }
    }

    public PlayerList GetAllPlayer()
    {
        
        PlayerList pl=new PlayerList();
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(Application.persistentDataPath+"//playerData.json");
        }catch(Exception e)
        {
            Debug.Log("存档文件不存在"+e.Message);
            return null;
        }


        string jsonStr = sr.ReadToEnd();
        PlayerList jsonInfo = JsonUtility.FromJson<PlayerList>(jsonStr);

        foreach (PlayerData p in jsonInfo.playerList)
        {
            pl.playerList.Add(p);
        }
        playerList = pl;
        sr.Close();
        sr.Dispose();
        return playerList;
    }

    public void Delete(int id)
    {
        PlayerData pd = GetPlayer(id);
        pd.open = false;
        Save(pd);
    }

    public List<StaticWeaponVo> GetWeapons(PlayerData player)
    {
        string[] weapons = player.weapons.Split('|');
        List<StaticWeaponVo> staticWeaponList = new List<StaticWeaponVo>();
        for(int i = 0; i < weapons.Length; i++)
        {
            staticWeaponList.Add(StaticDataPool.Instance.staticWeaponPool.GetStaticDataVo(int.Parse(weapons[i])));
        }
        return staticWeaponList;
    }
    public List<int> GetWeaponsId(PlayerData player)
    {
        string[] weapons = player.weapons.Split('|');
        List<int> weaponList = new List<int>();
        for (int i = 0; i < weapons.Length; i++)
        {
            weaponList.Add(int.Parse(weapons[i]));
        }
        return weaponList;
    }
    public List<int> GetClearedEnemyGroups(PlayerData player)
    {
        List<int> list = new List<int>();
        if (player.clearedEnemyGroup != "")
        {
            string clearStr = player.clearedEnemyGroup.Remove(player.clearedEnemyGroup.Length - 1, 1);
            string[] str = clearStr.Split('|');
            for(int i = 0; i < str.Length; i++)
            {
                list.Add(int.Parse(str[i]));
            }
        }
        return list;
    }

    public List<int> GetGuideList(PlayerData player)
    {
        List<int> list = new List<int>();
        string[] guideStr = player.guideList.Split('|');
        for(int i = 0; i < guideStr.Length; i++)
        {
            if (guideStr[i] != "")
            {
                list.Add(int.Parse(guideStr[i]));
            }
        }
        return list;
    }
    public List<int> GetTipList(PlayerData player)
    {
        List<int> list = new List<int>();
        string[] tipStr = player.tipList.Split('|');
        for (int i = 0; i < tipStr.Length; i++)
        {
            if (tipStr[i] != "")
            {
                list.Add(int.Parse(tipStr[i]));
            }
        }
        return list;
    }

    public List<int> GetOwnedItem(PlayerData player)
    {
        List<int> list = new List<int>();
        string[] tipStr = player.ownedItem.Split('|');
        for (int i = 0; i < tipStr.Length; i++)
        {
            if (tipStr[i] != "")
            {
                list.Add(int.Parse(tipStr[i]));
            }
        }
        return list;
    }
}

public class PlayerList
{
    public List<PlayerData> playerList = new List<PlayerData>();
}
[Serializable]
public class PlayerData
{
    public int id;
    public bool open;
    public string name;
    public int nowHealth;
    public int maxHealth;
    public int defence;
    public int powerPlus;
    public float shootSpeedPlus;
    public string weapons;
    public string clearedEnemyGroup;
    public string nowLevel;
    public int skin;
    public int antler;
    public int spot;
    public int color;
    public string startPosition;
    public string hasKey;
    public string guideList;
    public string tipList;
    public string ownedItem;
    public bool showMapPoint;
}
