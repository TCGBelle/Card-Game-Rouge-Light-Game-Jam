using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _health;
    public int Helath { get { return _health; } set { _health = value; } }
    private Deck _playerDeck;
    public Deck PlayerDeck { get { return _playerDeck; } set { _playerDeck = value;  } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collider2D other)
    {
        Debug.Log(gameObject.name + " started overlapping with " + other.gameObject.name);
    }

    private void OnCollisionExit(Collider2D other)
    {
        Debug.Log(gameObject.name + " stopped overlapping with " + other.gameObject.name);
    }
}
