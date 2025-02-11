using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public SpriteRenderer sr;

    private SoundController soundController;

    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
    }

    void Update()
    {
        sr.transform.Rotate(Vector3.forward, 50f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            soundController.PlayMeteoriteExplosionSFX();
        }
    }
}
