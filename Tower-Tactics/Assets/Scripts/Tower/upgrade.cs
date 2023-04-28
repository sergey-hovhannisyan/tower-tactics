using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrade : MonoBehaviour
{
    private int numOfUpgrades = 0;
    public int maxUpgrades = 5;
    public GameObject rangeIndicator;
    public GameObject AOETower;
    public Shoot shoot;
    public TowerAiming towerAiming;
    public GameObject upgradeCanvas;
    public GameObject maxLvlCanvas;
    // public 

    public float rangeIncreaseFactor = 1.25f;
    public float fireRateIncreaseFactor = 1.25f;
    private Vector3 scaleChange;

    public void IncreaseRange(){
        towerAiming.range *= rangeIncreaseFactor;
        scaleChange = new Vector3(rangeIncreaseFactor, 1f, rangeIncreaseFactor);
        rangeIndicator.transform.localScale = Vector3.Scale(rangeIndicator.transform.localScale, scaleChange);
        numOfUpgrades++;
        if (AOETower != null){
            AOETower.transform.localScale = Vector3.Scale(AOETower.transform.localScale, scaleChange);
        }
        checkUpgradeCount();
    }

    public void IncreaseDamage(){
        if(gameObject.tag == "laserTower"){

        }
        else if (gameObject.tag == "CannonTower"){

        }
    }

    public void IncreaseFireRate(){
        shoot.fireRate *= fireRateIncreaseFactor;
        numOfUpgrades++;
        checkUpgradeCount();
    }
    private void checkUpgradeCount(){
        if (numOfUpgrades >= maxUpgrades){
            upgradeCanvas.SetActive(false);
            maxLvlCanvas.SetActive(true);
        }
    }
}

