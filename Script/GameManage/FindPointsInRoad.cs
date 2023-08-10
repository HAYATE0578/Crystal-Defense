using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 配列でポイントをさがし、敵の移動ルートを設定する。
/// <summary>

public class FindPointsInRoad : MonoBehaviour
{
    public static Transform [] points;
    public static int alivingEnemyCount;
    //ルートのポイントの配列。staticと設定し、敵がポイントの座標を得られる。

    public void Awake()
    {
        FindPoints();
        //awakeはstartより速いので、ここはawake。
    }

    public void FindPoints()
    {
        points = new Transform[transform.childCount];//配列をつくる
        for(int i = 0; i<transform.childCount; i++)
        {
            points[i] =transform.GetChild(i);//自動的に座標を与える
        }
    }
}
