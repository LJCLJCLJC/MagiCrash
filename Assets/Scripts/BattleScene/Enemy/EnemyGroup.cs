using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;
using MalbersAnimations.Utilities;

public class EnemyGroup : MonoBehaviour {

    private StaticEnemyGroupVo enemyGroupVo;
    private List<int> enemyList = new List<int>();
    private PlayerData player;
    private int count;

    public void Create(StaticEnemyGroupVo enemyGroupVo)
    {
        player = GameRoot.Instance.GetNowPlayer();
        this.enemyGroupVo = enemyGroupVo;
        enemyList = enemyGroupVo.enemies;
        count = enemyList.Count;
        List<Transform> waypointList = new List<Transform>();
        for (int i = 0; i < enemyGroupVo.wayPointList.Count; i++)
        {
            GameObject point = new GameObject("point_" + i);
            point.transform.parent = transform;
            point.transform.position = enemyGroupVo.wayPointList[i];
            point.AddComponent<MWayPoint>();
            waypointList.Add(point.transform);
        }
        for (int i = 0; i < waypointList.Count - 1; i++)
        {
            waypointList[i].GetComponent<MWayPoint>().NextWaypoint = waypointList[i + 1];
            waypointList[i].GetComponent<MWayPoint>().StoppingDistance = 1f;
        }
        waypointList[waypointList.Count - 1].GetComponent<MWayPoint>().NextWaypoint = waypointList[0];
        waypointList[waypointList.Count - 1].GetComponent<MWayPoint>().StoppingDistance = 1f;
        for (int i = 0; i < enemyList.Count; i++)
        {
            StaticEnemyVo enemyVo = StaticDataPool.Instance.staticEnemyPool.GetStaticDataVo(enemyList[i]);
            GameObject goObj = Tools.CreateGameObject("Models/Enemy/"+enemyVo.path, transform.parent,enemyGroupVo.unitPos[i]);
            goObj.GetComponent<Enemy>().Create(enemyVo, enemyGroupVo.id, waypointList);

        }

        GameRoot.Instance.evt.AddListener(GameEventDefine.ENEMY_DIE, OnEnemyDie);
    }
    private void OnEnemyDie(object obj)
    {
        int groupId = (int)obj;
        if (groupId == enemyGroupVo.id)
        {
            count--;
            if (count <= 0)
            {
                player.clearedEnemyGroup = player.clearedEnemyGroup + groupId + "|";
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.ENEMY_DIE, OnEnemyDie);
    }
}
