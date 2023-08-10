using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 多くの敵が削除された瞬間に敵を生み出す。scenechangeのとき、生み出した敵をもう一度削除する。
/// <summary>

public class ClearEnemyInStartScene : MonoBehaviour
{
    public void Awake()
    {
        Time.timeScale = 1;//gameoverのとき、timescaleは１より小さいくなるので、sceneをsceneチェンジする時１に戻せる
    }

    public void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length>0)
        {
            foreach(var item in enemies)
            {
                Destroy(item);
            }
        }

    }
}
