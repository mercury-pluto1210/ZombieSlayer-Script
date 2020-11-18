using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotSoundController : MonoBehaviour
{
    public FirstPersonGunController firstPersonGunController;
    int ammoValue;
    GunShotSound gunShotSound;
    AudioSource audioSource;
    bool pauseMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        ammoValue = firstPersonGunController.Ammo;
        gunShotSound = GetComponent<GunShotSound>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(firstPersonGunController.Ammo == 0)
        {
            gunShotSound.enabled = false;
            audioSource.enabled = false;
        }
        else if(firstPersonGunController.Ammo > 0)
        {
            audioSource.enabled = true;
            gunShotSound.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            audioSource.mute = !audioSource.mute;
        }
    }
}
