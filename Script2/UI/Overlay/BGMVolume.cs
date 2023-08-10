using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// bgmの音量を調節
/// <summary>

public class BGMVolume : MonoBehaviour
{
    public void Update()
    {
        Camera.main.GetComponentInChildren<AudioSource>().volume = GetComponentInChildren<Slider>().value;
    }
}
