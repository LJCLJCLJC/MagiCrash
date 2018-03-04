using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicController : MonoBehaviour {
    public AudioSource audioSource;
    public AudioSource audioEffect;
    public AudioClip[] clips;
	void Start () {
        GameRoot.Instance.evt.AddListener(GameEventDefine.BOSS_BATTLE, OnBossBattle);
        GameRoot.Instance.evt.AddListener(GameEventDefine.MAGIC_CHANGE, OnBossBattle);
        GameRoot.Instance.evt.AddListener(GameEventDefine.GET_WEAPON, OnGetWeapon);
	}

    private void OnBossBattle(object obj)
    {

    }

    private void OnInBattle(object obj)
    {

    }

    private void OnGetWeapon(object obj)
    {
        
    }
    private void PlayAudio(AudioClip clip)
    {
        if (audioSource.clip.name != clip.name)
        {
            audioSource.DOFade(0, 0.2f).OnComplete<Tween>(delegate () { audioSource.clip = clip; audioSource.DOFade(1, 0.2f); });

        }
    }

    private void PlayEffect(AudioClip clip)
    {
        if (audioEffect.clip.name != clip.name)
        {
            audioEffect.DOFade(0, 0.2f).OnComplete<Tween>(delegate () { audioEffect.clip = clip; audioEffect.DOFade(1, 0.2f); });

        }
    }

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.BOSS_BATTLE, OnBossBattle);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.MAGIC_CHANGE, OnBossBattle);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.GET_WEAPON, OnGetWeapon);
    }

}
