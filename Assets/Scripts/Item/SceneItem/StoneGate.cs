using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGate : MonoBehaviour
{
    public int id = -1;
    public GameObject[] stones;
    public int eachHealth = 2;
    private int totalHealth;
    private int nowHealth;
    private int nowStone = 0;
    private void Start()
    {
        nowHealth = eachHealth;
        totalHealth = eachHealth * stones.Length;
        for(int i = 1; i < stones.Length; i++)
        {
            stones[i].SetActive(false);
        }
    }
    public void Hurt(int bulletId)
    {
        if (id != bulletId)
            return;
        nowHealth--;
        totalHealth--;
        if (totalHealth == 0)
        {
            GameObject effect = Tools.CreateGameObject("Effects/RockCrash");
            effect.transform.position = stones[stones.Length-1].transform.position;
            Destroy(gameObject);
        }
        if (nowHealth == 0 && nowStone<stones.Length-1)
        {
            nowStone++;
            for(int i = 0; i < stones.Length; i++)
            {
                GameObject effect = Tools.CreateGameObject("Effects/RockCrash");
                effect.transform.position = stones[i].transform.position;
                stones[i].SetActive(false);
            }
            stones[nowStone].SetActive(true);
            nowHealth = eachHealth;

        }
    }

    private void OnDestroy()
    {

    }
}
