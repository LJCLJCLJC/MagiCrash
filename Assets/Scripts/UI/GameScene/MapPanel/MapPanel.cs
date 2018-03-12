using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : BasePanel
{

    public Image playerIcon;
    public Image map;
    public Transform tsTip;
    private float centerX;
    private float centerY;
    private float width;
    private float height;
    private float playerX;
    private float playerY;
    private bool show = false;
    private PlayerData player;
    public GameObject[] keys;
    public override void OnEnter()
    {
        base.OnEnter();

        centerX = (GameController.Instance.RightTop.position.x + GameController.Instance.leftButton.position.x) / 2;
        centerY = (GameController.Instance.RightTop.position.z + GameController.Instance.leftButton.position.z) / 2;
        width = GameController.Instance.RightTop.position.x - GameController.Instance.leftButton.position.x;
        height = GameController.Instance.RightTop.position.z - GameController.Instance.leftButton.position.z;
        playerX = GameController.Instance.tsPlayer.position.x - centerX;
        playerY = GameController.Instance.tsPlayer.position.z - centerY;
        playerIcon.transform.localPosition = new Vector3((playerX / width) * map.rectTransform.sizeDelta.x, (playerY / height) * map.rectTransform.sizeDelta.y, 0);
        show = true;
        player = GameRoot.Instance.GetNowPlayer();
        GameRoot.Instance.evt.AddListener(GameEventDefine.GET_KEY, OnUpdateHasKey);
        if (player.showMapPoint == true)
        {
            for(int i = 0; i < keys.Length; i++)
            {
                keys[i].SetActive(true);
            }
        }
        OnUpdateHasKey(null);
        UpdateTip();
    }

    public override void OnExit()
    {
        base.OnExit();
        show = false;
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.GET_KEY, OnUpdateHasKey);
        if (player.showMapPoint == true)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (!show) return;
        playerX = GameController.Instance.tsPlayer.position.x - centerX;
        playerY = GameController.Instance.tsPlayer.position.z - centerY;
        playerIcon.transform.localPosition = new Vector3((playerX / width) * map.rectTransform.sizeDelta.x, (playerY / height) * map.rectTransform.sizeDelta.y, 0);
        //if (Input.GetKeyUp(KeyCode.Escape))
        //{
        //    UIManager.Instance.PopPanel();
        //}
    }

    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }
    private void UpdateTip()
    {

        Tools.ClearChildFromParent(tsTip);
        int index = 0;
        if (DataManager.Instance.GetTipList(player).Contains(8))
        {
            GameObject obj = Tools.CreateGameObject("UI/GameScene/MapPanel/TipCell", tsTip, new Vector3(410, 460 - index * 220, 0), Vector3.one);
            obj.GetComponent<TipCell>().Create(8);
            index++;
        }
        if (DataManager.Instance.GetTipList(player).Contains(9))
        {
            GameObject obj = Tools.CreateGameObject("UI/GameScene/MapPanel/TipCell", tsTip, new Vector3(410, 460 - index * 220, 0), Vector3.one);
            obj.GetComponent<TipCell>().Create(9);
            index++;
        }
        if (DataManager.Instance.GetTipList(player).Contains(10))
        {
            GameObject obj = Tools.CreateGameObject("UI/GameScene/MapPanel/TipCell", tsTip, new Vector3(410, 460 - index * 220, 0), Vector3.one);
            obj.GetComponent<TipCell>().Create(10);
            index++;
        }
        //StaticDataPool.Instance.staticTipPool.GetStaticDataVo()
    }

    private void OnUpdateHasKey(object obj)
    {
        List<int> hasKey = new List<int>();
        string[] keyStr = player.hasKey.Split('|');
        if (keyStr[0] == GameController.Instance.LevelName)
        {
            for (int i = 1; i < keyStr.Length; i++)
            {
                hasKey.Add(int.Parse(keyStr[i]));
            }
        }
        if (hasKey.Contains(0)) keys[0].SetActive(false);
        if (hasKey.Contains(1)) keys[1].SetActive(false);
        if (hasKey.Contains(2)) keys[2].SetActive(false);
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.GET_KEY, OnUpdateHasKey);
    }
}
