using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleButtonController : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public BackgroungAnimation bgAnim;

    public TittleSceneController tittleSceneController;

    public void GameStartButton()
    {
        StartCoroutine(bgAnim.Fade());
    }

    public void DeleteScoreButton()
    {
        tittleSceneController.DeleteScore();
    }
}
