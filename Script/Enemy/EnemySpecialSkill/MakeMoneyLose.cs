using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///　敵の攻撃方式。敵の攻撃範囲にあるタワーがあるとき、プレイヤーの金が減らされる
/// <summary>

public class MakeMoneyLose : MonoBehaviour
{
    public float radius = 16f;
    public int loseMoney;
    public float intervalTime;
    public AudioClip loseMoneyADO;
    float helpTime;

    public void Update()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        int inRangeTowerCount = 0;

        foreach (var item in towers)
        {
            float distance = (item.transform.position - transform.position).magnitude;
            if (distance < radius)
            {
                inRangeTowerCount++;
            }
        }

        if(helpTime<Time.time)
        {
            if(TowerBuildManager.playerMoney>0)
            TowerBuildManager.playerMoney -= inRangeTowerCount*loseMoney;
            if(loseMoneyADO)
            Camera.main.GetComponent<AudioSource>().PlayOneShot(loseMoneyADO);
            helpTime = Time.time + intervalTime;
        }

    }

    //gizmosの関数で半径の長さを確認
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
