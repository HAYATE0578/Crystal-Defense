using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///敵のSDと最大SDの数値を得て、SDのuisliderでSDを示す。
///
/// また、sliderのmin valueとmax valueは0と1である。
/// 
/// <summary>

public class EnemySD : MonoBehaviour
{
    EnemyProfile ep;
    Slider theSlider;
    public float enemySDRate;
    public float nowSD;
    public float maxSD;

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
        nowSD = ep.nowShield;
        maxSD = ep.maxShield;

        enemySDRate = nowSD / maxSD;
        theSlider.value = enemySDRate;
    }

}
