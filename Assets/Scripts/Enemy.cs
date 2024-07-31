using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _health;
    public int Helath { get { return _health; } set { _health = value; } }
    private Element _resistance;
    public Element Resistance { get { return _resistance; } set { _resistance = value; } }

    private int _strength;
    public int Strength { get { return _strength; } set { _strength = value; } }

    [SerializeField] private EnemyHealthBar _healthBar;

    public void SetMaxHealthBar()
    {
        _healthBar.SetMaxHealth(_health);
    }

    public void DealtDamage(Element element, int damage)
    {
        if(_resistance == element)
        {
            _health -= (damage / 2);
        }
        else
        {
            _health -= damage;
        }
        if(_health < 0)
        {
            _health = 0;
        }
        _healthBar.UpdateHealthBar(_health);
    }
}
