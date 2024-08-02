using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{ 
    Fire,
    Water,
    Air,
    Earth
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
    private Quaternion _snapBackRotation;
    private Collider2D _boxcol;
    private Collider2D _overlap;
    private int _layer;
    private LayerMask _layerMask;
    private bool _overplayer;

    private GameManager _gameManager;

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
        _snapBackRotation = transform.rotation;
        _offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _offset -= new Vector3(0.0f, 0.0f, 5.0f);
        transform.rotation = Quaternion.identity;
        _dragging = true;
    }

    private void OnMouseEnter()
    {
        transform.localScale = new Vector3(0.22f, 0.33f, 0.0f);
    }

    private void OnMouseExit()
    {
        transform.localScale = new Vector3(0.2f, 0.3f, 0.0f);
    }

    private void OnMouseUp()
    {
        _dragging = false;
        if (!_overplayer)
        {
            transform.position = _snapBackTransform;
            transform.rotation = _snapBackRotation;
        }
        else
        {
            //get game mangaer
            _gameManager = FindObjectOfType<GameManager>();
            //call play card from game manager
            if(!_gameManager.PlayCard(this))
            {
                transform.position = _snapBackTransform;
            }
        }
    }
}

