using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UIをカメラに向かわせるスクリプト
/// <summary>

public class UILookatMainCamera : MonoBehaviour
{
    Camera mainCamera;

    public void Awake()
    {
        mainCamera = Camera.main;

    }

    private void Update()
    {
        if(transform!=null && mainCamera!=null)
        transform.LookAt(mainCamera.transform, mainCamera.transform.rotation * Vector3.up);
        transform.rotation = 
            Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(Camera.main.transform.position - transform.position), 
            10 * Time.deltaTime);

    }
}
