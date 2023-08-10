using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// GameMangerという空き物体にアタッチ。
/// <summary>

public class EnemySpawner : MonoBehaviour
{
    public WaveEnemy[] waveOfEnemies;//毎回の敵
    public static int alivingEnemyCount;
    Transform birthPoint;
    public float waveIntervalTime = 10;
    public bool isWin = false;

    private float nowTime;
    public float moneyAutoPlusIntervalTime = 1;

    public void Awake()
    {
        Time.timeScale = 1;//gameoverのとき、timescaleは１より小さいくなるので、sceneをsceneチェンジする時１に戻せる
        GetComponent<EnemySpawner>().enabled = false;
    }

    public void Start()
    {

        birthPoint = FindPointsInRoad.points[0];//birthpointを獲得。
        StartCoroutine(MakeManyEnemy());
    }

    public void Update()
    {
        print("alivingEnemies: \t"+alivingEnemyCount);
        MoneyAutoPlus();
    }

    IEnumerator MakeManyEnemy()
    {
        for (int i = 0; i < waveOfEnemies.Length; i++)
        {//毎回の敵が作り出されると、循環が終わる。
            for (int y = 0; y < waveOfEnemies[i].m_countOfEnemy; y++)
            {
                //数量に達すると、この循環が終わる。
                Instantiate(waveOfEnemies[i].m_enemy, birthPoint.position, Quaternion.identity);
                alivingEnemyCount++;
                yield return new WaitForSeconds(waveOfEnemies[i].m_intervalTime);
            }
            while (alivingEnemyCount > 0) yield return -1;//敵がいる限り、今回が終わらない
            yield return new WaitForSeconds(waveIntervalTime);
        }

        //すべての敵が生成した。
        while (alivingEnemyCount>0)//敵がいれば、勝利できないはず。
        {
             yield return 0; 
        }
        
        //if (CrystalProfile.HP > 0
        //    && alivingEnemyCount<=0)

        while(CrystalProfile.HP<=0) yield return 0;
        isWin = true; //敵がいない、同時にcrystalのHPは正数であれば、勝利になる。勝利のUIが降りる。

    }

    public void MoneyAutoPlus()
    {
        //時間を経て、お金が自動的に増加する。
        if (Time.time > nowTime)
        {
            TowerBuildManager.playerMoney++;
            nowTime = Time.time + moneyAutoPlusIntervalTime;
        }
    }
}
