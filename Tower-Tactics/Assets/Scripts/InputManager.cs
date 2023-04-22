using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    FieldGrid fieldGrid; 
    [SerializeField] private LayerMask whatIsAGridLayer;
    // Start is called before the first frame update
    void Start()
    {
        fieldGrid = FindObjectOfType<FieldGrid>();    
    }

    // Update is called once per frame
    void Update()
    {
        
        GridCell cellTouchIsOver = IsTouchOverGridSpace();
        if (cellTouchIsOver != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cellTouchIsOver.GetComponentInChildren<SpriteRenderer>().material.color = Color.green;
            }
        }
    }

    // Returns the grid cell if mouse is over a grid cell and returns null if it is not
    private GridCell IsTouchOverGridSpace()
    {
        // if (Input.touchCount > 0)
        // {
            // Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, whatIsAGridLayer))
            {
                return hitInfo.transform.GetComponent<GridCell>();
            }
            else
            {
                return null;
            }
        // } 
    }
}
