using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameController gameController;
    private PinBallObjectController pBObjController;
    private Player player;
    private SoundController soundController;
    private HoleController holeController;

    public bool isFake;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        pBObjController = FindObjectOfType<PinBallObjectController>();
        player = FindObjectOfType<Player>();
        soundController = FindObjectOfType<SoundController>();
        holeController = FindObjectOfType<HoleController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //soundController.PlayBallHitSFX();

        switch (collision.gameObject.tag)
        {
            case "NormalAttack":
                gameController.ScoreUpdate(10);
                player.BulletShoot(0);
                player.ChangeSwordTrailColor(Color.white);
                break;

            case "BombAttack":
                gameController.ScoreUpdate(20);
                player.BulletShoot(1);
                player.ChangeSwordTrailColor(Color.black);
                break;

            case "PalsyAttack":
                gameController.ScoreUpdate(20);
                player.BulletShoot(2);
                player.ChangeSwordTrailColor(Color.yellow);
                break;

            case "RetardAttack":
                gameController.ScoreUpdate(20);
                player.BulletShoot(3);
                player.ChangeSwordTrailColor(new Color(0f / 255f, 200f / 255f, 255f / 255f));
                break;

            case "BurnAttack":
                gameController.ScoreUpdate(20);
                player.BulletShoot(4);
                player.ChangeSwordTrailColor(Color.red);
                break;

            case "BallSpawnPoint":
                pBObjController.isShoot = false;
                pBObjController.isPlayChargeSFX = false;
                break;

            default:
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BallSpawnPoint"))
        {
            pBObjController.isShoot = true;
        }
        if (collision.gameObject.CompareTag("Bounce"))
        {
            soundController.PlayBounceSFX();
        }
        if (collision.gameObject.CompareTag("Flipper"))
        {
            soundController.PlayFlipperSFX();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadArea"))
        {
            soundController.PlayPopSFX();
            if (!isFake)
            {
                gameController.ballShotCount--;
                pBObjController.canLoaded = true;
                gameController.CheckGameOverStatus();
            }
            else
            {
                holeController.canFakeBallInstantiate = false;
            }
            Destroy(gameObject);
        }
    }
}
