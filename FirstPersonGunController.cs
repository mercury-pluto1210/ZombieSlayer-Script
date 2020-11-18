﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonGunController : MonoBehaviour
{
    public enum ShootMode { AUTO, SEMIAUTO }
    public bool shootEnabled = true;
    [SerializeField]
    ShootMode shootMode = ShootMode.AUTO;
    [SerializeField]
    int maxAmmo = 30;
    [SerializeField]
    int maxSupplyValue = 30;
    public int test = 30;
    [SerializeField]
    int damage = 1;
    [SerializeField]
    float shootInterval = 0.1f;
    [SerializeField]
    float shootRange = 100;
    [SerializeField]
    float supplyInterval = 0.3f;
    [SerializeField]
    Vector3 muzzleFlashScale;
    [SerializeField]
    GameObject muzzleFlashPrefab;
    [SerializeField]
    GameObject hitEffectPrefab;
    [SerializeField]
    GameObject gunSound;
    [SerializeField]
    Image ammoGauge;
    [SerializeField]
    public Text ammoText;
    [SerializeField]
    Image supplyGauge;
    bool shooting = false;
    bool supplying = false;
    int ammo = 0;
    int supplyValue = 0;
    int killScore = 1;
    GameObject muzzleFlash;
    GameObject hitEffect;
    GameManager gameManager;

    public int Ammo
    {
        set
        {
            ammo = Mathf.Clamp(value, 0, maxAmmo);
            //UIの表示を操作
            //テキスト
            ammoText.text = ammo.ToString("D3");
            //ゲージ
            float scaleX = (float)ammo / maxAmmo;
            ammoGauge.rectTransform.localScale = new Vector3(scaleX, 1, 1);
            //Debug.Log(ammo);
        }
        get
        {
            return ammo;
        }
    }
    public int SupplyValue
    {
        set
        {
            supplyValue = Mathf.Clamp(value, 0, maxSupplyValue);
            if (SupplyValue >= maxSupplyValue)
            {
                Ammo = maxAmmo;
                supplyValue = 0;
            }
            float scaleX = (float)supplyValue / maxSupplyValue;
            supplyGauge.rectTransform.localScale = new Vector3(scaleX, 1, 1);
        }
        get
        {
            return supplyValue;
        }
    }
    void Start()
    {
        InitGun();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    void Update()
    {
        if (shootEnabled & ammo > 0 & GetInput())
        {
            StartCoroutine(ShootTimer());
        }
        if (shootEnabled)
        {
            StartCoroutine(SupplyTimer());
        }
        
    }
    public void InitGun()
    {
        Ammo = maxAmmo;
        SupplyValue = 0;
    }

    bool GetInput()
    {
        switch (shootMode)
        {
            case ShootMode.AUTO:
                return Input.GetMouseButton(0);
            case ShootMode.SEMIAUTO:
                return Input.GetMouseButtonDown(0);
        }
        return false;
    }
    IEnumerator SupplyTimer()
    {
        if (!supplying)
        {
            supplying = true;
            SupplyValue++;
            yield return new WaitForSeconds(supplyInterval);
            supplying = false;
        }
    }
    IEnumerator ShootTimer()
    {
        if (!shooting)
        {
            shooting = true;
            //マズルフラッシュON
            if (muzzleFlashPrefab != null)
            {
                if (muzzleFlash != null)
                {
                    muzzleFlash.SetActive(true);
                }
                else
                {
                    muzzleFlash = Instantiate(muzzleFlashPrefab, transform.position, transform.rotation);
                    muzzleFlash.transform.SetParent(gameObject.transform);
                    muzzleFlash.transform.localScale = muzzleFlashScale;
                }
            }
            Shoot();
            yield return new WaitForSeconds(shootInterval);
            //マズルフラッシュOFF
            if (muzzleFlash != null)
            {
                muzzleFlash.SetActive(false);
            }
            //ヒットエフェクトOFF
            if (hitEffect != null)
            {
                if (hitEffect.activeSelf)
                {
                    hitEffect.SetActive(false);
                }
            }
            shooting = false;
        }
        else
        {
            yield return null;
        }
    }
    void Shoot()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool hitJudgment = true;
        //レイを飛ばして、ヒットしたオブジェクトの情報を得る
        if (Physics.Raycast(ray, out hit, shootRange))
        {
            string gameObjectName = hit.collider.gameObject.name;
            string zombie1 = "Zombie1(Clone)";
            string zombie2 = "Zombie2(Clone)";

            // ヒットエフェクトON
            if (hitEffectPrefab != null)
            {
                if (hitEffect != null)
                {
                    hitEffect.transform.position = hit.point;
                    hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                    hitEffect.SetActive(true);
                }
                else
                {
                    hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
                }
            }
            //★ここに敵へのダメージ処理などを追加
            if (gameObjectName == zombie1)
            {
                EnemyController_player enemy1 = hit.collider.gameObject.GetComponent<EnemyController_player>();
                enemy1.Hp -= damage;
                //gameManager.Kill += killScore;
            }
            else if (gameObjectName == zombie2)
            {            
                EnemyController_GameOverZone enemy2 = hit.collider.gameObject.GetComponent<EnemyController_GameOverZone>();
                enemy2.Hp -= damage;
                //gameManager.Kill += killScore;
            }

        }
        Ammo--;
    }

    void ShotSound()
    {
        if (ammo == 0)
        {
            gunSound.SetActive(false);
        }
        else if(ammo > 0)
        {
            gunSound.SetActive(true);
        }
    }


}