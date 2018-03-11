using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel {

    public Slider bgmSlider;
    public Slider effectSlider;
    public Slider viewSensitive;
    public Button btnOK;
    private SettingData setting;

    public override void OnEnter()
    {
        base.OnEnter();
        setting = DataManager.Instance.GetSetting();
        bgmSlider.value = setting.bgmVolume;
        effectSlider.value = setting.effectVolume;
        viewSensitive.value = setting.viewSensitive;
        bgmSlider.onValueChanged.AddListener(OnBgmChanged);
        effectSlider.onValueChanged.AddListener(OnEffectChanged);
        viewSensitive.onValueChanged.AddListener(OnViewSensitiveChanged);
        btnOK.onClick.AddListener(delegate () { BtnClick(btnOK); });
    }


    public override void OnExit()
    {
        base.OnExit();
        btnOK.onClick.RemoveAllListeners();
        GameRoot.Instance.evt.CallEvent(GameEventDefine.SET_BGM_VOLUME, setting.bgmVolume);
        GameRoot.Instance.evt.CallEvent(GameEventDefine.SET_EFFECT_VOLUME, setting.effectVolume);
        GameRoot.Instance.evt.CallEvent(GameEventDefine.SET_VIEW_SENSITIVE, setting.viewSensitive);
    }
    private void OnBgmChanged(float value)
    {
        GameRoot.Instance.evt.CallEvent(GameEventDefine.SET_BGM_VOLUME, bgmSlider.value);
    }
    private void OnEffectChanged(float value)
    {
        GameRoot.Instance.evt.CallEvent(GameEventDefine.SET_EFFECT_VOLUME, effectSlider.value);
    }
    private void OnViewSensitiveChanged(float value)
    {
        GameRoot.Instance.evt.CallEvent(GameEventDefine.SET_VIEW_SENSITIVE, viewSensitive.value);
    }
    private void BtnClick(Button btn)
    {
        setting.bgmVolume = bgmSlider.value;
        setting.effectVolume = effectSlider.value;
        setting.viewSensitive = viewSensitive.value;
        DataManager.Instance.SaveSetting(setting);
        UIManager.Instance.PopPanel();
    }
}
