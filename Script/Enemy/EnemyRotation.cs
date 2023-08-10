using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 方向を決める。
/// Enemyの物体（モデル）にアタッチ。
/// <summary>

public class EnemyRotation : MonoBehaviour
{
    public Transform[] points;
    public int index = 0; //目標配列のに使う
    public void Start()
    {
        points = FindPointsInRoad.points;//ルートを得る
        //awakeはstartより速いので、まずfindのスクリプトでポイントを得て、ここでstartで座標を教える。
        //transform.LookAt(points[0]);//初めての目標を見る
    }
    public void Update()
    {
        DecideRotation();
    }

    public void DecideRotation()
    {
        if (index == points.Length) return; //到着したら退出。
        if (transform == null || gameObject == null) return;

            if ((points[index].position - transform.position).magnitude < 1f)
            {
                index++;//もし近づいたらindex+1。
            }


        if (index != points.Length)
            transform.rotation = Quaternion.LookRotation(points[index].position - transform.position);
    }
}
