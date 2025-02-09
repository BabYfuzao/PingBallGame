using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteBumper : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite glassEmoji, flushedEmoji, screamingEmoji, anxiousEmoji;

    public GameObject controlDoor;

    public GameController gameController;
    public SoundController soundController;

    private void Start()
    {
        spriteRenderer.sprite = glassEmoji;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            soundController.PlayScoreSFX();
            gameController.ScoreUpdate(30);
            spriteRenderer.sprite = flushedEmoji;
            controlDoor.SetActive(false);
            StartCoroutine(SetColor());
        }
    }

    private IEnumerator SetColor()
    {
        spriteRenderer.color = Color.gray;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
