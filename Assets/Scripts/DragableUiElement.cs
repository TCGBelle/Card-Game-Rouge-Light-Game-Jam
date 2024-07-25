using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableUiElement : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 _snapBackTransform;
    // Start is called before the first frame update
    private void Update()
    {
        if(dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        _snapBackTransform = transform.position;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
        transform.position = _snapBackTransform;
    }
}
