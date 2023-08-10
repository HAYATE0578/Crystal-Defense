using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// crystalは10回に攻撃されたら、ゲームは終わる。
/// <summary>

public class CrystalProfile : MonoBehaviour
{
    public static int HP = 10;

    public int startHP = 10;

    public void Start()
    {
        HP = startHP;
    }

    public void Update()
    {
        if (HP <= 0)
        {
            HP = 0;
            Invoke("PauseGame", 4);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0.2f;
    }
}
