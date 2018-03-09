using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicController : MonoBehaviour {
    public AudioSource audioSource;
    public AudioSource audioEffect;
    public AudioClip idleBgm;
    public AudioClip battleBgm;
    public AudioClip bossBgm;
    public AudioClip winBgm;
    public AudioClip getWeaponEffect;
    public int battleTime = 0;
    void Start () {
        GameRoot.Instance.evt.AddListener(GameEventDefine.BOSS_BATTLE, OnBossBattle);
        GameRoot.Instance.evt.AddListener(GameEventDefine.GET_WEAPON, OnGetWeapon);
        GameRoot.Instance.evt.AddListener(GameEventDefine.IN_BATTLE, OnInBattle);
        GameRoot.Instance.evt.AddListener(GameEventDefine.OUT_BATTLE, OnOutBattle);
        GameRoot.Instance.evt.AddListener(GameEventDefine.LEVEL_CLEAR, OnLevelClear);

    }

    private void OnBossBattle(object obj)
    {
        PlayAudio(bossBgm);
    }

    private void OnInBattle(object obj)
    {
        battleTime++;
        PlayAudio(battleBgm);
    }
    private void OnOutBattle(object obj)
    {
        battleTime--;
        if (battleTime < 0) battleTime = 0;
        if (battleTime == 0)
        {
            PlayAudio(idleBgm);
        }

    }
    private void OnLevelClear(object obj)
    {
        audioSource.loop = false;
        PlayAudio(winBgm);
    }
    private void OnGetWeapon(object obj)
    {
        PlayEffect(getWeaponEffect);
    }

    private void PlayAudio(AudioClip clip)
    {
        if (audioSource.clip.name != clip.name)
        {
            audioSource.clip = clip;
            audioSource.Play();

        }
    }

    private void PlayEffect(AudioClip clip)
    {

            audioEffect.clip = clip;
            audioEffect.Play();

    }

    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.BOSS_BATTLE, OnBossBattle);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.IN_BATTLE, OnInBattle);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.OUT_BATTLE, OnOutBattle);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.GET_WEAPON, OnGetWeapon);
    }

}
