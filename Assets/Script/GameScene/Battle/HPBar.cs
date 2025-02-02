using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public GameObject fill;
    public float maxHP;
    public float currentHP;

    public Color fullHealthColor;
    public Color zeroHealthColor;

    public void SetHPBar(float amount)
    {
        StartCoroutine(SetHPBarSmooth(amount));
    }

    private IEnumerator SetHPBarSmooth(float amount)
    {
        float targetHP = currentHP - amount;
        float initialHP = currentHP;
        float duration = 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            currentHP = Mathf.Lerp(initialHP, targetHP, elapsedTime / duration);
            UpdateHPBar();
            yield return null;
        }

        currentHP = targetHP;
        UpdateHPBar();
    }

    public void UpdateHPBar()
    {
        float fillAmount = currentHP / maxHP;
        SpriteRenderer fillSpriteRenderer = fill.GetComponent<SpriteRenderer>();

        fill.transform.localScale = new Vector3(fillAmount, 1f, 1f);
        fill.transform.localPosition = new Vector3(-0.5f + (fillAmount * 0.5f), fill.transform.localPosition.y, fill.transform.localPosition.z);

        Color newColor = Color.Lerp(zeroHealthColor, fullHealthColor, fillAmount);
        fillSpriteRenderer.color = newColor;
    }
}
