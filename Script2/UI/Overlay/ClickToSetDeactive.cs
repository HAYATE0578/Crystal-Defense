using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UIをチェンジするため、使うスクリプト
/// <summary>

public class ClickToSetDeactive : MonoBehaviour
{
    public GameObject whichIWannaDeactive;

    public void OnClickToDeactive()
    {
        whichIWannaDeactive.SetActive(!whichIWannaDeactive.activeSelf);
    }

}
