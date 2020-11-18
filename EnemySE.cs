using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySE : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] se;

    void ZombieAttackSE()
    {
        audioSource.PlayOneShot(se[0]);
    }

    void ZombieDeathSE()
    {
        audioSource.PlayOneShot(se[1]);
    }
}
