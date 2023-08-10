using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///敵を探す、攻撃する方式、など。listの形式で、敵が攻撃範囲内であるかどうかを確認する。
/// <summary>

public class TowerAI : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public float rotateSpeed;
    public float atkInterval;
    private float helpTime;
    private float attackRange;
    public GameObject bullet;
    public GameObject fireHole;

    public bool isEnemyInRange;

    public AudioClip fireAudio;
    private AudioSource fireHoleSource;

    public TurretType theTurretType = TurretType.Laser;

    //レーザーの攻撃をするとき使うATK
    public int atk;
    public int atk_SD;
    public int atk_AR;

    public LineRenderer lineRenderer;
    LineRenderer branch1;
    LineRenderer branch2;
    public GameObject enemy1;
    public GameObject enemy2;

    public void Awake()
    {
        attackRange = GetComponent<SphereCollider>().radius;
        if (GetComponent<AudioSource>() != null)
            fireHoleSource = GetComponent<AudioSource>();
        if (GetComponent<LineRenderer>() != null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    public void Update()
    {
        CheckEnemyInOrNot();
        RefreshTheList();//敵のリストをリフレッシュ

        if (enemies.Count > 0)
        {
            MakeTowerLookEnemy();
        }
        else//範囲内に敵がなかったら0の座標に戻す
        {
            IfNoEnemyBackTo0();
        }
    }

    private void CheckEnemyInOrNot()
    {
        if (enemies.Count > 0)
            isEnemyInRange = true;
        else if (enemies.Count <= 0)
            isEnemyInRange = false;

        //電磁波の大砲であるとき、射線イフェクトを使うかどうかを決める
        if (!isEnemyInRange && lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }


    void OnTriggerEnter(Collider col)//colliderで敵が攻撃範囲内かどうかを
    {
        if (col.gameObject.tag == "Enemy") enemies.Add(col.gameObject);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy") enemies.Remove(col.gameObject);
    }

    public void MakeTowerLookEnemy()
    {
        //最初に入った敵を攻撃
        GameObject theFirstTarget = enemies[0].gameObject;

        //方向を確認
        Quaternion dir2Enemy = Quaternion.LookRotation(theFirstTarget.transform.position - transform.position);

        //lerp四元数計算。オイラー角計算。結果をオブジェクトに。
        Quaternion newRot = Quaternion.Lerp(transform.rotation, dir2Enemy, rotateSpeed * Time.deltaTime);
        Vector3 newV3 = newRot.eulerAngles;
        if (theFirstTarget != null) transform.eulerAngles = newV3;

        //補助線で確認
        Debug.DrawLine(transform.position, theFirstTarget.transform.position, Color.red);

        //砲撃開始
        AttackEnemy(dir2Enemy, theFirstTarget);
    }
    public void IfNoEnemyBackTo0()
    {
        //ゼロの四元数を獲得
        Quaternion zero = Quaternion.Euler(Vector3.zero);
        //四元数とオイラー角計算
        Quaternion newRot = Quaternion.Lerp
              (transform.rotation, zero, rotateSpeed * Time.deltaTime);
        Vector3 newV3 = newRot.eulerAngles;
        //結果をオブジェクトに
        transform.eulerAngles = newV3;
    }
    public void RefreshTheList()
    {
        //新しいリストで、nullのリスト対象（missingの死んだ敵）を排除する。
        List<int> theIndex = new List<int>();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                theIndex.Add(i);
            }//インデクスを保存
        }
        for (int i = 0; i < theIndex.Count; i++)
        {
            enemies.RemoveAt(theIndex[i] - i);
        }//インデクスによりnullのリスト対象を排除。
    }

    public void AttackEnemy(Quaternion dir2Enemy, GameObject enemyTarget)
    {
        //方向が一致して、攻撃間隔時間を超えれば、攻撃する。
        if (Quaternion.Angle(transform.rotation, dir2Enemy) < 30f
            && helpTime < Time.time
            && fireHole != null
            )
        {
            // 攻撃方式はだいたい二種類：弾丸（鎧砲撃）と電磁波（レーザー、アーク、光槍）など。
            if ((theTurretType.ToString() == TurretType.Missle.ToString()
                || theTurretType.ToString() == TurretType.Cannon.ToString()))//ミサイルとカノン
            {
                if (bullet != null)
                {
                    //弾丸を作り、目標を設定。
                    GameObject theBullet = Instantiate(bullet, fireHole.transform.position, transform.rotation);

                    if (theBullet.GetComponent<Bullet>() != null)
                    {
                        //大砲による攻撃力と対鎧攻撃力を弾丸に入力、発射
                        theBullet.GetComponent<Bullet>().ATK = atk;
                        theBullet.GetComponent<Bullet>().atk_AR = atk_AR;
                        theBullet.GetComponent<Bullet>().SetTarget(enemyTarget.transform);
                    }

                    //撃てる音を
                    fireHoleSource.PlayOneShot(fireAudio);
                }
            }
            else if (theTurretType.ToString() == TurretType.Laser.ToString())//レーザー
            {
                if (isEnemyInRange)
                {
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, fireHole.transform.position);
                    lineRenderer.SetPosition(1, enemies[0].transform.position);
                    lineRenderer.material.mainTextureOffset = new Vector2(-Time.time * 3, 0);
                    enemies[0].GetComponent<EnemyProfile>().ShieldAttack(atk, atk_SD);
                }
            }
            else if (theTurretType.ToString() == TurretType.Thunder.ToString())//アーク
            {
                if (isEnemyInRange)
                {
                    lineRenderer.enabled = true;
                    lineRenderer.material.mainTextureOffset = new Vector2(-Time.time * 6, 0);
                    lineRenderer.SetPosition(0, fireHole.transform.position);
                    lineRenderer.SetPosition(1, enemies[0].transform.position);
                    enemies[0].GetComponent<EnemyProfile>().OverallAttack(atk, atk_SD, atk_AR);


                    //thunderは、同時に三体の敵に電流攻撃を。また、敵同士の間に、電流のeffectを。
                    if (enemies.Count > 2 && enemies[1] != null)
                    {
                        if (branch1 != null) branch1.enabled = true;
                        enemy1 = enemies[1];
                        enemies[1].GetComponent<EnemyProfile>().OverallAttack(atk, atk_SD, atk_AR);
                        branch1 = enemies[0].GetComponent<LineRenderer>();
                        branch1.material = lineRenderer.material;
                        branch1.SetPosition(0, enemies[0].transform.position);
                        branch1.SetPosition(1, enemies[1].transform.position);
                    }
                    else if (enemies.Count > 1&& enemies[1] == null&& enemy1 != null)
                    {
                        enemy1.GetComponent<LineRenderer>().enabled = false;
                        enemy1 = null;
                    }

                    if (enemies.Count > 3 && enemies[2] != null)
                    {
                        if (branch2 != null) branch2.enabled = true;
                        enemy2 = enemies[2];
                        enemies[2].GetComponent<EnemyProfile>().OverallAttack(atk, atk_SD, atk_AR);
                        branch2 = enemies[1].GetComponent<LineRenderer>();
                        branch2.material = lineRenderer.material;
                        branch2.SetPosition(0, enemies[1].transform.position);
                        branch2.SetPosition(1, enemies[2].transform.position);
                    }
                    else if (enemies.Count > 2 && enemies[2] == null && enemy2!=null)
                    {
                        enemy2.GetComponent<LineRenderer>().enabled = false;
                        enemy2 = null;
                    }

                }

            }
            else if (theTurretType.ToString() == TurretType.BrightLance.ToString())//光槍
            {
                if (isEnemyInRange)
                {
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, fireHole.transform.position);
                    lineRenderer.SetPosition(1, transform.TransformPoint(0,0,45)//長さは約45
                        );
                    lineRenderer.material.mainTextureOffset = new Vector2(-Time.time * 5, 0);


                    //光槍は、目標の敵だけでなく、光槍に触れた多くの敵にもダメージを与えられる。
                    //AnotherBrightLanceAttackというスクリプトにもう一つの攻撃方式が書かれた。
                }
            }
            //攻撃間隔時間を計算
            helpTime = Time.time + atkInterval;
        }
    }
}

