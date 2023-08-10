using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 急にスピードを上げる能力。
/// <summary>

public class FrequentRush : MonoBehaviour
{
    float originalSpeed;
    public float rushSpeed = 10;
    public float skillCD = 3f;
    public float rushTime = 1f;

    public void Awake()
    {
        originalSpeed = GetComponentInParent<EnemyMove>().moveSpeed;
        StartCoroutine(RushToGoal());
    }

    IEnumerator RushToGoal()
    {
        while(true)
        {
            GetComponentInParent<EnemyMove>().moveSpeed = originalSpeed;
            yield return new WaitForSeconds(skillCD);

            GetComponentInParent<EnemyMove>().moveSpeed = rushSpeed;
            yield return new WaitForSeconds(rushTime);
        }
    }
}
