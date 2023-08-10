using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///タワーの攻撃する範囲を示す。
/// <summary>

public class ToCheckAttackRange : MonoBehaviour
{
    public float atkRange = 1;
    public int pointsCount = 10; //多ければ円はよりスムーズであるはず
    private float calculateAngle;
    private List<Vector3> pointsList;
    public LineRenderer renderer;
    private bool isKeyOn;

    public Ray checkRay;
    public RaycastHit hittedTowerBasicCube;
    public LayerMask towerBasicCubeMask;
    public GameObject selectedTower;

    public void Awake()
    {
        calculateAngle = 360 / pointsCount;
        pointsList = new List<Vector3>();
    }

    public void Update()
    {
        checkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(checkRay, out hittedTowerBasicCube, 1000, towerBasicCubeMask.value)
            )
        {
            selectedTower = hittedTowerBasicCube.collider.gameObject.
                GetComponent<TowerControl>().tower;
            renderer = selectedTower.GetComponent<LineRenderer>();
        }
        else if (renderer != null)
        {
            pointsList.Clear();
            renderer.positionCount = 0;
            renderer = null;
        }
        GetAtkRange();
        DrawTheCircle();
    }

    public void GetAtkRange()
    {
        if (
            selectedTower!=null
            &&selectedTower.GetComponentInChildren<SphereCollider>() != null)
            atkRange = selectedTower.GetComponentInChildren<SphereCollider>().radius;
    }

    public void AddPointsToList()
    {
        for (int i = 0; i < pointsCount; i++)
        {
            //Vector3 point = transform.position+transform.up*5//キューブに遮られないため、少し上に移動
            //    + transform.forward * atkRange;

            Quaternion r = selectedTower.transform.rotation;
            //点への方向の角度を計算
            Quaternion v = Quaternion.Euler(
                r.eulerAngles.x,
                r.eulerAngles.y - (calculateAngle * i),
                r.eulerAngles.z);

            //点々の座標を計算、リストに記入。
            Vector3 point = selectedTower.transform.position + selectedTower.transform.up * 1.2f
                + v * selectedTower.transform.forward * atkRange;
            pointsList.Add(point);
            Debug.DrawLine(selectedTower.transform.position, point, Color.red);
        }
    }

    public void SetPoints()
    {
        for (int i = 0; i < pointsList.Count; i++)
        {
            renderer.SetPosition(i, pointsList[i]);
        }
        if (pointsList.Count > 0)//円なので
            renderer.SetPosition(pointsCount, pointsList[0]);
    }
    public void DrawTheCircle()
    {
        //押すかどうか
        if (Input.GetKeyDown(KeyCode.LeftAlt)
            && renderer != null
            )
        {
            print("ready");
            isKeyOn = true;
            Debug.Log("Alt == true");
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            isKeyOn = false;
            Debug.Log("Alt == false");
        }

        if (isKeyOn&&renderer!=null)
        {
            //＋1の原因は、起点と終点は実際に重なっている。
            renderer.positionCount = pointsCount + 1;
            AddPointsToList();
            SetPoints();
        }
        else
        {
            if (renderer != null)
                renderer.positionCount = 0;
        }
        pointsList.Clear();

    }

}
