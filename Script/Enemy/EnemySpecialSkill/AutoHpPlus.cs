using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// enemyは、自動的に数値が回復。
/// <summary>

public class AutoHpPlus : MonoBehaviour
{
    public float intervalTime;

    public float plusHP;

    public void Awake()
    {
        StartCoroutine(Stealth(intervalTime));
    }

    IEnumerator Stealth(float interval)
    {
        while (true)
        {
            //もし生命は最大値より少ないなら自動的に回復
            if(GetComponentInChildren<EnemyProfile>().nowHP< GetComponentInChildren<EnemyProfile>().maxHP)
            {
                GetComponentInChildren<EnemyProfile>().nowHP += (int)plusHP;
            }
            yield return new WaitForSeconds(interval);
        }
    }
}
