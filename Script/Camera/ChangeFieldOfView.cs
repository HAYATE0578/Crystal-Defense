using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///マウスホイールを操作して、カメラの視野を変える
////// <summary>

public class ChangeFieldOfView : MonoBehaviour
{
    public float viewMAX = 90;
    public float viewMIN = 50;
    public float nowView = 70;
    public void Update()
    {
        nowView = GetComponent<Camera>().fieldOfView;

        //操作して、マウスホイールの数値を変える。
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            nowView = GetComponent<Camera>().fieldOfView - 2;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            nowView = GetComponent<Camera>().fieldOfView + 2;
        }

        //最大最小の制限
        if (nowView > viewMAX)
        {
            nowView = viewMAX;
        }
        else if(nowView < viewMIN)
        {
            nowView = viewMIN;
        }

        //最後には算出した数を入力
        GetComponent<Camera>().fieldOfView = nowView;
    }
}
