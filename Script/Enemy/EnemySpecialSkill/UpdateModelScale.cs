using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の攻撃範囲が徐々に高くなる
/// <summary>

public class UpdateModelScale : MonoBehaviour
{
    public void Update()
    {
        float scaleNumber = GetComponent<MakeTowersDeactive>().radius*2;
        gameObject.transform.localScale = new Vector3(scaleNumber, scaleNumber, scaleNumber); 
    }
}
