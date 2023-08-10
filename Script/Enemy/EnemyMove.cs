using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///ルートのポイントを敵の移動するゴールと設定し、敵を移動させる。
///Enemyの空きの物体（father）にアタッチ。
/// <summary>

public class EnemyMove : MonoBehaviour
{
    public Transform[] points;
    public int index = 0; //目標配列のに使う
    public float moveSpeed = 5;
    public GameObject enemyAttackEffect;
    public AudioClip enemyAttackAudio;

    public void Start()
    {
        points = FindPointsInRoad.points;//ルートを得る
        //awakeはstartより速いので、まずfindのスクリプトでポイントを得て、ここでstartで座標を教える。
        //transform.LookAt(points[0]);//初めての目標を見る
    }

    public void Update()
    {
        MakeEnemyMove();
        DestroyThisObjectAndAttackCrystal();
    }

    public void MakeEnemyMove()
    {
        if (index == points.Length) return; //到着したら退出。

        if (transform == null || gameObject == null) return;

        if ((points[index].position - transform.position).magnitude < 1f)
        {
            index++;//もし近づいたらindex+1。
        }
        else
        {
            transform.Translate(
            (points[index].position - transform.position).normalized//ベクトル標準化
            * Time.deltaTime * moveSpeed);//移動させる。
        }

    }
    public void DestroyThisObjectAndAttackCrystal()
    {
        if (index == points.Length)//到着
        {

            //普通のモンスターの攻撃力は１で、bossは10。
            if (this.gameObject.GetComponent<EnemyProfile>().isBoss)
            {
                CrystalProfile.HP -= 10;
            }
            else
            {
                CrystalProfile.HP--;
            }

            //audioとeffectの実現
            if (enemyAttackEffect != null)
            {
                GameObject tempEffect =
                    Instantiate(enemyAttackEffect, transform.position, transform.rotation);
                Destroy(tempEffect, 3f);
            }
            if (enemyAttackAudio != null)
                Camera.main.GetComponent<AudioSource>().PlayOneShot(enemyAttackAudio);

            Destroy(this.gameObject);
        }
    }
}
