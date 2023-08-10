using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///お金の金額より、価格は白い文字か赤い文字かを決める。
/// <summary>

public class IfPriceGreaterMoneyRed : MonoBehaviour
{
    public int thisPrice;
    public GameObject gameManager;
    private int compareMoney;
    public TurretType theType;

    public void Awake()
    {
        GetPriceOfTurret();
    }

    public void Update()
    {
        GetMoneyNum();
        CheckColor();
    }

    public void GetPriceOfTurret()
    {//gamemanagerで、タワーの価格を獲得
        if(theType.ToString()==TurretType.Laser.ToString())
        {
            thisPrice = gameManager.GetComponent<TowerBuildManager>().towerLaser.m_price;
        }
        else if (theType.ToString() == TurretType.Cannon.ToString())
        {
            thisPrice = gameManager.GetComponent<TowerBuildManager>().towerCannon.m_price;
        }
        else if(theType.ToString() == TurretType.Missle.ToString())
        {
            thisPrice = gameManager.GetComponent<TowerBuildManager>().towerMissle.m_price;
        }
        else if(theType.ToString() == TurretType.Thunder.ToString())
        {
            thisPrice = gameManager.GetComponent<TowerBuildManager>().towerThunder.m_price;
        }
        else if(theType.ToString() == TurretType.BrightLance.ToString())
        {
            thisPrice = gameManager.GetComponent<TowerBuildManager>().towerBrightLance.m_price;
        }
    }

    public void GetMoneyNum()
    {//持っている金額を獲得
        compareMoney = TowerBuildManager.playerMoney;
    }

    public void CheckColor()
    {
        if(thisPrice<=compareMoney)//お金が足りるなら、大きく白い文字で表示。
        {
            GetComponent<Text>().text = "<color=white>$" + thisPrice + "</color>";
            GetComponent<Text>().fontSize = 16;
        }
        else if (thisPrice > compareMoney)//お金が足りないなら、小さく赤い文字で表示。
        {
            GetComponent<Text>().text = "<color=red>$" + thisPrice + "</color>";
            GetComponent<Text>().fontSize = 14;
        }
    }
}
