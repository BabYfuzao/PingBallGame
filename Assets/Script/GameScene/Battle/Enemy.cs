using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public int hP;
    private HPBar hPBar;

    public float moveSpeed;
    private float originalSpeed;

    public GameObject ballCountAddDropItem;
    public float dropChance;

    private bool isBuff = false;

    private GameController gameController;

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
            StateUpdate(0);
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
                StartCoroutine(Hit());
                break;

            case 1:
                isBuff = true;
                StartCoroutine(Palsy());
                break;

            case 2:
                isBuff = true;
                StartCoroutine(Retard());
                break;

            case 3:
                isBuff = true;
                StartCoroutine(Burn());
                break;

            default:
                break;
        }
    }

    private IEnumerator Hit()
    {
        SetColor(Color.gray);
        TakeDamage(1);
        yield return new WaitForSeconds(0.2f);

        RestoreState();
    }

    private IEnumerator Palsy()
    {
        moveSpeed = 0;
        SetColor(Color.yellow);
        TakeDamage(1);
        yield return new WaitForSeconds(1f);

        isBuff = false;
        RestoreState();
    }

    private IEnumerator Retard()
    {
        moveSpeed /= 2;
        SetColor(Color.blue);
        yield return new WaitForSeconds(3f);

        isBuff = false;
        RestoreState();
    }

    private IEnumerator Burn()
    {
        SetColor(Color.red);
        for (int i = 0; i < 3; i++)
        {
            TakeDamage(1);
            yield return new WaitForSeconds(1f);
        }

        isBuff = false;
        RestoreState();
    }

    private void RestoreState()
    {
        if (isBuff)
        {
            return;
        }
        moveSpeed = originalSpeed;
        SetColor(Color.white);
    }

    private void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void TakeDamage(int damage)
    {
        hP -= damage;

        hPBar.SetHPBar(damage);

        CheckDead();
    }

    public void CheckDead()
    {
        float randomValue = Random.Range(0f, 1f);

        if (hP <= 0)
        {
            gameController.killCount++;
            gameController.TextHandle();
            Destroy(gameObject);
        }
    }
}