using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGrid : MonoBehaviour
{
    public int width = 21;
    public int height = 1;
    
    public float gridSpaceSize = 1f;

    public Transform gridBackgroundTransform; 

    [SerializeField] private GameObject gridCellPrefab;
    private GameObject[,] fieldGrid; 


    void Start()
    {
        CreateGrid();
    }

    // Creates the grid when the game starts
    private void CreateGrid()
    {
        fieldGrid = new GameObject[width, height];
        if (gridCellPrefab == null)
        {
            Debug.LogError("ERROR: Grid Cell Prefab on the field grid is not assigned");
            return;
        }

        // Make the grid
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Create a new GridSpace object for each cell
                fieldGrid[x, y] = Instantiate(gridCellPrefab, new Vector3(x * gridSpaceSize, y * gridSpaceSize), Quaternion.identity);
                fieldGrid[x, y].GetComponent<GridCell>().SetPosition(x, y);
                fieldGrid[x, y].transform.parent = transform; 
                fieldGrid[x, y].gameObject.name = "Grid Space ( X: ) " + x.ToString() + " , Y: " + y.ToString() + ")";        
            }
        }
        gameObject.transform.position = new Vector3(-11f, 0.01f, -5f);
        gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Gets the grid position from the world position
    public Vector2Int GetGridPosFromWorld(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / gridSpaceSize);
        int y = Mathf.FloorToInt(worldPosition.z / gridSpaceSize);

        x = Mathf.Clamp(x, 0, width);
        y = Mathf.Clamp(y, 0, height);

        return new Vector2Int(x, y);
    }

    // Gets the world position of a grid position
    public Vector3 GetWorldPosFromGridPos(Vector2Int gridPos)
    {
        float x = gridPos.x * gridSpaceSize; 
        float y = gridPos.y * gridSpaceSize; 

        return new Vector3(x, 0, y);
    }
}
