using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///　頻繫に子enemyを作り出す。
/// <summary>

public class FrequentMakeEnemy : MonoBehaviour
{
    public float newEnemySpeed;
    public GameObject newEnemy;
    public GameObject effect;
    public float makeEnemyCD = 3;

    public void Awake()
    {
        StartCoroutine(MakeSon());
    }

    IEnumerator MakeSon()
    {
        while(true)
        {
            yield return new WaitForSeconds(makeEnemyCD);
            MakeNewEnemies();
        }
    }

    public void MakeNewEnemies()
    {
        GameObject temp = Instantiate(newEnemy, transform.position, transform.rotation);

        EnemySpawner.alivingEnemyCount++;

        GameObject birthEffect = new GameObject { };
        if (effect != null)
        {
            birthEffect = Instantiate(effect, transform.position, transform.rotation);
            Destroy(birthEffect, 3f);
        }

        //目標のポイントや速度を生み出したenemyに教える
        temp.GetComponentInChildren<EnemyMove>().index = this.gameObject.GetComponent<EnemyMove>().index;
        temp.GetComponentInChildren<EnemyMove>().moveSpeed = newEnemySpeed;
    }
}
