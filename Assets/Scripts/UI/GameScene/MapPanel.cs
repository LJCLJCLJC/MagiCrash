using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : BasePanel {

    public Image playerIcon;
    public Image map;
    private float centerX;
    private float centerY;
    private float width;
    private float height;
    private float playerX;
    private float playerY;
    public override void OnEnter()
    {
        base.OnEnter();
        centerX = (GameController.Instance.RightTop.position.x + GameController.Instance.leftButton.position.x)/2;
        centerY = (GameController.Instance.RightTop.position.z + GameController.Instance.leftButton.position.z)/ 2;
        width = GameController.Instance.RightTop.position.x - GameController.Instance.leftButton.position.x;
        height = GameController.Instance.RightTop.position.z - GameController.Instance.leftButton.position.z;
        playerX = GameController.Instance.tsPlayer.position.x - centerX;
        playerY = GameController.Instance.tsPlayer.position.z - centerY;
        playerIcon.transform.localPosition = new Vector3((playerX / (width / 2))*450, (playerY / (height / 2)) * 450, 0);
    }

}
