using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public GameObject healthPickup;
    public PlayerV2 player;
    public int healingValue;

    // Permet que lorsque le joueur rentre en contact avec le consomoable de vie, il soit consomé et que le joueur soit soigné du montant healingValue
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.currentHealth + healingValue < player.health)
            {
                player.currentHealth += healingValue;
                Destroy(healthPickup);
            }
            else
            {   
                player.currentHealth = player.health;
                Destroy(healthPickup);
            }
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerV2>();
    }
}
