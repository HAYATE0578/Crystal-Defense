using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///このスクリプトにアタッチされたeffectは自動的に自らの中心に回す。
/// <summary>

public class SpawnPointRotate : MonoBehaviour
{
    public float rotateSpeed = 5f;
    public void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
