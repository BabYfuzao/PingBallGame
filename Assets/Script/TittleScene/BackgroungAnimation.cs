using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroungAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        while (true)
        {
            yield return spriteRenderer.DOColor(new Color(40f / 255f, 0f / 255f, 100f / 255f), 1f).WaitForCompletion();
            for (int i = 0; i < 4; i++)
            {
                yield return FadeOutAndIn();
            }

            yield return spriteRenderer.DOColor(new Color(200f / 255f, 100f / 255f, 0f / 255f), 1f).WaitForCompletion();
            yield return spriteRenderer.DOFade(1f, 1f).WaitForCompletion();
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator FadeOutAndIn()
    {
        yield return spriteRenderer.DOFade(1f, 0.03f).WaitForCompletion();
        yield return new WaitForSeconds(0.03f);
        yield return spriteRenderer.DOFade(0f, 0.03f).WaitForCompletion();
    }
}
