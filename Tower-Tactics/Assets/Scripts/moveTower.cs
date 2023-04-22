using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTower : MonoBehaviour
{
    private Vector3 offset;
    private float zCoordinate;
    private bool isMoving = false;
    private Camera mainCamera;
    private GameObject[] gridCells;

    void Start()
    {
        mainCamera = Camera.main;
        gridCells = GameObject.FindGameObjectsWithTag("cell");
    }

    void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
         if (Input.touchCount > 0) 
        {
        Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero
        
        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) 
        {
            // get the touch position from the screen touch to world point
            Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 
                touch.position.y, -mainCamera.transform.position.z));
            // lerp and set the position of the current object to that of the touch, but smoothly over time.
            Debug.Log("Touch position: " + touch.position); // Add this line
            Debug.Log("Touched world position: " + touchedPos); 
            touchedPos = SnapToNearestCell(touchedPos);
            transform.position = touchedPos;
        }
        }
    }
     private Vector3 SnapToNearestCell(Vector3 position)
    {
        GameObject nearestCell = null;
        float minDistance = float.MaxValue;

        foreach (GameObject cell in gridCells)
        {
            float distance = Vector3.Distance(position,cell.transform.position);
            if (distance < minDistance && IsCellEmpty(cell))
            {
                minDistance = distance;
                nearestCell = cell;
            }
        }
        Debug.Log(nearestCell);
        return nearestCell != null ? nearestCell.transform.position : position;
    }

    private bool IsCellEmpty(GameObject cell){
        if (cell.GetComponent<GridCell>().objectInThisGridSpace == null){
            return true;
        }
        Debug.Log("here");
        return false;
    }

}