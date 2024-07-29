using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _health = 20;
    public int Helath { get { return _health; } set { _health = value; } }

    private void OnCollisionEnter(Collider2D other)
    {
        Debug.Log(gameObject.name + " started overlapping with " + other.gameObject.name);
    }

    private void OnCollisionExit(Collider2D other)
    {
        Debug.Log(gameObject.name + " stopped overlapping with " + other.gameObject.name);
    }
}
