using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private int posX; 
    private int posY; 
    
    /// Saves a referenc e to the gameObject that gets places on this cell
    public GameObject objectInThisGridSpace = null;

    // Saves if the grid space is occupied or not
    public bool isOccupied = false;
    
    // Set grid cell positions
    public void SetPosition(int x, int y)
    {
        posX = x; 
        posY = y;
    }
    
    // Get grid cell positions
    public Vector2Int GetPosition()
    {
        return new Vector2Int(posX, posY);
    }
}
