using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BackgroungAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer1, spriteRenderer2, spriteRenderer3;
    public SceneLoader sceneLoader;

    private void Update()
    {
        spriteRenderer2.transform.Rotate(Vector3.forward, 2000f * Time.deltaTime);
        spriteRenderer3.transform.Rotate(Vector3.forward, 2000f * Time.deltaTime);
    }

    public IEnumerator Fade()
    {
        Sequence initialSequence = DOTween.Sequence();

        initialSequence.Append(spriteRenderer1.DOColor(new Color(40f / 255f, 0f / 255f, 100f / 255f), 1f));
        initialSequence.Join(spriteRenderer2.DOColor(new Color(40f / 255f, 0f / 255f, 100f / 255f), 1f));
        initialSequence.Join(spriteRenderer3.DOColor(new Color(40f / 255f, 0f / 255f, 100f / 255f), 1f));

        yield return initialSequence.WaitForCompletion();

        for (int i = 0; i < 4; i++)
        {
            yield return FadeOutAndIn();
        }

        sceneLoader.LoadGameScene(0f);
    }

    private IEnumerator FadeOutAndIn()
    {
        Sequence fadeOutSequence = DOTween.Sequence();

        fadeOutSequence.Append(spriteRenderer1.DOFade(1f, 0.03f));
        fadeOutSequence.Join(spriteRenderer2.DOFade(1f, 0.03f));
        fadeOutSequence.Join(spriteRenderer3.DOFade(1f, 0.03f));

        yield return fadeOutSequence.WaitForCompletion();
        yield return new WaitForSeconds(0.03f);

        Sequence fadeInSequence = DOTween.Sequence();

        fadeInSequence.Append(spriteRenderer1.DOFade(0f, 0.03f));
        fadeInSequence.Join(spriteRenderer2.DOFade(0f, 0.03f));
        fadeInSequence.Join(spriteRenderer3.DOFade(0f, 0.03f));

        yield return fadeInSequence.WaitForCompletion();
    }
}