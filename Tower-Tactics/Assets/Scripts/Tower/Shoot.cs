using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float fireRate = 1f;
    public float projectileSpeed = 10f;
    public TowerAiming towerAiming;
    private float timeSinceLastShot;
    
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (towerAiming.Target != null && timeSinceLastShot >= 1f / fireRate)
        {
            ShootProjectile();
            timeSinceLastShot = 0f;
        }
    }
    private void FireLaser(){

    }

    private void ShootProjectile()
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        Rigidbody projectileRigidbody = projectileInstance.GetComponent<Rigidbody>();

        if (projectileRigidbody != null)
        {
            projectileRigidbody.velocity = projectileSpawnPoint.forward * projectileSpeed;
        }

        Destroy(projectileInstance, 5f);
    }
}
