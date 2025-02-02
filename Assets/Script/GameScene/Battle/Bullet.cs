using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionEffectPrefab;

    public enum BulletType
    {
        Normal,
        Bomb,
        Palsy,
        Retard,
        Burn
    }

    public BulletType bulletType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                switch (bulletType)
                {
                    case BulletType.Normal:
                        enemy.StateUpdate(0);
                        break;
                    case BulletType.Bomb:
                        Explosion();
                        break;
                    case BulletType.Palsy:
                        enemy.StateUpdate(1);
                        break;
                    case BulletType.Retard:
                        enemy.StateUpdate(2);
                        Explosion();
                        break;
                    case BulletType.Burn:
                        enemy.StateUpdate(3);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void Explosion()
    {
        GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
    }
}
