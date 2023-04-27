using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    GameManager _gameManager;
    public GameObject objectPrefab;
    private bool _isSelected = false;

    private void Awake ()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void SelectSwitch()
    {
        if (_isSelected)
            _gameManager.DeselectObject();
        else
            _gameManager.SelectObject(objectPrefab);
        
        _isSelected = !_isSelected;
    }
}
