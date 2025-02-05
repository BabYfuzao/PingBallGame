using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BackgroungAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public IEnumerator Fade()
    {
        yield return spriteRenderer.DOColor(new Color(40f / 255f, 0f / 255f, 100f / 255f), 1f).WaitForCompletion();
        for (int i = 0; i < 4; i++)
        {
            yield return FadeOutAndIn();
        }

        SceneManager.LoadScene(1);
    }

    private IEnumerator FadeOutAndIn()
    {
        yield return spriteRenderer.DOFade(1f, 0.03f).WaitForCompletion();
        yield return new WaitForSeconds(0.03f);
        yield return spriteRenderer.DOFade(0f, 0.03f).WaitForCompletion();
    }
}
