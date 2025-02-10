using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hole : MonoBehaviour
{
    public bool isBlack;

    public SpriteRenderer spriteRenderer;

    public HoleController holeController;

    void Update()
    {
        if (isBlack)
        {
            spriteRenderer.transform.Rotate(Vector3.forward, 100f * Time.deltaTime);
        }
        else
        {
            spriteRenderer.transform.Rotate(Vector3.forward, -100f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (isBlack)
            {
                holeController.BallInBlackHole();
            }
            else
            {
                holeController.FakeBallInstantiate();
            }
        }
    }
}
