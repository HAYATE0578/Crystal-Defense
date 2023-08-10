using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// もしゲームが負けたら、gameoverというUIが下がる。
/// <summary>

public class HP0GameOver : MonoBehaviour
{
    public float moveRangeRatio= 0.2f;
    Vector3 mainScreenMidPoint;

    public void Awake()
    {
        mainScreenMidPoint = GetComponentInParent<Canvas>().gameObject.transform.position;
    }
    public void FixedUpdate()
    {
        if (CrystalProfile.HP <= 0)
            transform.position = Vector3.Lerp(transform.position, mainScreenMidPoint, moveRangeRatio);
    }
}
