using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleButtonController : MonoBehaviour
{
    public SceneLoader sceneLoader;

    public void GameStartButton()
    {
        sceneLoader.LoadGameScene();
    }
}
