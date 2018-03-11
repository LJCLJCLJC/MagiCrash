using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : PoolItem
{
    public float destroyTime = 3;
    public AudioSource audio;
    public void Create(Vector3 pos)
    {
        audio.volume = DataManager.Instance.GetSettingData().effectVolume;
        transform.position = pos;
        TimeLine.GetInstance().AddTimeEvent(DestroyEft, destroyTime, null, gameObject);
    }
    private void DestroyEft(object obj)
    {
        Hide();
        TimeLine.GetInstance().RemoveTimeEvent(DestroyEft);
    }
}
