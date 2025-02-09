using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonController : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public SoundController soundController;
    public GameController gameController;

    private float buttonSFXDealy;

    public void Start()
    {
        buttonSFXDealy = soundController.buttonSFX.length;
    }

    public void OKButton()
    {
        soundController.PlayButtonSFX();
        gameController.SubmitPlayerName();
    }

    public void ReadyButton()
    {
        StartCoroutine(gameController.ReadyCountDown());
    }

    public void RetryButton()
    {
        soundController.PlayButtonSFX();
        sceneLoader.LoadGameScene(buttonSFXDealy);
    }

    public void ReturnMenuButton()
    {
        soundController.PlayButtonSFX();
        sceneLoader.LoadTitleScene(buttonSFXDealy);
    }

    public void PauseControlButton()
    {
        soundController.PlayButtonSFX();
        gameController.PauseControl();
    }
}
