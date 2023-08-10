using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///敵のARと最大ARの数値を得て、ARのuisliderでhpを示す。
///
/// また、sliderのmin valueとmax valueは0と1である。
/// 
/// <summary>

public class EnemyAR : MonoBehaviour
{
    EnemyProfile ep;
    Slider theSlider;
    public float enemyARRate;
    public float nowAR;
    public float maxAR;

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
        nowAR = ep.nowArmor;
        maxAR = ep.maxArmor;

        enemyARRate = nowAR / maxAR;
        theSlider.value = enemyARRate;
    }

}
