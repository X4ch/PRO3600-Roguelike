using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    //Classe g�n�rique Item qui sert de parent � Armor et Weapon

    [SerializeField] public string itemName;
    public SpriteRenderer sprite;

    public void setName(string itemName)
    {
        this.itemName = itemName;
    }

}
