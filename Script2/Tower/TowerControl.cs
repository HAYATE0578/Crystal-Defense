using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///選択されたキューブに、建てられたタワーを記録。
/// <summary>

public class TowerControl : MonoBehaviour
{
    //タワーを建設、昇格、削除するための補助。
    public int LV; // 0はなし、2はlv1、2はlv2
    public Turret theTurret;
    public GameObject tower;
    public float attackRange;

    public void Update()
    {
        if (theTurret != null && LV ==1)
            attackRange = theTurret.attackRangeLV1;
        if(theTurret != null && LV == 2)
            attackRange = theTurret.attackRangeLV2;
    }
}
