using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : Item
{

    //Caracteristiques de l'arme

    public int health;
    public int attack;
    public int defense;
    public int speed;
    public int mana;
    public int magic;


    //Initialise les statistiques de l'arme
    public void Init(int health, int attack, int defense, int speed, int mana, int magic, string name)
    {
        setName(name);
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.mana = mana;
        this.magic = magic;
    }

    public void transformInto(Weapon w) //Permet de modifier les statistiques de l'arme
    {
        health = w.health;
        attack = w.attack;
        defense = w.defense;
        speed = w.speed;
        mana = w.mana;
        magic = w.magic;
        setName(w.itemName);
    }


    public string toString()
    {
        return itemName + ", PV : " + health + ", Attaque : " + attack + ", Défense : " + defense + ", Vitesse : " + speed + ", Mana : " + mana + ", Magie : " + magic;
        
    } //Permet d'afficher les statistiques de l'arme

   
}
