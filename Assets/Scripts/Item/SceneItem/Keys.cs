using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour {

    public int id;
    private PlayerData player;
    private List<int> hasKey = new List<int>();
	private void Start ()
    {
        player = GameRoot.Instance.GetNowPlayer();
        string[] keyStr = player.hasKey.Split('|');
        if (keyStr[0] == GameController.Instance.LevelName)
        {
            for(int i = 1; i < keyStr.Length; i++)
            {
                hasKey.Add(int.Parse(keyStr[i]));
            }
        }
        if (hasKey.Contains(id))
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.hasKey += ("|" + id);
            GameRoot.Instance.GetNowPlayer().startPosition = GameController.Instance.tsPlayer.position.x + "#" + GameController.Instance.tsPlayer.position.y + "#" + GameController.Instance.tsPlayer.position.z;
            GameRoot.Instance.evt.CallEvent(GameEventDefine.GET_KEY, null);
            GameRoot.Instance.evt.CallEvent(GameEventDefine.SAVE_GAME, null);
            Destroy(gameObject);
        }
    }

}
