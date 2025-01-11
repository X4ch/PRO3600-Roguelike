using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    //Classe générique Item qui sert de parent à Armor et Weapon

    [SerializeField] public string itemName;
    public SpriteRenderer sprite;

    public void setName(string itemName)
    {
        this.itemName = itemName;
    }

}
