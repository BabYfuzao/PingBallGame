using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;

    public int hP;

    public float moveSpeed;

    void Start()
    {
        
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
            hP--;
            CheckDead();
        }
    }

    public void CheckDead()
    {
        if (hP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
