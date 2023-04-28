using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float fireRate = 1f;
    public float projectileSpeed = 20f;
    public TowerAiming towerAiming;
    private float timeSinceLastShot;
    public int damage = 5;

    private Transform target;
    private Bullet bullet;
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (towerAiming.Target != null && timeSinceLastShot >= 1f / fireRate)
        {
            target = towerAiming.Target;
            ShootProjectile();
            timeSinceLastShot = 0f;
        }
    }

    private void ShootProjectile()
    {
        Vector3 direction = (target.position - projectileSpawnPoint.position).normalized;
        Vector3 directionYOnly = new Vector3(90, direction.y, 0);
        Quaternion rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(directionYOnly);

        GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, rotation);
        Rigidbody projectileRigidbody = projectileInstance.GetComponent<Rigidbody>();

        if (projectileRigidbody != null)
        {
            float distanceToTarget = Vector3.Distance(projectileSpawnPoint.position, target.position);
            float timeToTarget = distanceToTarget / projectileSpeed;
            Vector3 targetFuturePosition = target.position + target.GetComponent<Rigidbody>().velocity * timeToTarget;

            projectileRigidbody.velocity = (targetFuturePosition - projectileSpawnPoint.position).normalized * projectileSpeed;
        }

        Destroy(projectileInstance, 5f);
    }

}
