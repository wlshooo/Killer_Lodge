using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource bgm;

    public void SetBgmVolume(float volume)
    {
        bgm.volume = volume;
    }
}
