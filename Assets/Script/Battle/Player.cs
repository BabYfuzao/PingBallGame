using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hP;

    public GameObject bulletPrefab;

    public void BulletShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = transform.right;
        bulletRb.velocity = direction * 10f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
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
