using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGrid : MonoBehaviour
{
    public int height = 10;
    public int width = 10;

    public float gridSpaceSize = 5f;

    [SerializeField] private GameObject gridCellPrefab;
    private GameObject[,] fieldGrid; 


    void Start()
    {
        CreateGrid();
    }

    // Creates the grid when the game starts
    private void CreateGrid()
    {
        fieldGrid = new GameObject[height, width];
        if (gridCellPrefab == null)
        {
            Debug.LogError("ERROR: Grid Cell Prefab on the field grid is not assigned");
            return;
        }

        // Make the grid
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Create a new GridSpace object for each cell
                fieldGrid[x, y] = Instantiate(gridCellPrefab, new Vector3(x * gridSpaceSize, y * gridSpaceSize), Quaternion.identity);
                fieldGrid[x, y].GetComponent<GridCell>().SetPosition(x, y);
                fieldGrid[x, y].transform.parent = transform; 
                fieldGrid[x, y].gameObject.name = "Grid Space ( X: ) " + x.ToString() + " , Y: " + y.ToString() + ")";
                
            }
        }
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
