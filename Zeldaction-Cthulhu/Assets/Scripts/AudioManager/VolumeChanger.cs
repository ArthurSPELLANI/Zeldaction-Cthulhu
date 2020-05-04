using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChanger : MonoBehaviour
{

    public AudioSource Music;
    float volume;

    static float time = 0f;
    public float IncreasVolume;

    void Start()
    {
        volume = 0.5f; 
    }


    void Update()
    {
        if (Music.volume != volume)
            Music.volume = Mathf.Lerp(0f, volume, time);

        if (time > 1f)
        {
            time = 0f;
        }

        time += IncreasVolume * Time.deltaTime;
    }
}
