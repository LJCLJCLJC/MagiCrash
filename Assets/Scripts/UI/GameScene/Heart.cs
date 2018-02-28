using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Image redHeart;
    public void Create(float num)
    {
        redHeart.fillAmount = num / 4f;
    }
}
