 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 offset;

    private void OnTouchDown()
    {
        offset = transform.position - BuildingSystem.GetTouchWorldPosition();
    }

    private void ONTouchDrag()
    {
       Vector3 pos = BuildingSystem.GetTouchWorldPosition() + offset;
       transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);
    }
}
