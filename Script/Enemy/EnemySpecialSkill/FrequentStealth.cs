using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 少し時間をへて、enemyはステルス状態になる（missleとcannonに攻撃されできない）。
/// <summary>

public class FrequentStealth : MonoBehaviour
{
    public float intervalTime;

    SkinnedMeshRenderer theSkinRen;
    Collider theCol;
    bool isEnemy = true;

    public void Awake()
    {
        theSkinRen = GetComponentInChildren<SkinnedMeshRenderer>();
        theCol = GetComponentInChildren<Collider>();
        StartCoroutine(Stealth(intervalTime));
    }

    IEnumerator Stealth(float interval)
    {
        while(true)
        {
            isEnemy = !isEnemy;
            if (isEnemy) this.gameObject.tag = "Enemy";
            else if (!isEnemy) this.gameObject.tag = "Untagged";
            
            theSkinRen.enabled = !theSkinRen.enabled;
            theCol.enabled = !theCol.enabled;
            

            yield return new WaitForSeconds(interval);
        }
    }


}
