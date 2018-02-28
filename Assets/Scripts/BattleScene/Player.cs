using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations.Utilities;

public class Player : MonoBehaviour {
    public ActiveMeshes mesh;
    public MaterialChanger material;
    public GameObject Antler;
    public Transform tsHead;
    public Transform tsBody;
    public Transform tsEffect;
    private PlayerData nowPlayer;

    private void Start()
    {
        GameRoot.Instance.evt.AddListener(GameEventDefine.MAGIC_CHANGE, ChangeMagic);
        nowPlayer = GameRoot.Instance.GetNowPlayer();
        Init();
    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.MAGIC_CHANGE, ChangeMagic);
    }
    private void Init()
    {
        material.SetMaterial(0, nowPlayer.skin);
        mesh.ChangeMesh(0, nowPlayer.antler);
        mesh.ChangeMesh(2, nowPlayer.spot);
        if (nowPlayer.weapons == "0")
        {
            mesh.ChangeMesh(1, 1);
        }
        else
        {
            mesh.ChangeMesh(1, 0);
            material.SetMaterial(1, nowPlayer.color);
        }
        string[] PosStr = nowPlayer.startPosition.Split('#');
        Vector3 startPos = new Vector3(float.Parse(PosStr[0]), float.Parse(PosStr[1]), float.Parse(PosStr[2]));
        transform.position = startPos;
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
        Tools.ClearChildFromParent(tsEffect);
        string effectPath = (string)obj;
        PSMeshRendererUpdater effect = Tools.CreateGameObject("Effects/AntlerMagic/" + effectPath, tsEffect).GetComponent<PSMeshRendererUpdater>();
        if (effect == null) return;
        effect.MeshObject = Antler;
        effect.UpdateMeshEffect();
        mesh.ChangeMesh(1, 0);
        material.SetMaterial(1, nowPlayer.color);
    }

}
