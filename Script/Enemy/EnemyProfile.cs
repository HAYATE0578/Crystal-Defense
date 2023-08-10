using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 敵の生命、鎧、盾の数値を管理するスクリプト
/// <summary>

public class EnemyProfile : MonoBehaviour
{
    public int nowHP = 100;
    public int maxHP;
    public float nowArmor = 100; //比率でダメージを計算するので、float
    public float maxArmor;
    public float nowShield = 100;
    public float maxShield;

    public int dropMoney = 100;
    public GameObject dropMoneyUI;
    GameObject theDropMoneyUI;

    public bool isBoss;
    public bool isAttacked;
    private float moveSpeed;
    private bool isAttackedByCannonOrMissle;
    private float speedRecoverStartTime;
    public float speedRecoverTime = 3f;

    public GameObject deadEffect;
    public AudioClip[] spawnAudio;
    public AudioClip attackedAudio;
    public AudioClip dropMoneyAudio;
    AudioSource theAS;
    LineRenderer theLineRend;
    float intervelTime = 2;
    float helpTime;

    public void Update()
    {
        RecoverSpeedSituation();
        CheckIsBeingAttackedAndTurnOffLineRend();
        Death();
        theDropMoneyUI.GetComponent<DropMoney>().dropMoney = dropMoney;
    }

    public void Awake()
    {
        maxHP = nowHP;//最初の時、最大値を得る
        maxArmor = nowArmor;
        maxShield = nowShield;
        moveSpeed = GetComponent<EnemyMove>().moveSpeed;
        theLineRend = GetComponent<LineRenderer>();
        theAS = GetComponent<AudioSource>();
        theDropMoneyUI = dropMoneyUI;
        MakeDropMoneyRandomlyOnAwake();

        if (spawnAudio.Length > 0)
        {
            int temp = Random.Range(0, spawnAudio.Length);

            if (spawnAudio[temp] != null
                && this.gameObject != null
                && Camera.main != null
                && Camera.main.GetComponent<AudioSource>() != null
                )
                Camera.main.GetComponent<AudioSource>().PlayOneShot(spawnAudio[temp]);
        }
    }


    public void ShieldAttack(int atk, int atk_SD)
    {
        isAttacked = true;
        if (nowShield > 0)
        {
            nowShield -= atk_SD;
        }
        else if (nowShield <= 0)
        {
            nowHP -= atk;
        }
        Death();
    }

    public void ArmorAttack(int atk, int atk_AR)
    {

        isAttacked = true;
        if (theAS)
            theAS.PlayOneShot(attackedAudio);
        // 鎧の数値を考えて、敵のHPを削減
        // まず鎧の数値を「鎧への攻撃力」で削減
        if (nowArmor > 0)
        {
            nowArmor -= atk_AR;

            //鎧の数値を考えて「敵への攻撃力」で削減
            float temp = atk * (1 - nowArmor / maxArmor);
            print("fater armor atk: " + temp);
            nowHP -= (int)temp;
        }
        else//マイナスのarにならないように
        {
            print("direct atk without armor ");
            nowArmor = 0;
            nowHP -= atk;
        }

        speedRecoverStartTime = 0;//撃てられたら回復スタート時間を再計算

        isAttackedByCannonOrMissle = true;

        Death();
    }

    public void OverallAttack(int atk, int atk_SD, int atk_AR)
    {

        isAttacked = true;
        nowHP -= atk;
        nowShield -= atk_SD;
        nowArmor -= atk_AR;
        Death();
    }

    public void DirectAttack(int atk)
    {

        isAttacked = true;
        nowHP -= atk;
        Death();
    }

    private void OnDestroy()
    {
        //enemyの死亡場合は二つ：１．ctystalに到着　２．攻撃を受けて消失

        //この関数は極めて重要。直接にdeath（）にalivingEnemyCount--を書いたら、
        //Unityエンジンの特性により、alivingEnemyCountはマイナスになる可能性がある。
        EnemySpawner.alivingEnemyCount--;

        //攻撃を受けて死亡したら
        if(nowHP<=0)
        {
            TowerBuildManager.playerMoney += dropMoney;
            GameObject temp = Instantiate(theDropMoneyUI, transform.position, transform.rotation);
            temp.GetComponent<DropMoney>().dropMoney = dropMoney;
            dropMoney = 0;//二度とプラスの場合を防止
        }
    }

    public void Death()
    {
        if (nowHP <= 0 && gameObject != null)
        {

            if (deadEffect != null)
            {
                GameObject tempEffect = Instantiate(deadEffect, transform.position, transform.rotation);
                Destroy(tempEffect, 2f);
            }

            if (Camera.main.GetComponent<AudioSource>() && dropMoneyAudio)
                Camera.main.GetComponent<AudioSource>().PlayOneShot(dropMoneyAudio);

            Destroy(this.gameObject);
        }
    }

    public void RecoverSpeedSituation()
    {
        if (isAttackedByCannonOrMissle && !isBoss)
        {
            if (speedRecoverStartTime < speedRecoverTime)
            {
                speedRecoverStartTime += Time.deltaTime;
                GetComponent<EnemyMove>().moveSpeed = moveSpeed * 1 / 2;
            }
            else
            {
                isAttackedByCannonOrMissle = false;
                GetComponent<EnemyMove>().moveSpeed = moveSpeed;
            }
        }
    }

    public void CheckIsBeingAttackedAndTurnOffLineRend()
    {
        if (helpTime < Time.time)
        {
            isAttacked = false;
            helpTime = Time.time + intervelTime;
        }

        if (isAttacked == false)
        {
            theLineRend.enabled = false;
        }
    }

    public void MakeDropMoneyRandomlyOnAwake()
    {
        int temp = Random.Range(1, 101);//範囲は[1, 100]

        //50% 原価
        if (temp <= 10) dropMoney = dropMoney + 3;
        else if (10 < temp && temp <= 20) dropMoney = dropMoney + 4;
        else if (20 < temp && temp <= 30) dropMoney = dropMoney + 5;
        else if (30 < temp && temp <= 40) dropMoney = dropMoney + 6;
        else if (40 < temp && temp <= 50) dropMoney = dropMoney + 7;

        else if (50 < temp && temp <= 55) dropMoney = dropMoney * 2 + 2;        //5％ 2倍
        else if (55 < temp && temp <= 60) dropMoney = dropMoney * 1 / 2 + 3;     //5％ 1/2
        else if (60 < temp && temp <= 65) dropMoney = dropMoney * 3+ 4;        //5％ 3倍
        else if (65 < temp && temp <= 70) dropMoney = dropMoney * 1 / 3 + 5;     //5％ 1/3

        else if (70 < temp && temp <= 90) dropMoney = dropMoney*3/20 + 2;　//20％　0.15倍

        else if (90 < temp && temp <= 98) dropMoney = dropMoney*13 / 10 +6;　//8％ 1.3倍
        else if (98 < temp && temp <= 100) dropMoney = dropMoney*15 +7;　//2％　15倍

        //期待値は約1.035
    }
}
