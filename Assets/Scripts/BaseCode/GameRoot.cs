using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour {
    public static GameRoot Instance;

    public EventCenter evt;
    public TimeLine timeLine;
    public ActivePool activePool;
    public string currentLoadScene;
    public Transform tsPlayer;
    private bool canMove = true;
    private PlayerData nowPlayer;

    public bool CanMove
    {
        get
        {
            return canMove;
        }
    }

    private void Awake()
    {
        Instance = this;
        evt = new EventCenter();
        timeLine = new TimeLine();
        activePool = new ActivePool();
        DontDestroyOnLoad(gameObject);

        DataManager.Instance.GetAllPlayer();
        
    }
    void Start () {
        UIManager.Instance.PushPanel(Panel_ID.StartPanel);
        evt.AddListener(GameEventDefine.INIT_DATA, InitData);
        evt.AddListener(GameEventDefine.LOAD_GAME, LoadGame);
        evt.AddListener(GameEventDefine.SAVE_GAME, SaveGame);
        evt.AddListener(GameEventDefine.GAME_PAUSE, OnPause);
        evt.AddListener(GameEventDefine.GAME_RESUME, OnResume);
        tsPlayer = GameObject.FindGameObjectWithTag("Player").transform.root;


    }
    private void FixedUpdate()
    {
        timeLine.Update();
    }

    public void SetNowPlayer(int id)
    {
        nowPlayer = DataManager.Instance.GetPlayer(id);
    }
    public PlayerData GetNowPlayer()
    {
        return nowPlayer;
    }

    private void InitData(object obj)
    {
        StaticDataPool.Instance.CreateData();
    }
    private void LoadGame(object obj)
    {
        tsPlayer = GameObject.FindGameObjectWithTag("Player").transform.root;
    }
    public void SaveGame(object obj)
    {
        DataManager.Instance.Save(nowPlayer);
        Debug.Log("Saved");
    }

    private void OnPause(object obj)
    {
        canMove = false;
    }
    private void OnResume(object obj)
    {
        canMove = true;
    }
    private void OnDestroy()
    {
        evt.RemoveListener(GameEventDefine.INIT_DATA, InitData);
        evt.RemoveListener(GameEventDefine.LOAD_GAME, LoadGame);
        evt.RemoveListener(GameEventDefine.SAVE_GAME, SaveGame);
        evt.RemoveListener(GameEventDefine.GAME_PAUSE, OnPause);
        evt.RemoveListener(GameEventDefine.GAME_RESUME, OnResume);
    }

}
