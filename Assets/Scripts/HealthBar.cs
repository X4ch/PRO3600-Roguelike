using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// permet d'ajuster la barre de vie
public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        
    }


}
