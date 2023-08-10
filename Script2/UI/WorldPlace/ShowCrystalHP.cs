using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// crystalHPのUI
/// <summary>

public class ShowCrystalHP : MonoBehaviour
{
    Text theText;

    public void Update()
    {
        GetComponentInChildren<Text>().text =CrystalProfile.HP.ToString();
        transform.LookAt(Camera.main.transform, Camera.main.transform.rotation * Vector3.up);
    }
}
