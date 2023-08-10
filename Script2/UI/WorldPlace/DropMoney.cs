using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///
/// <summary>

public class DropMoney : MonoBehaviour
{
    public int dropMoney;
    string theText;

    public void Start()
    {
        theText = "+" + dropMoney.ToString();
        GetComponentInChildren<Text>().text = theText;
        Destroy(gameObject, 2f);
    }

    public void Update()
    {


        transform.LookAt(Camera.main.transform, Camera.main.transform.rotation * Vector3.up);
        transform.Translate(Vector3.up * Time.deltaTime*30);
    }

}
