using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void CallBackFunctionWithObject(object param);
public class ConfirmDetial
{
    public string desc;
    public CallBackFunctionWithObject OK;
    public object paramOK;
    public CallBackFunctionWithObject Cancel;
    public object paramCancel;
}
public class ConfirmPanel : BasePanel {

    public Text desc;
    public Button btnOK;
    public Button btnCancel;

    private ConfirmDetial detial=new ConfirmDetial();

    private new void Start()
    {
        base.Start();
        btnOK.onClick.AddListener(delegate() { BtnClick(btnOK); });
        btnCancel.onClick.AddListener(delegate () { BtnClick(btnCancel  ); });
    }

    public override void OnEnter(object param)
    {
        base.OnEnter(param);
        detial = (ConfirmDetial)param;
        desc.text = detial.desc;
    }

    public void BtnClick(Button button)
    {
        switch (button.name)
        {
            case "OK":
                if (detial.OK != null)
                {
                    detial.OK(detial.paramOK);
                }
                
                break;
            case "Cancel":
                if (detial.Cancel != null)
                {
                    detial.Cancel(detial.paramCancel);
                }
                UIManager.Instance.PopPanel();
                break;
        }
    }
}
