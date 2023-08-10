using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Cannonは、放物線の攻撃方式
/// <summary>

public class CannonBall : Bullet
{
    public float distance;
    public float coroutineIntervalTime = 0.3f;
    public float penetrateTime = 4f;

    private float currentDistance;
    private bool shootMove = true;
    private float angle = 45;

    public void Start()
    {
        distance = Vector3.Distance(transform.position, target.position);
    }

    public new void Update()
    {
        StartCoroutine(CannonFire());
    }

    IEnumerator CannonFire()
    {
        while (shootMove)
        {
            if(target != null)
            {
                transform.LookAt(target);
                angle = Mathf.Min(1, Vector3.Distance(target.position, transform.position) / distance) * 45;
                transform.rotation = transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);
                currentDistance = Vector3.Distance(target.position, transform.position);
            }

            else if(target == null)
            {
                transform.Translate(Vector3.down*Time.deltaTime*15f);
            }

            yield return new WaitForSeconds(0.3f);

                if (currentDistance < 0.5f)
                    shootMove = false;

                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
           
        }
        yield return null;
    }

    public new void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            if (col.gameObject.GetComponent<EnemyProfile>() != null)
                col.gameObject.GetComponent<EnemyProfile>().ArmorAttack(ATK,atk_AR);
            GameObject temp=
                Instantiate(onEnemyEffect, gameObject.transform.position, Quaternion.Euler(Vector3.up));

            /*bulletと違って、多くの敵にダメージを与えるので、「GetComponent<Collider>().enabled = false;」
            　というセンテンスがなくてもよい！*/

            Destroy(gameObject,penetrateTime);
            Destroy(temp, 2f);
        }
    }
}
