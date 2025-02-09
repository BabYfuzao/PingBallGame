using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteButton : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer bgPinBallSR;
    public SpriteRenderer bgBattleSR;

    public MeteoriteBumper meteoriteBumper;
    public GameController gameController;
    public SoundController soundController;

    public GameObject meteoritePrefab;
    public Transform meteoriteTransform;
    private bool isMeteoriteShoot = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !isMeteoriteShoot)
        {

            soundController.PlayScoreSFX();
            soundController.PlayDangerSFX();
            soundController.PlayMeteoriteSFX();

            gameController.ScoreUpdate(50);

            isMeteoriteShoot = true;

            StartCoroutine(ColorChange());

            StartCoroutine(MeteoriteShot());
        }
    }

    public IEnumerator MeteoriteShot()
    {
        meteoriteBumper.spriteRenderer.sprite = meteoriteBumper.screamingEmoji;
        yield return new WaitForSeconds(3f);

        GameObject meteorite = Instantiate(meteoritePrefab, meteoriteTransform.position, Quaternion.identity);

        StartCoroutine(EmojiChange());

        Destroy(meteorite, 5f);
    }

    public IEnumerator ColorChange()
    {
        Color originalColor = Color.white;
        Color targetColor = Color.red;

        for (int i = 0; i < 60; i++)
        {
            spriteRenderer.color = targetColor;
            bgPinBallSR.color = targetColor;
            bgBattleSR.color = targetColor;
            meteoriteBumper.spriteRenderer.color = targetColor;
            yield return new WaitForSeconds(0.05f);

            spriteRenderer.color = originalColor;
            bgPinBallSR.color = originalColor;
            bgBattleSR.color = originalColor;
            meteoriteBumper.spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.05f);

            targetColor = (targetColor == Color.red) ? Color.white : Color.red;
        }

        spriteRenderer.color = originalColor;
        bgPinBallSR.color = originalColor;
        meteoriteBumper.spriteRenderer.color = originalColor;
    }

    public IEnumerator EmojiChange()
    {
        yield return new WaitForSeconds(3f);

        meteoriteBumper.spriteRenderer.sprite = meteoriteBumper.anxiousEmoji;
        soundController.PlayShotSFX();
        gameController.ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0.2f));
        yield return new WaitForSeconds(0.5f);

        meteoriteBumper.controlDoor.SetActive(true);
        yield return new WaitForSeconds(1f);

        meteoriteBumper.spriteRenderer.sprite = meteoriteBumper.glassEmoji;
        isMeteoriteShoot = false;
    }
}
