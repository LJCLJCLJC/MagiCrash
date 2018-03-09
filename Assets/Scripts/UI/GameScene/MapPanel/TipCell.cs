using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipCell : MonoBehaviour {
    public Text txt;
	public void Create(int id)
    {
        StaticTipVo vo = StaticDataPool.Instance.staticTipPool.GetStaticDataVo(id);
        txt.text = vo.desc[1];
    }
}
