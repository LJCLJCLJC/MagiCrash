using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSideGate : MonoBehaviour {

    public bool open = false;
    public ParticleSystem particle;
    private float tipTime = 5f;
    private float time;
    private bool canTip=true;
	void Start ()
    {

    }
    private void Update()
    {
        if (Time.time - time >= tipTime&&canTip==false)
        {
            canTip = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "PlayerBody"&&GameController.Instance.canOpen == false&&canTip==true)
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.SHOW_TIP, 12);
            Debug.Log(collision.gameObject.name);
            time = Time.time;
            canTip = false;
        }
        else if(collision.gameObject.tag == "PlayerBody" && GameController.Instance.canOpen == true)
        {
            gameObject.GetComponent<MeshCollider>().convex = true;
            gameObject.GetComponent<MeshCollider>().isTrigger = true;

        }
    }
}
