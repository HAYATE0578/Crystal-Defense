using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///タワーのプロフィール
////// <summary>

[System.Serializable]//クラスの活性化
public class Turret
{
    //タワーは、二つのレベルしかない。
    public GameObject m_Tower_LV1;
    public GameObject m_Tower_LV2;
    public int m_price;
    public int m_priceOfLVUP;//レベルアップするための価格
    public int attackRangeLV1;
    public int attackRangeLV2;
    public TurretType type;
}

        
public enum TurretType
{
    Laser,//レーザー射線。盾攻撃方式（盾の値が0でないとHPを減らせない）。
    Missle,//ミサイル。鎧攻撃方式（鎧が低いほど受けるダメージがより高い）。
    Cannon,//カノン。鎧攻撃方式。(ミサイルと似てるが、放物線の攻撃方式)。
    Thunder,//アーク。全体攻撃方式（生命、鎧、盾三者の数値を減らせる）。
    BrightLance,//光の槍。直接攻撃方式。（盾や鎧を無視してＨＰを直接に減らせる）。
}
