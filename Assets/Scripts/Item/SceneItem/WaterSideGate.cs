using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSideGate : MonoBehaviour {

    public bool open = false;
    private float tipTime = 5f;
    private float time;
    private bool canTip=true;
	void Start ()
    {
        ComBineMesh();

    }
    private void Update()
    {
        if (Time.time - time >= tipTime&&canTip==false)
        {
            canTip = true;
        }
    }
    private void ComBineMesh()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);
        gameObject.AddComponent<MeshCollider>().sharedMesh = transform.GetComponent<MeshFilter>().mesh;
    }
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Player"&&GameController.Instance.canOpen == false&&canTip==true)
        {
            GameRoot.Instance.evt.CallEvent(GameEventDefine.SHOW_TIP, 12);
            Debug.Log(collision.gameObject.name);
            time = Time.time;
            canTip = false;
        }
        else if(collision.gameObject.tag == "Player" && GameController.Instance.canOpen == true)
        {
            gameObject.GetComponent<MeshCollider>().convex = true;
            gameObject.GetComponent<MeshCollider>().isTrigger = true;

        }
    }
}
