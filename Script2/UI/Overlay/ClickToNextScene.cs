using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 
/// <summary>

public class ClickToNextScene : MonoBehaviour
{
    int displayProgress = 0;
    int goal = 0;
    AsyncOperation theAp;
    public GameObject loadedRange;

    public int nextSceneIndex = 0;

    public void Awake()
    {
        loadedRange.SetActive(false);
    }

    public void OnClickNextLevelButton()
    {
        StartCoroutine(StartLoading(nextSceneIndex));
        loadedRange.SetActive(true);
    }

    private IEnumerator StartLoading(int sceneIndex)
    {
        theAp = SceneManager.LoadSceneAsync(sceneIndex);
        theAp.allowSceneActivation = false;
        while (theAp.progress < 0.9f)
        {
            goal = (int)theAp.progress * 100;
            while (displayProgress < goal)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }

        goal = 100;

        while (displayProgress < goal)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        theAp.allowSceneActivation = true;
    }
    private void SetLoadingPercentage(float per)
    {
        loadedRange.GetComponentInChildren<Text>().text = per.ToString() + " %";
    }
}
