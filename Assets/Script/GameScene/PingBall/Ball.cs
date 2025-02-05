using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameController gameController;
    private PingBallObjectController pBObjController;
    private Player player;
    private SoundController soundController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        pBObjController = FindObjectOfType<PingBallObjectController>();
        player = FindObjectOfType<Player>();
        soundController = FindObjectOfType<SoundController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //soundController.PlayBallHitSFX();

        switch (collision.gameObject.tag)
        {
            case "NormalAttack":
                gameController.score += 10;
                StartCoroutine(gameController.UpdateScore());
                player.BulletShoot(0);
                ApplyAttackColor(collision.gameObject);
                break;

            case "BombAttack":
                gameController.score += 20;
                StartCoroutine(gameController.UpdateScore());
                player.BulletShoot(1);
                ApplyAttackColor(collision.gameObject);
                break;

            case "PalsyAttack":
                gameController.score += 20;
                StartCoroutine(gameController.UpdateScore());
                player.BulletShoot(2);
                ApplyAttackColor(collision.gameObject);
                break;

            case "RetardAttack":
                gameController.score += 20;
                StartCoroutine(gameController.UpdateScore());
                player.BulletShoot(3);
                ApplyAttackColor(collision.gameObject);
                break;

            case "BurnAttack":
                gameController.score += 20;
                StartCoroutine(gameController.UpdateScore());
                player.BulletShoot(4);
                ApplyAttackColor(collision.gameObject);
                break;

            case "BallSpawnPoint":
                pBObjController.isShoot = false;
                pBObjController.canLoaded = false;
                pBObjController.isPlayChargeSFX = false;
                break;

            case "LauchDoor":
                pBObjController.canLoaded = true;
                break;

            default:
                break;
        }
    }

    public void ApplyAttackColor(GameObject attackObject)
    {
        SpriteRenderer attackSpriteRenderer = attackObject.GetComponent<SpriteRenderer>();
        Color attackColor = attackSpriteRenderer.color;
        player.ChangeSwordTrailColor(attackColor);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BallSpawnPoint"))
        {
            pBObjController.isShoot = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadArea"))
        {
            gameController.ballShotCount--;
            gameController.CheckGameOverStatus();
            Destroy(gameObject);
        }
    }
}
