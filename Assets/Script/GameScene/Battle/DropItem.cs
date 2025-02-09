using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DropItem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DropItem : MonoBehaviour
{
    private Player player;
    private GameController gameController;
    private HoleController holeController;
    private Transform playerTransform;

    public enum ItemType
    {
        HPRegenItem,
        BallCountItem,
        BlackHoleItem
    }

    public ItemType itemType;

    void Start()
    {
        player = FindObjectOfType<Player>();
        gameController = FindObjectOfType<GameController>();
        holeController = FindObjectOfType<HoleController>();
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
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (itemType)
            {
                case ItemType.HPRegenItem:
                    HPRegenItem();
                    break;
                case ItemType.BallCountItem:
                    BallCountItem();
                    break;
                case ItemType.BlackHoleItem:
                    BlackHoleItem();
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

    private void BallCountItem()
    {
        if (gameController.ballStayCount < 5)
        {
            gameController.ballStayCount++;
            gameController.UIStatusUpdate();
        }
    }

    private void BlackHoleItem()
    {
        if (!holeController.isBlackHoleFormation)
        {
            holeController.PinBallBlackHoleFormation(true);
        }
    }
}
