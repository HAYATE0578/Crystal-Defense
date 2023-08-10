using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
///toggleのuiで、タワーの種類を変える。マウスでタワーを建てる。
/// <summary>

public class TowerBuildManager : MonoBehaviour
{
    public GameObject constructEffect;
    public float c_scale = 1;
    public GameObject lvlupEffect;
    public float l_scale = 1;
    public GameObject deleteEffect;
    public float d_scale = 1;

    public Turret towerLaser;
    public Turret towerMissle;
    public Turret towerCannon;
    public Turret towerThunder;
    public Turret towerBrightLance;
    private Turret selectedTower;

    private AudioSource theAudioSource;
    public AudioClip clickAudio;
    public AudioClip constructAudio;
    public AudioClip lvlupAudio;
    public AudioClip deleteAudio;

    private Ray theRay;
    private RaycastHit theHit;
    public LayerMask construct_LayerMask;//普通のキューブでいい　
    public LayerMask lvlup_or_delete_LayerMask;//selectedCubeのみ

    public int gameStartPlayerMoney = 600;
    public static int playerMoney = 0;


    public GameObject lvButton;
    public GameObject deleteButton;
    private bool lvlupOrNot;
    private bool deleteOrNot;

    public void Awake()
    {
        playerMoney = gameStartPlayerMoney;
        //cameraのaudiosourceで音を流す
        theAudioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void Update()
    {
        SelectTowerBuildItOnCube();
        SelectTowerLvUPOrDelete();
        MoneyCannotBeZero();
    }

    //isOnはインスペクターの中のブール。また、インスペクターの中に操作が必要。
    public void OnLaserSelected(bool isOn)
    {
        if (isOn) selectedTower = towerLaser;
        theAudioSource.PlayOneShot(clickAudio);
    }
    public void OnMissleSelected(bool isOn)
    {
        if (isOn) selectedTower = towerMissle;
        theAudioSource.PlayOneShot(clickAudio);
    }
    public void OnCannonSelected(bool isOn)
    {
        if (isOn) selectedTower = towerCannon;
        theAudioSource.PlayOneShot(clickAudio);
    }
    public void OnThunderSelected(bool isOn)
    {
        if (isOn) selectedTower = towerThunder;
        theAudioSource.PlayOneShot(clickAudio);
    }
    public void OnBrightLanceSelected(bool isOn)
    {
        if (isOn) selectedTower = towerBrightLance;
        theAudioSource.PlayOneShot(clickAudio);
    }
    public void OnLVUPButtonSelected(bool isOn)
    {
        lvlupOrNot = lvButton.GetComponent<Toggle>().isOn;
        theAudioSource.PlayOneShot(clickAudio);
    }
    public void OnDeleteButtonSelected(bool isOn)
    {
        deleteOrNot = deleteButton.GetComponent<Toggle>().isOn;
        theAudioSource.PlayOneShot(clickAudio);
    }
    public void OnBuyHPButtonSelected(bool isOn)
    { //500以上のお金を持てば、半分のお金でHP++。
        if(playerMoney>=500)
        {
            playerMoney = playerMoney*1 / 2;
            CrystalProfile.HP++;
            theAudioSource.PlayOneShot(clickAudio);
        }
    }

    public void OnRestartButtonSelected(bool isOn)
    {
        theAudioSource.PlayOneShot(clickAudio);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //このゲームにはenemyが死んだ瞬間にenemyを生み出す能力を持つので、delete関数で残った敵のある場合を防止。
        GameObject[] notDeletedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (notDeletedEnemies.Length < 0) return;
        foreach (var item in notDeletedEnemies)
        {
            Destroy(item);
        }

        //RESTART
        CrystalProfile.HP = 10;
        TowerBuildManager.playerMoney = 600;
        Time.timeScale = 1;
    }

    public void SelectTowerBuildItOnCube()
    {
        //物理の射線とマウスでキューブの座標を得て、クリックして、防御タワーを建てる。
        theRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //建設
        if (selectedTower != null && Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()//マウスはUIの上にかどうか
                &&
                Physics.Raycast(theRay, out theHit, 1000, construct_LayerMask.value)//射線が当たるかどうか
                && !lvlupOrNot
                && !deleteOrNot
                )
            {
                //選択されたキューブを得て、
                GameObject selectedCube = theHit.collider.gameObject;

                if (playerMoney >= selectedTower.m_price)
                {
                    //お金を払う
                    playerMoney -= selectedTower.m_price;

                    //選択されたキューブに補助するスクリプトをアタッチ。
                    selectedCube.AddComponent<TowerControl>();

                    //キューブの座標にタワーを建て、スクリプトに記録する。
                    selectedCube.GetComponent<TowerControl>().theTurret = selectedTower;
                    selectedCube.GetComponent<TowerControl>().tower =
                        Instantiate(selectedTower.m_Tower_LV1,
                       selectedCube.transform.position, this.transform.rotation
                       );

                    //effectを実現
                    GameObject theEffect =
                        Instantiate(constructEffect,
                        selectedCube.transform.position, Quaternion.Euler(Vector3.up)
                        );
                    theEffect.transform.localScale = new Vector3(c_scale, c_scale, c_scale);
                    Destroy(theEffect, 1);

                    //建設の音を
                    theAudioSource.PlayOneShot(constructAudio);

                    //一個のキューブに一個のタワーをしか建てないため、layer番号を変える。
                    selectedCube.layer = 6;

                    //標識＆記録
                    selectedCube.GetComponent<MeshRenderer>().material.color = Color.grey;
                    selectedCube.GetComponent<TowerControl>().LV = 1;
                }
            }
        }
    }

    public void SelectTowerLvUPOrDelete()
    {
        //物理の射線とマウスでキューブの座標を得て、クリックして、防御タワーを昇格する、削除する。
        theRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //昇格あるいは削除。
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()//マウスはUIの上にかどうか
            && Physics.Raycast
            (theRay, out theHit, 1000, lvlup_or_delete_LayerMask.value)//射線が当たるかどうか
                )
            {
                //選択されたキューブを得る
                GameObject selectedCube = theHit.collider.gameObject;
                Debug.Log(selectedCube);

                //昇格
                if (lvlupOrNot)
                {
                    if (playerMoney >= selectedTower.m_priceOfLVUP
                        && selectedCube.GetComponent<TowerControl>().LV == 1
                        && selectedCube.GetComponent<TowerControl>().theTurret.m_Tower_LV2 != null
                        )//ハイレベルのタワーがあるかどうかを確認
                    {
                        //キューブからタワーの種類を確定する。
                        GameObject newLVTower = selectedCube.
                            GetComponent<TowerControl>().theTurret.m_Tower_LV2;

                        //お金を払う
                        playerMoney -= theHit.collider.gameObject.
                            GetComponent<TowerControl>().theTurret.m_priceOfLVUP;

                        //lv1のタワーを削除する。
                        Destroy(selectedCube.GetComponent<TowerControl>().tower);

                        //キューブの座標にタワーをレベルアップ、スクリプトに記録する。
                        selectedCube.GetComponent<TowerControl>().tower =
                            Instantiate(newLVTower,
                           selectedCube.transform.position,
                           this.transform.rotation
                           );

                        //effectを実現
                        GameObject theEffect =
                            Instantiate(lvlupEffect,
                            selectedCube.transform.position, Quaternion.Euler(Vector3.up)
                            );
                        theEffect.transform.localScale = new Vector3(l_scale, l_scale, l_scale);
                        Destroy(theEffect, 1);

                        //レベルアップの音を
                        theAudioSource.PlayOneShot(lvlupAudio);

                        //標識　記録
                        selectedCube.GetComponent<MeshRenderer>().material.color = Color.black;
                        selectedCube.GetComponent<TowerControl>().LV = 2;
                    }

                }

                //削除
                else if (deleteOrNot)
                {
                    //LV1タワーの価格の半分を払い戻す(LV2のタワーでもlv1の価格で販売)
                    playerMoney += selectedCube.GetComponent<TowerControl>().theTurret.m_price / 2;

                    //lv1のタワーを削除する。
                    Destroy(selectedCube.GetComponent<TowerControl>().tower);

                    //effectを実現
                    GameObject theEffect =
                        Instantiate(deleteEffect,
                        selectedCube.transform.position, Quaternion.Euler(Vector3.up)
                        );
                    theEffect.transform.localScale = new Vector3(d_scale, d_scale, d_scale);
                    Destroy(theEffect, 1);

                    //削除の音を
                    theAudioSource.PlayOneShot(deleteAudio);

                    //普通のキューブに戻せる。
                    selectedCube.layer = 3;

                    //標識 記録
                    selectedCube.GetComponent<MeshRenderer>().material.color = Color.white;
                    selectedCube.GetComponent<TowerControl>().LV = 0;
                }
            }
        }
    }

    public void MoneyCannotBeZero()
    {
        if (playerMoney <= 0) playerMoney = 0;
    }
}
