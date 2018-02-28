using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPool {

    public StaticLogPool staticLogPool;

    private static LogPool _instance;
    public static LogPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LogPool();
            }
            return _instance;
        }
    }
    public void CreateData()
    {
        staticLogPool = new StaticLogPool();
        for(int i = 1; i <= 15; i++)
        {
            ResourceManager.Instance.StartLoadResource("Data/Log/payLoger1-"+i, LoadAsset, staticLogPool);
        }
        ResourceManager.Instance.StartLoadResource("Data/Log/payLoger2-1", LoadAsset, staticLogPool);

    }

    private void LoadAsset(TextAsset obj, object loadPool)
    {
        TextAsset binAsset = obj;
        Debug.Log(binAsset.text);
        if (loadPool == staticLogPool)
            staticLogPool.AddData(LoadData(binAsset));

    }

    private string[] LoadData(TextAsset binAsset)
    {

        string[] lineArray = binAsset.text.Split("\n"[0]);
        return lineArray;
    }
}

public class StaticLogPool
{
    private List<StaticLogVo> _datapool;
    public StaticLogPool()
    {
        _datapool = new List<StaticLogVo>();
    }
    public void AddData(string[] lineArray)
    {
        for (int i = 0; i < lineArray.Length; i++)
        {
            lineArray[i] = lineArray[i].Replace("\r", "");
            if (lineArray[i].Contains("off pay"))
            {
                string[] strArray = lineArray[i].Split(" "[0]);
                StaticLogVo vo = new StaticLogVo(strArray);
                _datapool.Add(vo);
            }
        }
    }
    public int GetPayOff()
    {
        int all=0;
        for(int i = 0; i < _datapool.Count; i++)
        {
            all += _datapool[i].payoff;
        }
        return all;
    }

}
public class StaticLogVo
{
    public string str;
    public int payoff;

    public StaticLogVo(string[] al)
    {
        str = al[7];
        payoff = int.Parse(str.Split(':')[2]);
    }
}
