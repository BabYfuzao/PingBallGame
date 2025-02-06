using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[System.Serializable]
public class DropItemObject
{
    public GameObject itemPrefab;
    public float dropChance;
}

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public int hP;
    private HPBar hPBar;

    public float moveSpeed;
    private float originalSpeed;

    public DropItemObject[] dropItemObjects;
    public float overallDropChance;

    public GameObject[] effectVFXs;
    private GameObject activeEffect;

    private GameController gameController;

    private bool isPalsy = false, isRetard = false, isBurn = false;

    void Start()
    {
        originalSpeed = moveSpeed;

        hPBar = FindObjectOfType<HPBar>();
        gameController = FindObjectOfType<GameController>();

        hPBar.maxHP = hP;
        hPBar.currentHP = hPBar.maxHP;
        hPBar.UpdateHPBar();
    }

    void Update()
    {
        Vector2 direction = Vector2.left;
        rb.velocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Explosion"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("RetardExplosion"))
        {
            StateUpdate(2);
        }
    }

    public void StateUpdate(int effectIndex)
    {
        switch (effectIndex)
        {
            case 0:
                StartCoroutine(Hit(Color.gray));
                break;

            case 1:
                if (!isPalsy)
                {
                    StartCoroutine(Palsy());
                }
                StartCoroutine(Hit(Color.yellow));
                break;

            case 2:
                if (!isRetard)
                {
                    StartCoroutine(Retard());
                }
                StartCoroutine(Hit(Color.blue));
                break;

            case 3:
                if (!isBurn)
                {
                    StartCoroutine(Burn());
                }
                StartCoroutine(Hit(Color.red));
                break;

            default:
                break;
        }
    }

    private IEnumerator Hit(Color color)
    {
        SetColor(color);
        yield return new WaitForSeconds(0.2f);

        SetColor(Color.white);
    }

    private IEnumerator Palsy()
    {
        isPalsy = true;
        moveSpeed = 0;
        GameObject palsyEffect = Instantiate(effectVFXs[0], transform.position, Quaternion.identity);
        palsyEffect.transform.SetParent(transform);
        yield return new WaitForSeconds(3f);

        isPalsy = false;
        RestoreState(palsyEffect);
    }

    private IEnumerator Retard()
    {
        isRetard = true;
        moveSpeed /= 2;
        GameObject retardEffect = Instantiate(effectVFXs[1], transform.position, Quaternion.identity);
        retardEffect.transform.SetParent(transform);
        yield return new WaitForSeconds(3f);

        isRetard = false;
        RestoreState(retardEffect);
    }

    private IEnumerator Burn()
    {
        isBurn = true;
        GameObject burnEffect = Instantiate(effectVFXs[2], transform.position, Quaternion.identity);
        burnEffect.transform.SetParent(transform);
        for (int i = 0; i < 3; i++)
        {
            TakeDamage(1);
            StartCoroutine(Hit(Color.red));
            yield return new WaitForSeconds(1f);
        }

        isBurn = false;
        RestoreState(burnEffect);
    }

    private void RestoreState(GameObject effect)
    {
        Destroy(effect);
        moveSpeed = originalSpeed;
    }

    private void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void TakeDamage(int damage)
    {
        hP -= damage;

        hPBar.SetHPBar(-damage);

        CheckDead();
    }

    public void CheckDead()
    {
        if (hP <= 0)
        {
            ItemDrop();
            gameController.killCount++;
            gameController.TextHandle();
            DestroyCurrentEffects();
            Destroy(gameObject);
        }
    }

    private void DestroyCurrentEffects()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ItemDrop()
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= overallDropChance)
        {
            float cumulativeChance = 0f;

            for (int i = 0; i < dropItemObjects.Length; i++)
            {
                cumulativeChance += dropItemObjects[i].dropChance;

                if (randomValue <= cumulativeChance)
                {
                    Instantiate(dropItemObjects[i].itemPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }
}