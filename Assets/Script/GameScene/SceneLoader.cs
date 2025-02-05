using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadTittleScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void LoadGameScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
