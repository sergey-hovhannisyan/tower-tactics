using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    GameManager _gameManager;
    private void Awake ()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void SelectCurrentObject()
    {
        _gameManager.SelectObject(gameObject);
    }    

    // Deselects the current object: Used for testing
    public void DeselectCurrentObject()
    {
        _gameManager.DeselectObject();
    }
}
