using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadTitleScene(float delay)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAfterSound(0, delay));
    }

    public void LoadGameScene(float delay)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAfterSound(1, delay));
    }

    private IEnumerator LoadSceneAfterSound(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
}