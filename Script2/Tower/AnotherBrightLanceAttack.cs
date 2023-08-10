using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 光槍のもう一つの攻撃方式。長い長方形のcolliderに接触する敵は全部攻撃を受ける。
/// <summary>

public class AnotherBrightLanceAttack : MonoBehaviour
{
    
    public int atk;
    public List<GameObject> brightLanceAttackedEnemies = new List<GameObject>();


    public void Awake()
    {
        atk = GetComponentInParent<TowerAI>().atk;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
            brightLanceAttackedEnemies.Add(col.gameObject);
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
            brightLanceAttackedEnemies.Remove(col.gameObject);
    }

    public void FixedUpdate()
    {
        BrightLanceAttack();
        RefreshTheList();

    }

    public void BrightLanceAttack()
    {
        foreach (var item in brightLanceAttackedEnemies)
        {
            if (item != null)
            {
                item.GetComponent<EnemyProfile>().DirectAttack(atk);
            }
        }
    }

    public void RefreshTheList()
    {

        //新しいリストで、nullのリスト対象（missingの死んだ敵）を排除する。
        List<int> theIndex = new List<int>();
        for (int i = 0; i < brightLanceAttackedEnemies.Count; i++)
        {
            if (brightLanceAttackedEnemies[i] == null)
            {
                theIndex.Add(i);
            }//インデクスを保存
        }
        for (int i = 0; i < theIndex.Count; i++)
        {
            brightLanceAttackedEnemies.RemoveAt(theIndex[i] - i);
        }//インデクスによりnullのリスト対象を排除。
    }
}
