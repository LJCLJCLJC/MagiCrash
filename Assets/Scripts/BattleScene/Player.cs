using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations.Utilities;

public class Player : MonoBehaviour {
    public ActiveMeshes mesh;
    public MaterialChanger material;
    public PSMeshRendererUpdater antlerMagic;
    public Transform[] tsAntler;
    public Transform tsHead;
    public Transform tsBody;
    public Transform tsEffect;
    public Color color;
    private PlayerData nowPlayer;

    private void Start()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.MAGIC_CHANGE, ChangeMagic);
        GameRoot.Instance.evt.AddListener(GameEventDefine.CHANGE_ANTLER_MAGIC, ChangeAntlerMagic);
        nowPlayer = GameRoot.Instance.GetNowPlayer();
        Init();
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.MAGIC_CHANGE, ChangeMagic);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.CHANGE_ANTLER_MAGIC, ChangeAntlerMagic);
    }
    private void Init()
    {
        material.SetMaterial(0, nowPlayer.skin);
        mesh.ChangeMesh(0, nowPlayer.antler);
        mesh.ChangeMesh(2, nowPlayer.spot);
        mesh.ChangeMesh(1, 1);

        string[] PosStr = nowPlayer.startPosition.Split('#');
        Vector3 startPos = new Vector3(float.Parse(PosStr[0]), float.Parse(PosStr[1]), float.Parse(PosStr[2]));
        transform.position = startPos;
        antlerMagic.transform.parent = tsAntler[nowPlayer.antler-1];
        antlerMagic.gameObject.SetActive(false);
    }
    public void Hurt(int damage)
    {
        if (damage - nowPlayer.defence <= 1)
        {
            damage = 1;
        }
        else
        {
            damage -= nowPlayer.defence;
        }
        nowPlayer.nowHealth = nowPlayer.nowHealth - damage;
        GameRoot.Instance.evt.CallEvent(GameEventDefine.PLAYER_DAMAGE, null);
        Debug.Log(nowPlayer.nowHealth);
        if (nowPlayer.nowHealth <= 0)
        {
            Die();
            GameRoot.Instance.evt.CallEvent(GameEventDefine.PLAYER_DIE, null);

        }
    }
    private void Die()
    {
        Debug.Log("die");
    }
    private void ChangeMagic(object obj)
    {
        bool show = (bool)obj;
        if (show)
        {
            mesh.ChangeMesh(1, 0);
            material.SetMaterial(1, nowPlayer.color);
        }
        else
        {
            mesh.ChangeMesh(1, 1);
        }

    }

    private void ChangeAntlerMagic(object obj)
    {
        antlerMagic.gameObject.SetActive(true);
        float color = (float)obj;
        antlerMagic.UpdateMeshEffect();
        antlerMagic.UpdateColor(color/360f);

    }
}
