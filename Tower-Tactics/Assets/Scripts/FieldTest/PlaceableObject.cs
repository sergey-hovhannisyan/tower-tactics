using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaceableObject : MonoBehaviour
{
    public bool Placed { get; private set; }
    public Vector3Int Size { get; private set; }
    public Vector3[] Vertices; 

    private void GetColliderVertexPositionsLocal() 
    {
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        Vertices = new Vector3[4];
        Vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f;
        Vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f;
    }

    private void CalculateSizeInCells()
    {
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];
        
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(Vertices[i]);
            vertices[i] = BuildingSystem.current.gridLayout.WorldToCell(worldPos);
        }

        Vector2Int size1 = new Vector2Int();
        size1.x = Mathf.Abs(vertices[0].x - vertices[1].x);
        size1.y = Mathf.Abs(vertices[0].y - vertices[3].y);

        Vector2Int size2 = new Vector2Int();
        size2.x = Mathf.Abs(vertices[1].x - vertices[2].x);
        size2.y = Mathf.Abs(vertices[1].y - vertices[2].y);

        if (size1.x > size2.x)
        {
            Size = new Vector3Int(size1.x, size1.y, 1);
        }
        else
        {
            Size = new Vector3Int(size2.x, size2.y, 1);
        }
    }


    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(Vertices[0]);
    }

    private void Start()
    {
        GetColliderVertexPositionsLocal();
        CalculateSizeInCells();
    }

    public virtual void Place()
    {
        DragObject drag = gameObject.GetComponent<DragObject>();
        Destroy(drag);

        Placed = true;
    }
}
