using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance;
    public Transform tsPlayer;
    public string LevelName = SceneName.Level1_1;
    public int needKey = 3;
    public bool showingTip = false;
    public bool canOpen = false;
    private PlayerData player;
    private int hasKey;

    public bool isPausing=false;
    private void Awake()
    {
        Instance = this;
    }
    private void Start ()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.GET_KEY, UpDateHasKey);
        player = GameRoot.Instance.GetNowPlayer();
        UIManager.Instance.PushPanel(Panel_ID.GamePanel);
        UpDateHasKey(null);
	}

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)&& !isPausing)
        {
            UIManager.Instance.PushPanel(Panel_ID.PausePanel);
            GameRoot.Instance.evt.CallEvent(GameEventDefine.GAME_PAUSE, null);
            isPausing = true;
        }
    }

    private void UpDateHasKey(object obj)
    {
        hasKey = 0;
        string[] keyStr = player.hasKey.Split('|');
        for(int i = 1; i < keyStr.Length; i++)
        {
            if (keyStr[i] != "")
            {
                hasKey++;
            }
        }
        if (LevelName == keyStr[0] && needKey == hasKey)
        {
            canOpen = true;
        }
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.GET_KEY, UpDateHasKey);
    }

}
