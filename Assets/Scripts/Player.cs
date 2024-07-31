using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _health = 200;

    private Element _resistance;

    public HealthBar _healthBar;

    public void Start()
    {
        _health = 200;
        _healthBar.SetMaxHealth(_health);
    }

    public void DealtDamage(Element element, int damage)
    {
        if (_resistance == element)
        {
            _health -= (damage / 2);
        }
        else
        {
            _health -= damage;
        }
        if (_health < 0)
        {
            _health = 0;
        }
        _healthBar.SetHealth(_health);
    }

    public void Heal()
    {
        _health += 5;
        _healthBar.SetHealth(_health);
    }
}
