using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // This script is used to handle all input from the player
    GameManager _gameManager;
    private bool _isPaused = false;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        if (_isPaused) return;
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            // If the player clicks on the screen, insert the object into the cell
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) 
            {
                _gameManager.InsertObjectIntoCell(touch);
            }
        }
    }

    public void PauseGame()
    {
        _isPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        _isPaused = false;
        Time.timeScale = 1;
    }
}
