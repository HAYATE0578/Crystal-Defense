using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Exit
/// <summary>

public class ClickToExit : MonoBehaviour
{
    public void OnExitButtonSelected()
    {
        Application.Quit();
    }
}
