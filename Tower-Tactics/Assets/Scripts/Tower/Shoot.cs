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
    
    public AudioClip audioClip;
    private AudioSource audioSource;
    private Transform target;
    private Bullet bullet;

    private void Start(){
        audioSource = GetComponent<AudioSource>();
    }
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
        Vector3 targetPositionYFixed = new Vector3(target.position.x, projectileSpawnPoint.position.y, target.position.z);

        GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        projectileInstance.transform.LookAt(targetPositionYFixed);
        Rigidbody projectileRigidbody = projectileInstance.GetComponent<Rigidbody>();
        audioSource.PlayOneShot(audioClip);

        if (projectileRigidbody != null)
        {
            float distanceToTarget = Vector3.Distance(projectileSpawnPoint.position, target.position);
            float timeToTarget = distanceToTarget / projectileSpeed;
            Vector3 targetFuturePosition = targetPositionYFixed + target.GetComponent<Rigidbody>().velocity * timeToTarget;

            projectileRigidbody.velocity = (targetFuturePosition - projectileSpawnPoint.position).normalized * projectileSpeed;
        }

        Destroy(projectileInstance, 5f);
    }


    public void upgradeDamage(int increment)
    {
        damage += increment;
    }

}
