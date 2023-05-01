using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // This script is used to handle all input from the player
    GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        if (_gameManager.isPaused) return;
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            // If the finger is on the screen and moving or stationary: Call the ControlGridInput method in GameManager
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) 
            {
                _gameManager.ControlGridInput(touch);
            }
        }
    }
}
