using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager:MonoBehaviour
{
    public static UIManager Instance;
    public virtual void StartUIManager()
    {
        Instance = this;
    }
    private void Awake()
    {
        StartUIManager();
        ParsePanelTypeJson();
    }
    public Transform tsCanvas;
    private Dictionary<Panel_ID, string> panelPathDic;
    private Dictionary<Panel_ID, BasePanel> panelDic;
    private Stack<BasePanel> panelStack;

    public BasePanel GetTopPanel()
    {
        return panelStack.Peek();
    }
    public void PushPanel(Panel_ID panel_ID)
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if (panelStack.Count > 0)
        { 
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }
        BasePanel panelTemp = GetPanel(panel_ID);
        panelTemp.OnEnter();
        panelStack.Push(panelTemp);
    }
    public void PushPanel(Panel_ID panel_ID,object param)
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }
        BasePanel panelTemp = GetPanel(panel_ID);
        panelTemp.OnEnter(param);
        panelStack.Push(panelTemp);
    }
    public void CreateConfirmPanel(string desc, CallBackFunctionWithObject OK=null,object paramOK=null,CallBackFunctionWithObject Cancel=null,object paramCancel=null)
    {
        ConfirmDetial detial = new ConfirmDetial();
        detial.desc = desc;
        detial.OK = OK;
        detial.paramOK = paramOK;
        detial.Cancel = Cancel;
        detial.paramCancel = paramCancel;
        PushPanel(Panel_ID.ConfirmPanel, detial);
    }

    public void PopPanel()
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if (panelStack.Count <= 0) return;
        BasePanel topPanel1 = panelStack.Pop();
        topPanel1.OnExit();
        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();
    }

    public void Clear()
    {
        panelStack = new Stack<BasePanel>();
    }
    
    [System.Serializable]
    class PanelIDJson
    {
        public PanelInfo[] infoList=null;
    }

    private BasePanel GetPanel(Panel_ID id)
    {
        if (panelDic == null)
        {
            panelDic = new Dictionary<Panel_ID, BasePanel>();
        }

        BasePanel panel;
        panelDic.TryGetValue(id, out panel);
        if (panel == null)
        {
            string path;
            panelPathDic.TryGetValue(id, out path);
            GameObject newPanel = Tools.CreateGameObject(path, tsCanvas);
            panelDic.Add(id, newPanel.GetComponent<BasePanel>());
            return newPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }
    }

    private void ParsePanelTypeJson()
    {
        panelPathDic = new Dictionary<Panel_ID, string>();
        TextAsset ta = Resources.Load<TextAsset>("UI/UIPanelType");
        PanelIDJson jsonObject = JsonUtility.FromJson<PanelIDJson>(ta.text);
        foreach(PanelInfo info in jsonObject.infoList)
        {
            panelPathDic.Add(info.panelID, info.path);
        }
    }
}
