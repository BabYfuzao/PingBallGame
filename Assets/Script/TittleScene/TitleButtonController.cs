using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonController : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public BackgroungAnimation bgAnim;

    public AudioSource audioSource;
    public AudioClip buttonSFX;

    public TitleSceneController tittleSceneController;

    public void GameStartButton()
    {
        audioSource.PlayOneShot(buttonSFX);
        StartCoroutine(bgAnim.Fade());
        tittleSceneController.UIHide();
    }

    public void ClearDataButton()
    {
        audioSource.PlayOneShot(buttonSFX);
        tittleSceneController.confirmPanel.SetActive(true);
    }

    public void ClearDataConfirmButton()
    {
        audioSource.PlayOneShot(buttonSFX);
        tittleSceneController.DeleteScore();
        tittleSceneController.confirmPanel.SetActive(false);
    }

    public void ClearDataCancelButton()
    {
        audioSource.PlayOneShot(buttonSFX);
        tittleSceneController.confirmPanel.SetActive(false);
    }

    public void QuitButton()
    {
        StartCoroutine(QuitAfterSound());
    }

    private IEnumerator QuitAfterSound()
    {
        audioSource.PlayOneShot(buttonSFX);
        yield return new WaitForSeconds(buttonSFX.length);
        Application.Quit();
    }
}
