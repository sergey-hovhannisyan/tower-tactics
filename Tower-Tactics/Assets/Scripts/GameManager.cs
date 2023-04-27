using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera mainCam;
    public GameObject selectedObjectPrefab;
    private bool objectSelected = false;

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
         
    }

    private void Update()
    {
        
    }

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
        if (selectedObjectPrefab)
        {
            GameObject newTower = Instantiate(selectedObjectPrefab, cell.transform.position, Quaternion.identity);
            cell.objectInThisGridSpace = newTower;
            cell.isOccupied = true;
        }
    }

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
}
