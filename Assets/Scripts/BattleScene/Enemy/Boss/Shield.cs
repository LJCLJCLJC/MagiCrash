using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public int health;
    public int Id;
    public int lowDamage;
    public int highDamage;

    public void Hurt(int bulletId)
    {
        if (Id == bulletId)
        {
            health -= highDamage;
        }
        else
        {
            health -= lowDamage;
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
