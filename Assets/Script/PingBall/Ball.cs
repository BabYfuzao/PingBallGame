using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameController gameController;

    private Player player;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        player = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Score"))
        {
            gameController.score += 10;
            StartCoroutine(gameController.UpdateScore());
            player.BulletShoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadArea"))
        {
            Destroy(gameObject);
            gameController.BallDestroy();
        }
    }
}
