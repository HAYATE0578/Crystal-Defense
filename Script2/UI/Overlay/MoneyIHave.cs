using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// towerBuildManagerの金額を得てUI示す。
/// <summary>

public class MoneyIHave : MonoBehaviour
{
    public GameObject gameManager;

    public void Update()
    {//白い文字で示す
        GetComponent<Text>().text = "<color=white>$: " + 
            TowerBuildManager.playerMoney
            + "</color>";
    }
}
