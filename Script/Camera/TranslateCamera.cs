using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///範囲内にWSADでカメラの移動をコントロール。
/// <summary>

public class TranslateCamera : MonoBehaviour
{
    public float MoveSpeed = 20;
    public float MaxMoveRange = 50;
    public GameObject midPoint;
    private float dir2MidPointLength;
    public void Update()
    {
        TranslateTheCamera();
        DontMakeCameraExceed();
    }

    public void TranslateTheCamera()
    {
        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
        if (Input.GetKey(KeyCode.S)) transform.Translate(-Vector3.forward * Time.deltaTime * MoveSpeed);
        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
        if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
    }
    public void DontMakeCameraExceed()
    {
        //無限に移動させないため、範囲を超えたら戻させる。
        if (midPoint != null)
        {
            dir2MidPointLength = (transform.position - midPoint.transform.position).magnitude;
            if (dir2MidPointLength > MaxMoveRange)
                transform.position = Vector3.MoveTowards
                    (transform.position, midPoint.transform.position, Time.deltaTime * MoveSpeed);
        }

    }
}
