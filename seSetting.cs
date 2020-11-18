using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class seSetting : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SEVol", volume);
    }
}