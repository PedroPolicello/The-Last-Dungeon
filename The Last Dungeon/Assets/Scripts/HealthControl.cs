using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
    public Slider HealthSlider;
    public Gradient gradient;
    public Image fill;
    public void SetMaxHealth(int health)
    {
       HealthSlider.maxValue = health;
        HealthSlider.value = health;
      fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        HealthSlider.value = health;
        fill.color= gradient.Evaluate(HealthSlider.normalizedValue);
    }
   
}
