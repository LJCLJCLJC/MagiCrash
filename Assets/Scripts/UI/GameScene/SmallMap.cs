using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SmallMap : MonoBehaviour {
    public Image map;
    private float centerX;
    private float centerY;
    private float width;
    private float height;
    private float playerX;
    private float playerY;
    // Use this for initialization
    void Start ()
    {
        centerX = (GameController.Instance.RightTop.position.x + GameController.Instance.leftButton.position.x) / 2;
        centerY = (GameController.Instance.RightTop.position.z + GameController.Instance.leftButton.position.z) / 2;
        width = GameController.Instance.RightTop.position.x - GameController.Instance.leftButton.position.x;
        height = GameController.Instance.RightTop.position.z - GameController.Instance.leftButton.position.z;


    }
    void Update () {
        playerX = GameController.Instance.tsPlayer.position.x - centerX;
        playerY = GameController.Instance.tsPlayer.position.z - centerY;
        map.transform.localPosition = new Vector3((playerX / width) * map.rectTransform.sizeDelta.x, (playerY / height) * map.rectTransform.sizeDelta.y, 0) * -1;
    }
}
