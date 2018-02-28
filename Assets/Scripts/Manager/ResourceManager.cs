using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ResourceCallback(TextAsset obj, object param);
public class ResourceManager
{
    private static ResourceManager _instance;
    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ResourceManager();
            }
            return _instance;
        }
    }

    public object StartLoadResource(string path, ResourceCallback callback = null,object param = null)
    {

        TextAsset prefab = Resources.Load<TextAsset>(path);
        if(callback!=null)
        {
            callback(prefab, param);
        }
        return prefab;
    }
}
