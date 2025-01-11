using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickUp : MonoBehaviour
{
    public GameObject manaPickup;
    public PlayerV2 player;
    public int manaValue;

    // Permet que lorsque le joueur rentre en contact avec le consomoable de vie, il soit consomé et que le joueur regagne du mana d'un montant égal à manaValue
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.currentMana + manaValue < player.maxMana)
            {   
                player.currentMana += manaValue;
                Destroy(manaPickup);
            }
            else
            {   
                player.currentMana = player.maxMana;
                Destroy(manaPickup);
            }
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerV2>();
    }
}
