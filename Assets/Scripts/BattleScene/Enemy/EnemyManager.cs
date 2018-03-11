using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EnemyManager();
            }
            return _instance;
        }
    }

    private List<int> clearList;
    private List<StaticEnemyGroupVo> groupList;
    private void Start()
    {
        //GameRoot.Instance.evt.AddListener(GameEventDefine.LOAD_GAME,OnUpdate);
        OnUpdate(null);
    }

    private void OnUpdate(object obj)
    {
        clearList = DataManager.Instance.GetClearedEnemyGroups(GameRoot.Instance.GetNowPlayer());
        groupList = StaticDataPool.Instance.staticEnemyGroupPool.GetStaticDataPool();
        for (int i = 0; i < groupList.Count; i++)
        {
            
            if (groupList[i].level == SceneManager.GetActiveScene().name)
            {
                GameObject goObj = Tools.CreateGameObject("Models/Enemy/EnemyGroup", transform.parent);
                goObj.name = "EnemyGroup" + groupList[i].id;
                goObj.GetComponent<EnemyGroup>().Create(groupList[i]);
            }
        }

    }
    private void OnDestroy()
    {
       // GameRoot.Instance.evt.RemoveListener(GameEventDefine.LOAD_GAME, OnUpdate);

    }
}
