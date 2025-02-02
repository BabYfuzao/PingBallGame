using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hP;
    public HPBar hPBar;

    public GameObject[] bulletPrefabs;

    public HingeJoint2D swordJoint;

    public GameController gameController;

    public SoundController soundController;

    private void Start()
    {
        hPBar.maxHP = hP;
        hPBar.currentHP = hPBar.maxHP;
        hPBar.UpdateHPBar();
    }

    public void BulletShoot(int bulletIndex)
    {
        soundController.PlayGunSFX();

        GameObject bullet = Instantiate(bulletPrefabs[bulletIndex], transform.position, Quaternion.identity);

        float randomAngle = Random.Range(-45f, 45f);
        bullet.transform.Rotate(0, 0, randomAngle);

        float randomSize = Random.Range(0.3f, 0.6f);
        bullet.transform.localScale = new Vector3(randomSize, randomSize, 1);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = transform.right;
        bulletRb.velocity = direction * 15f;

        StartCoroutine(SlashAnimation());

        Destroy(bullet, 3f);
    }

    public IEnumerator SlashAnimation()
    {
        JointMotor2D slashMotor = swordJoint.motor;

        slashMotor.motorSpeed = 2000;
        swordJoint.motor = slashMotor;

        yield return new WaitForSeconds(0.1f);

        slashMotor.motorSpeed = -2000;
        swordJoint.motor = slashMotor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            UpdatePlayerState();
        }
    }

    public void UpdatePlayerState()
    {
        hP--;
        hPBar.SetHPBar(1);
        gameController.TextHandle();
        CheckDead();
    }

    public void CheckDead()
    {
        if (hP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
