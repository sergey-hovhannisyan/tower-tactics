using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Properties

    private Camera mainCam;
    public GameObject selectedObjectPrefab;
    private bool objectSelected = false;
    public Grid grid;

    #endregion

    #region Control Properties

    private void Awake() 
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
         grid.CreateGrid();
    }

    private void Update()
    {
        
    }

    #endregion
    
    #region Private: GridCell Methods

    // Gets the cell that the player is touching
    private GridCell GetCellFromTouch(Touch touch)
    {
        Ray ray = mainCam.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, LayerMask.GetMask("GridCell")))
        {
            return raycastHit.collider.GetComponent<GridCell>();
        }
        return null;
    }

    // Drops a tower into a cell
    private void DropInCell(GridCell cell)
    {
        if (selectedObjectPrefab && !cell.isOccupied    )
        {
            GameObject newTower = Instantiate(selectedObjectPrefab, cell.transform.position, Quaternion.identity);
            cell.objectInThisGridSpace = newTower;
            cell.isOccupied = true;
        }
    }
    
    // Removes a object from a cell
    public void RemoveFromCell(GridCell cell)
    {
        if (cell.objectInThisGridSpace)
        {
            Destroy(cell.objectInThisGridSpace);
            cell.objectInThisGridSpace = null;
            cell.isOccupied = false;
        }
    }

    #endregion

    #region Public: GridCell Interface Methods
    public void InsertObjectIntoCell(Touch touch)
    {
        GridCell cellTouchIsOver = GetCellFromTouch(touch);
        if (cellTouchIsOver && objectSelected)
            DropInCell(cellTouchIsOver);
    }

    public void RemoveObjectFromCell(Touch touch)
    {
        GridCell cellTouchIsOver = GetCellFromTouch(touch);
        if (cellTouchIsOver != null)
            RemoveFromCell(cellTouchIsOver);
    }

    public void ClearGrid() 
    {
        grid.ClearGrid();
    }

    #endregion

    #region Object Selection Methods
    // Selects a tower from the UI
    public void SelectObject(GameObject objectPrefab)
    {
        selectedObjectPrefab = objectPrefab;
        objectSelected = true;
    }

    // De-selects a tower from the UI
    public void DeselectObject()
    {
        selectedObjectPrefab = null;
        objectSelected = false;
    }
    
    #endregion
}
