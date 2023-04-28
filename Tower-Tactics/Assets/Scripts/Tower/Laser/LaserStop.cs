using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStop : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {   
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(transform.position);
        RaycastHit hit;
        Vector3[] positions = new Vector3 [2];
        Physics.Raycast(transform.position, transform.right, out hit);
        positions[0] = transform.position; 
        // lineRenderer.SetPosition(1, transform.position + transform.forward * 1000);
        // if (Physics.Linecast(transform.position, transform.position + transform.right * 1000, out RaycastHit hitInfo))
        // {
        //     lineRenderer.SetPosition(1,hitInfo.collider.transform.position);
        //     Debug.Log("blocked");
        //     Debug.Log(hitInfo.collider.transform.position);
        // }
        if (Physics.Raycast(transform.position, transform.right, out hit)){
            Debug.Log("here"); 
            positions[1] = hit.point;
            lineRenderer.SetPositions(positions);
        }
        else
        {
            positions[1] =transform.position +  transform.right*100;
            lineRenderer.SetPositions(positions);
            // lineRenderer.SetPosition(1, transform.position + transform.right * 1000);
        }
    }
}
