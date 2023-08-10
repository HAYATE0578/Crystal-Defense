using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///このシーンにいるすべての敵を消滅したら、nextsceneとWINのUIが降りる。
/// <summary>

public class GameWinUIDown : MonoBehaviour
{
    public GameObject gameManager;

    public float moveRangeRatio = 0.2f;
    Vector3 mainScreenMidPoint;

    public void Awake()
    {
        mainScreenMidPoint = GetComponentInParent<Canvas>().gameObject.transform.position;
    }
    public void FixedUpdate()
    {
        if (gameManager.GetComponent<EnemySpawner>().isWin)
            transform.position = Vector3.Lerp(transform.position, mainScreenMidPoint, moveRangeRatio);
    }
}
