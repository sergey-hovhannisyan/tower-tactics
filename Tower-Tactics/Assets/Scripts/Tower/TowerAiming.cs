using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAiming : MonoBehaviour
{
    public float range = 10f;
    public float rotationSpeed = 7f;
    public Transform turretTransform;
    public LayerMask enemyLayer;

    public Transform Target;
    
    void Update()
    {
        FindClosestEnemy();
        AimAtTarget();
    }

    void FindClosestEnemy()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range, enemyLayer);
        float minDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider enemyCollider in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemyCollider.transform.position);
            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                closestEnemy = enemyCollider.transform;
            }
        }

        Target = closestEnemy;
    }

    void AimAtTarget()
    {
        if (Target != null)
        {
            Vector3 direction = Target.position - turretTransform.position;
            direction.y  = 0;
            Quaternion TargetRotation = Quaternion.LookRotation(direction);
            turretTransform.rotation = Quaternion.Lerp(turretTransform.rotation, TargetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
