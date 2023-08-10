using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///敵の種類と数量をコントロールクラスである。
/// <summary>

[System.Serializable]//活性化
public class WaveEnemy
{
    public GameObject m_enemy;//毎回の敵の種類
    public int m_countOfEnemy;//その数
    public float m_intervalTime;//間隔時間
}
