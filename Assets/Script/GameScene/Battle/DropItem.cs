using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DropItem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DropItem : MonoBehaviour
{
    private Player player;
    private Transform playerTransform;

    public enum ItemType
    {
        HPRegenItem,
        BallCountItem,
        BumperItem
    }

    public ItemType itemType;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerTransform = player.transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, 15f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D called with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected!");
            switch (itemType)
            {
                case ItemType.HPRegenItem:
                    HPRegenItem();
                    break;
            }
            Destroy(gameObject);
        }
    }

    private void HPRegenItem()
    {
        if (player.currentHP < player.maxHP)
        {
            player.currentHP++;
            player.UpdatePlayerState(1);
        }
    }
}
