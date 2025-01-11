using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public GameObject canvas; //Zone de menu de l'inventaire

    //Les differents objets que l'inventaire peut contenir
    public GameObject helmet;
    public GameObject armor;
    public GameObject boot;
    public GameObject weapon;
    public GameObject stats;
    public GameObject selector;
    public GameObject player;
    private bool isActive = false; //Statut de l'affichage de l'inventaire

    [SerializeField] public List<Item> itemList; //Contenu de l'inventaire
    private string currentlyselected = "0";
    
    private string inventoryKey = "e"; //Permet d'ouvrir l'inventaire


    void toggleInventory() //Active ou desactive l'inventaire et empêche le joueur de bouger lorsque l'inventaire est ouvert
    {
        if (!isActive) { canvas.SetActive(true); player.GetComponent<PlayerV2>().enabled = false; }
        
        else { canvas.SetActive(false); player.GetComponent<PlayerV2>().enabled = true; }
        isActive = !isActive;
    }


    public void itemSwap(GameObject item, string type) //Permet de remplacer un objet de l'inventaire par un sur le sol
    {
        switch (type)
        {
            case "Weapon":
                weapon.GetComponent<Weapon>().transformInto(item.GetComponent<Weapon>()); itemList[3] = weapon.GetComponent<Weapon>(); Debug.Log("Objet interverti"); currentlyselected = "3"; break;

            case "Helmet":
                helmet.GetComponent<Armor>().transformInto(item.GetComponent<Armor>()); itemList[0] = helmet.GetComponent<Armor>(); Debug.Log("Objet interverti"); currentlyselected = "0"; break;

            case "Armor":
                armor.GetComponent<Armor>().transformInto(item.GetComponent<Armor>()); itemList[1] = armor.GetComponent<Armor>(); Debug.Log("Objet interverti"); currentlyselected = "1"; break;

            case "Boots":
                boot.GetComponent<Armor>().transformInto(item.GetComponent<Armor>()); itemList[2] = boot.GetComponent<Armor>(); Debug.Log("Objet interverti"); currentlyselected = "2"; break;

            default:
                break;
        }
        statDisplayer();
    }


    void statDisplayer() //Affiche les statistiques de l'objet selectionne
    {
        
        switch (currentlyselected)
        {
            case "0":
                stats.GetComponent<Text>().text = "Casque : " + helmet.GetComponent<Armor>().toString();
                selector.transform.position = helmet.transform.position;
                break;

            case "1":
                stats.GetComponent<Text>().text = "Armure : " + armor.GetComponent<Armor>().toString(); 
                selector.transform.position = armor.transform.position;
                break;

            case "2":
                stats.GetComponent<Text>().text = "Bottes : " + boot.GetComponent<Armor>().toString();
                selector.transform.position = boot.transform.position;
                break;

            case "3":
                stats.GetComponent<Text>().text = "Arme : " + weapon.GetComponent<Weapon>().toString();
                selector.transform.position = weapon.transform.position;
                break;
        }

        


    }


    public string statComparison(GameObject item, string type) //Effectue la comparaison entre les statistiques de l'objet donne avec celui de l'inventaire
    {
        string res = item.GetComponent<Item>().itemName + "\n";
        switch (type)
        {
            case "Helmet":
                Armor helm = item.GetComponent<Armor>();
                res += "PV : " + helm.health + " ("+ (helm.health -helmet.GetComponent<Armor>().health) + ")\nAttaque : " + helm.attack + " (" +(helm.attack - helmet.GetComponent<Armor>().attack)+ ")\nDefense : " + helm.defense + " (" +(helm.defense - helmet.GetComponent<Armor>().defense)+ ")\nVitesse : " + helm.speed + " (" + (helm.speed - helmet.GetComponent<Armor>().speed)+ ")\nMana : "+ helm.mana + " (" +(helm.mana- helmet.GetComponent<Armor>().mana) + ")\nMagie : " + helm.magic + " (" + (helm.magic - helmet.GetComponent<Armor>().magic)+")";
                break;

            case "Armor":
                Armor arm = item.GetComponent<Armor>();
                res += "PV : " + arm.health + " (" + (arm.health - armor.GetComponent<Armor>().health) + ")\nAttaque : " + arm.attack + " (" + (arm.attack - armor.GetComponent<Armor>().attack) + ")\nDefense : " + arm.defense + " (" + (arm.defense - armor.GetComponent<Armor>().defense) + ")\nVitesse : " + arm.speed + " (" + (arm.speed - armor.GetComponent<Armor>().speed) + ")\nMana : " + arm.mana + " (" + (arm.mana - armor.GetComponent<Armor>().mana) + ")\nMagie : " + arm.magic + " (" + (arm.magic - armor.GetComponent<Armor>().magic)+")";
                break;

            case "Boots":
                Armor boo = item.GetComponent<Armor>();
                res += "PV : " + boo.health + " (" + (boo.health - boot.GetComponent<Armor>().health) + ")\nAttaque : " + boo.attack + " (" + (boo.attack - boot.GetComponent<Armor>().attack) + ")\nDefense : " + boo.defense + " (" + (boo.defense - boot.GetComponent<Armor>().defense) + ")\nVitesse : " + boo.speed + " (" + (boo.speed - boot.GetComponent<Armor>().speed) + ")\nMana : " + boo.mana + " (" + (boo.mana - boot.GetComponent<Armor>().mana) + "\nMagie : " + boo.magic + " (" + (boo.magic - boot.GetComponent<Armor>().magic)+")";
                break;

            case "Weapon":
                Weapon wea = item.GetComponent<Weapon>();
                res += "PV : " + wea.health + " (" + (wea.health - weapon.GetComponent<Weapon>().health) + ")\nAttaque : " + wea.attack + " (" + (wea.attack - weapon.GetComponent<Weapon>().attack) + ")\nDefense : " + wea.defense + " (" + (wea.defense - weapon.GetComponent<Weapon>().defense) + ")\nVitesse : " + wea.speed + " (" + (wea.speed - weapon.GetComponent<Weapon>().speed) + ")\nMana : " + wea.mana + " (" + (wea.mana - weapon.GetComponent<Weapon>().mana) + ")\nMagie : " + wea.magic + " (" + (wea.magic - weapon.GetComponent<Weapon>().magic)+")";

                break;
        }


        return res;
    }


    public void inventoryReset() //Vide completement l'inventaire
    {
        if (itemList.Count !=0)
        {
            itemList.Clear();
        }

        helmet.AddComponent<Armor>();
        helmet.GetComponent<Armor>().Init(0, 0, 0, 0, 0, 0, "", "Helmet");
        armor.AddComponent<Armor>();
        armor.GetComponent<Armor>().Init(0, 0, 0, 0, 0, 0, "", "Armor");
        boot.AddComponent<Armor>();
        boot.GetComponent<Armor>().Init(0, 0, 0, 0, 0, 0, "", "Boots");
        weapon.AddComponent<Weapon>();
        weapon.GetComponent<Weapon>().Init(0, 0, 0, 0, 0, 0, "");

        itemList.Add(helmet.GetComponent<Armor>());
        itemList.Add(armor.GetComponent<Armor>());
        itemList.Add(boot.GetComponent<Armor>());
        itemList.Add(weapon.GetComponent<Weapon>());
        statDisplayer();


    }


    void Start()
    {

        player = GameObject.FindGameObjectsWithTag("Player")[0];
        itemList = new List<Item>();
        inventoryReset();
        

    }

    void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        { //Ouverture de l'inventaire
            toggleInventory();
        }

        if (isActive) //On affiche le nom de l'objet dans les emplacements correspondants
        {
            
            helmet.GetComponentInChildren<Text>().text = itemList[0].itemName;
            armor.GetComponentInChildren<Text>().text = itemList[1].itemName;
            boot.GetComponentInChildren<Text>().text = itemList[2].itemName;
            weapon.GetComponentInChildren<Text>().text = itemList[3].itemName;
            statDisplayer();
            currentlyselected = Input.inputString;



        }

    }
}
