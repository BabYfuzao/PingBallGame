using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public GameObject hitVFXPrefabs;
    public SpriteRenderer spriteRenderer;
    private SoundController soundController;
    public Color color;

    private void Start()
    {
        soundController = FindObjectOfType<SoundController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            soundController.PlayScoreSFX();
            GameObject vfx = Instantiate(hitVFXPrefabs, collision.GetContact(0).point, Quaternion.identity);
            ParticleSystem.MainModule data = vfx.GetComponent<ParticleSystem>().main;
            data.startColor = color;
            StartCoroutine(SetColor());
            Destroy(vfx, 0.2f);
        }
    }

    public IEnumerator SetColor()
    {
        spriteRenderer.color = color;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}