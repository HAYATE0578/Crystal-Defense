using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の攻撃方式。敵の攻撃範囲にあるタワーが無効になる。
/// <summary>

public class MakeTowersDeactive : MonoBehaviour
{
    public float radius = 16f;

    public void Update()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        foreach (var item in towers)
        {
            float distance = (item.transform.position - transform.position).magnitude;
            if (distance < radius)
            {
                if (item.GetComponentInParent<TowerAI>() != null)
                    item.GetComponentInParent<TowerAI>().enabled = false;
                if (item.GetComponentInChildren<LineRenderer>() != null)
                    item.GetComponentInChildren<LineRenderer>().enabled = false;
            }

            else if (distance > radius)
            {
                if (item.GetComponentInParent<TowerAI>() != null)
                    item.GetComponentInParent<TowerAI>().enabled = true;
            }
        }
    }

    public void OnDestroy()//enemyが死んだ瞬間、範囲内のタワーaiを再起動
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (var item in towers)
        {
            float distance = (item.transform.position - transform.position).magnitude;

            if (distance < radius)
            {
                if (item.GetComponentInParent<TowerAI>() != null)
                    item.GetComponentInParent<TowerAI>().enabled = true;
            }
        }
    }

    //gizmosの関数で半径の長さを確認
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
