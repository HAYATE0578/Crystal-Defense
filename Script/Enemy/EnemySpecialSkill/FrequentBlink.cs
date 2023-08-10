using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// blinkで、missleやcannonを騙す能力。
/// <summary>

public class FrequentBlink : MonoBehaviour
{
    float originalSpeed;
    public float blinkSpeed = 10;
    public float blinkTime = 1f;
    public float waitTime = 0.2f;
    public float skillCD = 3f;



    public void Awake()
    {
        originalSpeed = GetComponentInParent<EnemyMove>().moveSpeed;
        StartCoroutine(BlinkToGoal());
    }

    IEnumerator BlinkToGoal()
    {
        while (true)
        {
            //blinkを待つ
            GetComponentInParent<EnemyMove>().moveSpeed = originalSpeed;
            yield return new WaitForSeconds(skillCD);

            //blinkの途中
            GetComponentInParent<EnemyMove>().moveSpeed = blinkSpeed;
            yield return new WaitForSeconds(blinkTime);

            //blinkの目的地で弾丸を騙す。
            GetComponentInParent<EnemyMove>().moveSpeed = originalSpeed;
            yield return new WaitForSeconds(waitTime);

            //blink back
            GetComponentInParent<EnemyMove>().moveSpeed = -blinkSpeed;
            yield return new WaitForSeconds(waitTime);

        }
    }
}
