using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 作られたばかりのときから、敵に向かっていく。
///弾丸の方向を決めるのは、撃てるとき、大砲のinstantiateメソッドで決める。
/// <summary>

public class Bullet : MonoBehaviour
{
    public int ATK = 20;
    public int atk_AR = 20;
    public float moveSpeed = 20;
    public Transform target;
    public GameObject onEnemyEffect;

    public AudioClip attackedAudio;
    public AudioSource theAudioSource;

    public void SetTarget(Transform _target)
    {

        this.target = _target;
        transform.LookAt(this.target);
    }

    public void Awake()
    {
        //敵に当たらなかったら、自動的にオブジェクトを削除。
        Destroy(this.gameObject, 2);
    }

    public void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            if (col.gameObject.GetComponent<EnemyProfile>() != null)
                col.gameObject.GetComponent<EnemyProfile>().ArmorAttack(ATK, atk_AR);
            GameObject temp =
                Instantiate(onEnemyEffect, gameObject.transform.position, transform.rotation);

            //こうしないと、colliderは多くのcolliderを持つ敵に多段のダメージを与える。
            GetComponent<Collider>().enabled = false;

            Destroy(gameObject);
            Destroy(temp, 2f);
        }
    }
}
