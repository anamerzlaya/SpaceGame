using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public static HealthBar instance;

    private void Awake()
    {
        instance = this;
    }

        public void SetMaxHealth(int health)
    {
        slider.value = health;
        slider.maxValue = health;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color= gradient.Evaluate(slider.normalizedValue);
    }
}
