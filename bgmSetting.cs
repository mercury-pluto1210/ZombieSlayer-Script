using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bgmSetting : MonoBehaviour
{
    AudioSource audioSource;
    public Slider slider;
    private bool m_Play;
    public bool m_ToggleChange;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        m_Play = true;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = slider.GetComponent<Slider>().normalizedValue;
    }
}
