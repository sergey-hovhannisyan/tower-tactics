using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform spawnPoint;
    public TowerAiming towerAiming;
    private GameObject spawnedLaser;
    private LineRenderer lineRenderer;
    public int dps = 1;
    // Start is called before the first frame update
    void Start()
    {
        spawnedLaser = Instantiate(laserPrefab,spawnPoint.position,spawnPoint.rotation);
        lineRenderer = spawnedLaser.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLaser();
        if (towerAiming.Target != null){
            EnableLaser();
        }
        else{
            DeactivateLaser();
        }
    }
    void UpdateLaser(){
        spawnedLaser.transform.SetPositionAndRotation(spawnPoint.position,spawnPoint.rotation);
    }
    void EnableLaser(){
        lineRenderer.SetPosition(0, spawnPoint.position);
		lineRenderer.SetPosition(1, towerAiming.Target.position + new Vector3(0,0.5f,0));
        spawnedLaser.SetActive(true);
    }
    void DeactivateLaser(){
        spawnedLaser.SetActive(false);
    }
}
