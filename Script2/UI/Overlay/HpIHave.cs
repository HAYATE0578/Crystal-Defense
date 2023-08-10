using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///crystalのHPを示す
/// <summary>

public class HpIHave : MonoBehaviour
{
    public void Update()
    {//白い文字で示す
        GetComponent<Text>().text = "<color=white>HP: " +
            CrystalProfile.HP
            + "</color>";
    }
}
