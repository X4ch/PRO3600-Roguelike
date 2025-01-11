using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    //Caracteristiques de l'armure

    public int health;
    public int attack;
    public int defense;
    public int speed;
    public int mana;
    public int magic;

    public string armorType;

    public void transformInto(Armor a) //Permet de changer les statistiques d'une armure
    {
        health = a.health;
        attack = a.attack;
        defense = a.defense;
        speed = a.speed;
        mana = a.mana;
        magic = a.magic;
        setName(a.itemName);
    }

    //Initialise les caracteristiques de l'armure
    public void Init(int health, int attack, int defense, int speed, int mana, int magic, string name, string armorType)
    {
        setName(name);
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.mana = mana;
        this.magic = magic;
        this.armorType = armorType;
    }

    public string toString() // Permet d'afficher les statistiques de l'armure
    {
        return itemName + ", PV : " + health + ", Attaque : " + attack + ", Défense : " + defense + ", Vitesse : " + speed + ", Mana : " + mana + ", Magie : " + magic;

    }


}
