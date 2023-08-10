using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の攻撃方式、敵の攻撃範囲にあるタワーが破壊される
/// <summary>

public class DestroyTowers : MonoBehaviour
{
    public float radius = 16f;
    public GameObject destroyEffect;
    public AudioClip destroyADO;

    public void Update()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");

        foreach (var item in towers)
        {
            float distance = (item.transform.position - transform.position).magnitude;
            if (distance < radius)
            {
                GameObject theEffect = Instantiate(destroyEffect, item.transform.position, item.transform.rotation);
                Destroy(item.gameObject);
                Destroy(theEffect, 2f);
                if(destroyADO!=null)
                    Camera.main.GetComponent<AudioSource>().PlayOneShot(destroyADO);
            }
        }

        foreach (var item in cubes)
        {
            float distance = (item.transform.position - transform.position).magnitude;
            if (distance < radius)
            {
                //普通のキューブに戻せる。
                item.layer = 3;

                //標識 記録
                if (item.GetComponent<MeshRenderer>() != null)
                    item.GetComponent<MeshRenderer>().material.color = Color.white;
                if (item.GetComponent<TowerControl>() != null)
                    item.GetComponent<TowerControl>().LV = 0;
            }
        }

    }

    //gizmosの関数で半径の長さを確認
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
