using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public Transform target;
    public Transform top;
    

    public float totalTime = 1f;
    private float startTime = 0;
    private float nowTime = 0;
    private Bezier bezier;
    private GameObject player;
    private bool act = false;



    private void Start()
    {
        
    }

    private void Update()
    {
        if (act)
        {
            float overTime = Time.time - startTime;
            float overLimitTime = overTime / totalTime;
            player.transform.position = bezier.GetPointAtTime(overLimitTime);
            if (overLimitTime >= 1)
            {
                overLimitTime = 1;
                act = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBody")
        {
            bezier = new Bezier(transform.position, top.position, target.position);
            player = other.transform.root.gameObject;
            startTime = Time.time;
            act = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(3, 1, 3));
        Gizmos.DrawWireCube(target.position, new Vector3(3, 1, 3));
        Gizmos.DrawWireCube(top.position, new Vector3(3, 1, 3));
        Gizmos.DrawLine(transform.position, top.position);
        Gizmos.DrawLine(top.position, target.position);

    }
}
