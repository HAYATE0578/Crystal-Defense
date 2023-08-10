using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///敵のHPと最大HPの数値を得て、HPのuisliderでhpを示す。
///
/// また、sliderのmin valueとmax valueは0と1である。
/// 
/// <summary>

public class EnemyHP : MonoBehaviour
{
    EnemyProfile ep;
    Slider theSlider;
    public float enemyHPRate;
    public float nowHP;
    public float maxHP;

    public void Start()
    {
        ep = GetComponentInParent<EnemyProfile>();
        theSlider = GetComponent<Slider>();
    }

    public void Update()
    {
        CalculateValueAndSlide();
    }


    public void CalculateValueAndSlide()
    {
        nowHP = ep.nowHP;
        maxHP = ep.maxHP;

        enemyHPRate = nowHP / maxHP;
        theSlider.value = enemyHPRate;
    }

}
