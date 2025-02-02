using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonController : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public GameController gameController;

    public void RetryButton()
    {
        sceneLoader.LoadGameScene();
    }

    public void ReturnMenuButton()
    {
        sceneLoader.LoadTittleScene();
    }

    public void PauseControlButton()
    {
        gameController.PauseControl();
    }
}
