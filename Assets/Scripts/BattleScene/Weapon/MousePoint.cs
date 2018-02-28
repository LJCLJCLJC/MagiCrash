using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MousePoint : MonoBehaviour {

    public Animator anim;
    private Rigidbody rigidbody;
    private Vector3 beforePos = Vector3.zero;
    private bool moved = false;
    private bool isFirstUpdate = true;
    private float nowTime;
    private int floorMask;
    private float rayLength = 100f;
    private Vector3 HitPosition;
    private bool guide = false;
    private Vector3 guidePos;

    void Start () {
        GameRoot.Instance.evt.AddListener(GameEventDefine.GUIDE_OPEN, OnGuideOpen);
        GameRoot.Instance.evt.AddListener(GameEventDefine.GUIDE_OFF, OnGuideOff);
        rigidbody = GetComponent<Rigidbody>();
        nowTime = Time.time;
        floorMask = LayerMask.GetMask("Default");
    }
	
	void Update () {

        if (Time.time - nowTime > 0.01f && guide == false)
        {
            if (isFirstUpdate)
            {
                beforePos = Input.mousePosition;
                isFirstUpdate = false;
            }
            moved = ((beforePos != Input.mousePosition)||Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));
            anim.SetBool("isMoving", moved);
            beforePos = beforePos = Input.mousePosition;
            nowTime = Time.time;
        }
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, rayLength, floorMask))
        {

            Vector3 target = floorHit.point;
            HitPosition = floorHit.point;


        }
        if (!guide)
        {
            if (!moved)
            {
                transform.DOMove(HitPosition, 1);
            }
            else
            {
                transform.DOMove(new Vector3(HitPosition.x, HitPosition.y+2, HitPosition.z), 1);
                transform.parent.LookAt(new Vector3(HitPosition.x, transform.parent.position.y, HitPosition.z));
            }
        }
        else
        {
            transform.DOMove(new Vector3(guidePos.x, HitPosition.y+2, guidePos.z),1);
        }


    }
    private void OnDestroy()
    {
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.GUIDE_OPEN, OnGuideOpen);
        GameRoot.Instance.evt.RemoveListener(GameEventDefine.GUIDE_OFF, OnGuideOff);
    }
    private void OnGuideOpen(object obj)
    {
        guide = true;
        guidePos = (Vector3)obj;
        moved = true;
    }
    private void OnGuideOff(object obj)
    {
        guide = false;
        moved = false;
    }
}
