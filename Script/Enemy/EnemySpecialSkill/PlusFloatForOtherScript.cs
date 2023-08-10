using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///
/// <summary>

public class PlusFloatForOtherScript : MonoBehaviour
{
    public float intervalTime;
    public float amplitude;
    public MakeTowersDeactive theScript;

    float helpTime;

    public void Update()
    {
        if(helpTime<Time.time)
        {
            theScript.radius += amplitude;
            helpTime = Time.time + intervalTime;
        }
    }
}
