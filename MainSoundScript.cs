using UnityEngine;
using System.Collections;

public class MainSoundScript : MonoBehaviour
{
    void Awake()
    { 
        // 自分自身だったり
        DontDestroyOnLoad(this);
    }
}