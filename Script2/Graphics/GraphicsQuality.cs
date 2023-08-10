using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// TOGGLEで画面の品質を簡単にチェンジ
/// <summary>

public class GraphicsQuality : MonoBehaviour
{
    public void Awake()
    {
        GetComponent<Toggle>().isOn = true;
    }

    public void OnIsHighToggleSelected()
    {
        if (!GetComponent<Toggle>().isOn)
            Camera.main.GetComponent<Camera>().renderingPath = RenderingPath.VertexLit;
        else
            Camera.main.GetComponent<Camera>().renderingPath = RenderingPath.UsePlayerSettings;
    }
}
