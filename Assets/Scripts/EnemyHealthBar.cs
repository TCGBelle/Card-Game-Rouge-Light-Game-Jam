using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private int max;

    public void UpdateHealthBar(int currvalue)
    {
        slider.value = currvalue;
    }

    public void SetMaxHealth(int maxHealth)
    {
        max = maxHealth;
        slider.maxValue = max;
        slider.value = max;
    }
}
