using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class InGameVolumeControl : MonoBehaviour
{
    public AudioSource bgMusic;
    // Start is called before the first frame update
    void Start()
    {
        bgMusic.volume = GlobalDataManager.Instance.volume;
    }

    // Update is called once per frame
    void Update()
    {        
    }
}
