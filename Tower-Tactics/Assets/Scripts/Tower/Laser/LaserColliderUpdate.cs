using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class LaserColliderUpdate : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private BoxCollider boxCollider;

    public float colliderThickness = 0.1f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        UpdateCollider();
    }

    private void UpdateCollider()
    {
        if (lineRenderer.positionCount < 2)
        {
            Debug.LogError("LineRenderer needs at least 2 points for the BoxCollider to work.");
            return;
        }

        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        Vector3 midPoint = (startPoint + endPoint) / 2;

        float distance = Vector3.Distance(startPoint, endPoint);
        boxCollider.size = new Vector3(colliderThickness, distance, colliderThickness);

        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, endPoint - startPoint);
        boxCollider.transform.rotation = rotation;

        boxCollider.center = boxCollider.transform.InverseTransformPoint(midPoint);
    }
}