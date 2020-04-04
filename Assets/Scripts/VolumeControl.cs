using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class VolumeControl : MonoBehaviour
{
    public Slider volume;
    public AudioSource bgMusic;
    // Start is called before the first frame update
    void Start()
    {
        volume.value = GlobalDataManager.Instance.volume;
    }

    // Update is called once per frame
    void Update()
    {
        bgMusic.volume = volume.value;
        GlobalDataManager.Instance.volume = bgMusic.volume;
    }
}
