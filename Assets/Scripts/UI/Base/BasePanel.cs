using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    public Panel_ID panelID;
    public CanvasGroup canvasGroup;
    public Button btnClose;

    protected void Start()
    {
        if (btnClose != null)
        {
            btnClose.onClick.AddListener(CloseClick);
        }
    }

    public virtual void OnEnter()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        gameObject.SetActive(true);

    }
    public virtual void OnEnter(object param)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        gameObject.SetActive(true);
        Debug.Log("OnEnter:" + panelID);

    }
    public virtual void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        Debug.Log("OnPause:"+ panelID);
    }
    public virtual void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        Debug.Log("OnResume:" + panelID);
    }
    public virtual void OnExit()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        Debug.Log("OnExit:" + panelID);
    }

    private void CloseClick()
    {
        UIManager.Instance.PopPanel();
    }
}
