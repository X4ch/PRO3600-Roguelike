using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public GameObject pickUpSpawner;
    public GameObject healthPickUp;
    public GameObject manaPickup;
    public System.Random random;
    public int pickUpChance = 40;

    // Cette fonction permet de g�n�rer un consomable de vie ou de mana lorque le joueur finit une salle avec des enemies
    public void generatePickUp()
    {
        // Permet de g�rer la probabilit� de faire apparaitre un consomable
        if (random.Next(0, 100) < pickUpChance)
        {   
            // Permet de choisir le consomable � faire apparaitre
            if (random.Next(0,2) == 0)
            { 
                Instantiate(healthPickUp, pickUpSpawner.transform.position, Quaternion.identity);
                Destroy(pickUpSpawner);
            }
            else 
            {
                Instantiate(manaPickup, pickUpSpawner.transform.position, Quaternion.identity);
                Destroy(pickUpSpawner);
            }
           
        }
        else
        {   
            
            Debug.Log("Pas de pickup");
        }
    }

    private void Start()
    {
        random = new System.Random();
    }
}
