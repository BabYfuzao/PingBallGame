using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenVFX : MonoBehaviour
{
    public GameObject hitVFXPrefabs;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameObject vfx = Instantiate(hitVFXPrefabs, collision.GetContact(0).point, Quaternion.identity);
            ParticleSystem.MainModule data = vfx.GetComponent<ParticleSystem>().main;
            data.startColor = GetComponent<SpriteRenderer>().color;

            Destroy(vfx, 0.2f);
        }
    }
}
