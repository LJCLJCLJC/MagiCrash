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
    private SettingData setting;
    void Start () {
        GameRoot.Instance.evt.AddListener(GameEventDefine.BOSS_BATTLE, OnBossBattle);
        GameRoot.Instance.evt.AddListener(GameEventDefine.GET_WEAPON, OnGetWeapon);
        GameRoot.Instance.evt.AddListener(GameEventDefine.IN_BATTLE, OnInBattle);
        GameRoot.Instance.evt.AddListener(GameEventDefine.OUT_BATTLE, OnOutBattle);
        GameRoot.Instance.evt.AddListener(GameEventDefine.LEVEL_CLEAR, OnLevelClear);
        GameRoot.Instance.evt.AddListener(GameEventDefine.GET_KEY, OnGetWeapon);
        GameRoot.Instance.evt.AddListener(GameEventDefine.SET_BGM_VOLUME, OnSetBgmVolume);
        GameRoot.Instance.evt.AddListener(GameEventDefine.SET_EFFECT_VOLUME, OnSetEffectVolume);
        setting = DataManager.Instance.GetSetting();
        if (setting == null)
        {
            setting = new SettingData();
            audioSource.volume = 0.5f;
            audioEffect.volume = 0.2f;
            setting.bgmVolume = 0.5f;
            setting.effectVolume = 0.2f;
            setting.viewSensitive = 0.3f;
            DataManager.Instance.SaveSetting(setting);
        }
        else
        {
            audioSource.volume = setting.bgmVolume;
            audioEffect.volume = setting.effectVolume;
        }
    }

    private void OnSetBgmVolume(object obj)
    {
        audioSource.volume = (float)obj;
    }
    private void OnSetEffectVolume(object obj)
    {
        audioEffect.volume = (float)obj;
    }

    private void OnBossBattle(object obj)
    {
        PlayAudio(bossBgm);
    }

    private void OnInBattle(object obj)
    {
        battleTime++;
        PlayAudio(battleBgm);
        GameRoot.Instance.evt.CallEvent(GameEventDefine.MAGIC_CHANGE, true);
    }
    private void OnOutBattle(object obj)
    {
        battleTime--;
        if (battleTime < 0) battleTime = 0;
        if (battleTime == 0)
        {
            PlayAudio(idleBgm);
            GameRoot.Instance.evt.CallEvent(GameEventDefine.MAGIC_CHANGE, false);
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
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.GET_KEY, OnGetWeapon);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.SET_BGM_VOLUME, OnSetBgmVolume);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.SET_EFFECT_VOLUME, OnSetEffectVolume);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.LEVEL_CLEAR, OnLevelClear);
    }

}
