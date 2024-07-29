using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{ 
    Fire,
    Water,
    Earth,
    Air
}
[RequireComponent(typeof(Collider2D))]
public class BaseCardClass : MonoBehaviour
{
    private int _value;
    public int Value
    { 
        get { return _value; } 
        set { _value = value; }
    }
    private Element _type;
    public Element Type
    { 
        get { return _type; } 
        set { _type = value; }
    }

    private bool _dragging = false;
    private Vector3 _offset;
    private Vector3 _snapBackTransform;
    [SerializeField] private Sprite sprite;
    private Collider2D _boxcol;
    private Collider2D _overlap;
    private int _layer;
    private LayerMask _layerMask;
    private bool _overplayer;

    private GameManager _gameManager;

    public BaseCardClass(int value, int element)
    {
        _value = value;
        _type = (Element)element;
    }

    private void Awake()
    {
        _boxcol = GetComponent<BoxCollider2D>();
        _layer = LayerMask.NameToLayer("Player");
        if(_layer != -1)
        {
            _layerMask = 1 << _layer;
        }
    }

    private void Update()
    {
        if (_dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _offset;
        }
        _overlap = Physics2D.OverlapArea(_boxcol.bounds.min, _boxcol.bounds.max, _layerMask);
        if (_overlap != null)
        {
            _overplayer = true;
            Debug.Log("Overlaping with player: " + _overplayer);
        }
        else
        {
            _overplayer = false;
            Debug.Log("Overlaping with player: " + _overplayer);
        }

    }

    private void OnMouseDown()
    {
        _snapBackTransform = transform.position;
        _offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _dragging = true;
    }

    private void OnMouseUp()
    {
        _dragging = false;
        if (!_overplayer)
        {
            transform.position = _snapBackTransform;
        }
        else
        {
            //get game mangaer
            _gameManager = FindObjectOfType<GameManager>();
            //call play card from game manager
            _gameManager.PlayCard(this);
        }
    }
}

