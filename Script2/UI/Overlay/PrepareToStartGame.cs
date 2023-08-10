using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// gamestartとUIの管理
/// <summary>

public class PrepareToStartGame : MonoBehaviour
{
    public GameObject[] gameManager;
    public GameObject UIVolume;
    public AudioClip bgmInThisScene;
    public int preparationTime = 30;

    float helpTime;
    bool isGameStarted = false;

    //このゲームにはenemyが死んだ瞬間にenemyを生み出す能力を持つので、delete関数で残った敵のある場合を防止。
    public void Awake()
    {
        DeleteEnemies();
        EnemySpawner.alivingEnemyCount = 0;
    }

    public void Start()
    {
        DeleteEnemies();
        Invoke("DeleteEnemies", 0.1f);

        EnemySpawner.alivingEnemyCount = 0;
    }

    public void Update()
    {
        print(preparationTime);
        GameStart();//準備時間でチェック
        CalculateAndShowPreTime();//残った準備時間を計算し示す。
        GetAndPlayBGM();
    }

    public void GetAndPlayBGM()
    {
        if(isGameStarted)
        {
            Camera.main.GetComponent<AudioSource>().clip = bgmInThisScene; //bgmを得る
            Camera.main.GetComponent<AudioSource>().loop = true;//循環
            Camera.main.GetComponent<AudioSource>().Play();
        }
    }

    public void OnStartButtonSelected()
    {
        //準備時間をゼロに、スタート！
        preparationTime = 0;
    }

    public void GameStart()
    {
        if (preparationTime <= 0)
        {
            foreach (var item in gameManager)
            {
                item.GetComponent<EnemySpawner>().enabled = true;
            }

            print("Game Start! ");

            this.gameObject.SetActive(false);
        }

        isGameStarted = true;
    }

    public void CalculateAndShowPreTime()
    {
        if (helpTime < Time.time)
        {
            preparationTime--;
            helpTime = Time.time + 1;
        }

        GetComponentInChildren<Text>().text =
                "Preparation time: " + preparationTime;
    }

    public void DeleteEnemies()
    {
        GameObject[] notDeletedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (notDeletedEnemies.Length < 0) return;
        foreach (var item in notDeletedEnemies)
        {
            Destroy(item);
        }
    }
}
