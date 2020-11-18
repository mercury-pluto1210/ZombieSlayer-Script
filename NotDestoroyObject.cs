using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDestoroyObject : MonoBehaviour
{
    static public NotDestoroyObject instance;

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
    }
}