using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotSound : MonoBehaviour
{
    private AudioSource sound1;

    // Start is called before the first frame update
    void Start()
    {
        sound1 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sound1.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            sound1.Stop();
        }
    }

    void StopSound()
    {
        sound1.Stop();
    }
}
