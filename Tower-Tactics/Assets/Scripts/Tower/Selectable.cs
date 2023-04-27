using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    GameManager _gameManager;
    private bool _isSelected = false;

    private void Awake ()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void SelectSwitch()
    {
        if (_isSelected)
            DeselectCurrentObject();
        else
            SelectCurrentObject();
        
        _isSelected = !_isSelected;
    }

    private void SelectCurrentObject()
    {
        _gameManager.SelectObject(gameObject);
    }    

    private void DeselectCurrentObject()
    {
        _gameManager.DeselectObject();
    }
}
