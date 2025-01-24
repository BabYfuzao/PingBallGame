using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public PlayerController player;
    void Update()
    {
        if (player.ball != null)
        {
            float posY = Mathf.Clamp(player.ball.transform.position.y, 0, 5.5f);
            transform.position = new Vector3(0, posY, -10);
        }
    }
}
