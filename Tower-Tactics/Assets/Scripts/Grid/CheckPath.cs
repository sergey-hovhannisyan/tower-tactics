using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
