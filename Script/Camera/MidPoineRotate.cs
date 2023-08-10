using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// マウスの右ボタンを押して、カメラの角度を変える。
/// <summary>

public class MidPoineRotate : MonoBehaviour
{
    public GameObject midPoint;  //rotateの点
    public float rotateSpeedLeftRight = 240;
    public float mouseX; //マウスの移動で獲得する数

    public void Update()
    {
        MakeCameraRotateAroundMidPoint();
    }

    public void MakeCameraRotateAroundMidPoint()
    {
        if (Input.GetMouseButton(1))//右ボタンを押しつつ
        {
            mouseX = Input.GetAxis("Mouse X");　//マウスの移動で獲得する数
            mouseX *= Time.deltaTime * rotateSpeedLeftRight;

            //角度を変える
            this.transform.RotateAround(midPoint.transform.position,Vector3.up, mouseX);
        }
    }
}
