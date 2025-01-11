using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Pedestal : MonoBehaviour
{

    public SpriteRenderer sprite;
    public GameObject heldItem; //Objet qui sera ramasse
    public Inventory inventory; //Inventaire avec lequel le piedestal va interagir
    public BoxCollider2D boxCollider;
    public CircleCollider2D circleCollider;
    public GameObject canvas;
    public GameObject player;

    //Probabilite des differents objets sur le piedestal
    public int weaproba = 50;
    public int magproba = 0;

    private string type;
    private string randomPedestalKey = "r"; //Genere un objet au hasard sur le piedestal
    private string weaponPedestalKey = "t"; //Genere une arme sur le piedestal
    private string armorPedestalKey = "y"; //Genere une armure



    private void OnTriggerEnter2D(Collider2D collision) //Interaction lors de la collision avec le piedestal
    {
        if (collision.CompareTag("Player"))
        {
            if (circleCollider.Distance(collision).distance <= 0)
            {
                canvas.SetActive(true);                                                 //Permet d'afficher les statistiques pour la comparaison
                canvas.GetComponentInChildren<Text>().text = inventory.statComparison(heldItem, type); //Effectue la comparaison
            }


            if (boxCollider.Distance(collision).distance <= 0)
            {



                Debug.Log("Debut du swap");
                inventory.itemSwap(heldItem, type);


                canvas.SetActive(false);
                boxCollider.enabled = false;
                sprite.enabled = false;
                this.enabled = false;  //Une fois l'echange fait, on desactive le piedestal pour empecher les interactions

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            canvas.SetActive(false);
    }





    void generateItem(int weaproba, int magproba) //Genere un objet avec les probabilites donnees en parametres
    {
        int res = Random.Range(1, 100);
        int weaptresh = weaproba;
        int magtresh = 100 - magproba;

        if (res <= weaptresh)
        {
            generateWeapon();
        }
        else if (weaptresh < res && res <= magtresh)
        {
            generateArmor();
        }
        else
        {
            //generateMagic();
        }
    }


    void generateWeapon() //Genere une arme avec des statistiques aleatoires entre 0 et 15
    {
        heldItem.AddComponent<Weapon>();
        string name = "Dague " + Random.Range(0, 100);
        heldItem.GetComponent<Weapon>().Init(Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), name);

    }

    void generateArmor() //Genere une armure avec des statistiques entre 0 et 15 et un type aleatoire
    {
        heldItem.AddComponent<Armor>();
        int armorType = Random.Range(1, 3);

        switch (armorType)
        {
            case 1:
                string helmname = "Casque " + Random.Range(0, 100);
                heldItem.GetComponent<Armor>().Init(Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), helmname, "Helmet");
                break;
            case 2:
                string armname = "Armure " + Random.Range(0, 100);
                heldItem.GetComponent<Armor>().Init(Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), armname, "Armor");
                break;
            case 3:
                string bootname = "Bottes " + Random.Range(0, 100);
                heldItem.GetComponent<Armor>().Init(Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15), bootname, "Boots");
                break;
            default:
                break;
        }
    }




    void Start() //Genere un objet sur le piedestal lorsque celui-ci est cree
    {
        inventory = GameObject.FindWithTag("InventoryManager").GetComponent<Inventory>();
        generateItem(weaproba, magproba);
        Type itemtype = GetComponent<Item>().GetType();
        if (itemtype == typeof(Weapon)) // Verification du type d'objet a echanger
        {
            type = "Weapon";

        }
        else if (itemtype == typeof(Armor))
        {
            type = heldItem.GetComponent<Armor>().armorType;

        }
    }

    // Update is called once per frame
    void Update()
    {




        if (Input.GetKeyDown(randomPedestalKey)) //Fait apparaitre un objet sur le piedestal
        {
            generateItem(weaproba, magproba);
        }
        else if (Input.GetKeyDown(weaponPedestalKey)) //Fait apparaitre une arme sur le piedestal
        {
            generateWeapon();
        }
        else if (Input.GetKeyDown(armorPedestalKey)) //Fait apparaitre une armure sur le piedestal
        {
            generateArmor();
        }



    }
}
