using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    #region Properties
    public int width = 42;
    public int height = 16;
    
    public float gridSpaceSize = 1f;

    public Transform gridBackgroundTransform;

    private GameManager _gameManager;
    [SerializeField] private GameObject gridCellPrefab;
    private GameObject[,] fieldGrid; 

    #endregion

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    #region Public: Grid Instantiation & Clearing Methods
    // Creates the grid when the game starts
    public void CreateGrid()
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
        Vector3 gridSize = new Vector3(width * gridSpaceSize, -0.1f, height * gridSpaceSize);
        gameObject.transform.position = gridBackgroundTransform.position - gridSize/2;
        gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Clears all objects from the grid
    public void ClearGrid()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                GridCell cell = fieldGrid[x, y].GetComponent<GridCell>();
                if (cell.isOccupied)
                {
                    Destroy(cell.objectInThisGridSpace);
                    cell.objectInThisGridSpace = null;
                    cell.isOccupied = false;
                }
            }
        }
    }

    // Destroys the grid
    public void DestroyGrid()
    {
        ClearGrid();
        Destroy(gameObject);
    }

    public bool CanPlaceObstacle(Transform startPoint, Transform endPoint, GameObject obstaclePrefab, Vector3 obstaclePosition)
    {
        // Instantiate an obstacle in the desired position
        GameObject tempObstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);

        // Calculate the path between the start and end points
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        bool pathFound = UnityEngine.AI.NavMesh.CalculatePath(startPoint.position, endPoint.position, UnityEngine.AI.NavMesh.AllAreas, path);

        // Destroy the temporary obstacle
        Destroy(tempObstacle);

        return pathFound;
    }

    #endregion
}
