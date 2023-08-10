using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///enemyが撃破されたとき、enemyを生み出す！
/// 二種類のenemy特殊能力を実現。
/// １．出産：大きな蜘蛛が死んだら、小さくてスピードが速い蜘蛛を生み出す。
/// ２．復活：一度殺されたら、もう一度このenemyを作る。
/// <summary>

public class OnDestroyMakeEnemy : MonoBehaviour
{
    public float newEnemySpeed;
    public GameObject newEnemy;
    public GameObject effect;

    public void OnDestroy()
    {
        MakeNewEnemies();
    }

    public void MakeNewEnemies()
    {
        GameObject temp = Instantiate(newEnemy, transform.position, transform.rotation);

        EnemySpawner.alivingEnemyCount++;

        GameObject birthEffect = new GameObject { };
        if (effect!=null)
        {
            birthEffect = Instantiate(effect, transform.position, transform.rotation);
            Destroy(birthEffect, 3f);
        }   

        //目標のポイントや速度を生み出したenemyに教える
        temp.GetComponentInChildren<EnemyMove>().index = this.gameObject.GetComponent<EnemyMove>().index;
        temp.GetComponentInChildren<EnemyMove>().moveSpeed = newEnemySpeed;
    }
}
