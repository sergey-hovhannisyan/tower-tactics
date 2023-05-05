using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private int numOfUpgrades = 0;
    public int maxUpgrades = 5;
    public GameObject rangeIndicator;
    public GameObject AOETower;
    public DamageOverTime damageOverTime;
    public Shoot shoot;
    public TowerAiming towerAiming;
    public GameObject upgradeCanvas;
    public GameObject maxLvlCanvas;
    // public 

    public float rangeIncreaseFactor = 1.25f;
    public float fireRateIncreaseFactor = 1.25f;
    public int bulletDamageIncrease = 5;
    public int damageOverTimeIncrease = 1;
    private Vector3 scaleChange;

    public void IncreaseRange(){
        towerAiming.range *= rangeIncreaseFactor;
        scaleChange = new Vector3(rangeIncreaseFactor, 1f, rangeIncreaseFactor);
        rangeIndicator.transform.localScale = Vector3.Scale(rangeIndicator.transform.localScale, scaleChange);
        if (AOETower != null){
            AOETower.transform.localScale = Vector3.Scale(AOETower.transform.localScale, scaleChange);
        }
        numOfUpgrades++;
        Debug.Log("upgradeCount" + numOfUpgrades);
        checkUpgradeCount();
    }

    public void IncreaseDamage(){
        if(gameObject.tag == "DamageOverTimeTower"){
            damageOverTime.dps += damageOverTimeIncrease;
        }
        else if ( gameObject.tag == "SingleDamageTower"){
            shoot.damage += bulletDamageIncrease;
        }
        numOfUpgrades++;
        checkUpgradeCount();
    }

    public void IncreaseFireRate(){
        shoot.fireRate *= fireRateIncreaseFactor;
        numOfUpgrades++;
        checkUpgradeCount();
    }
    private void checkUpgradeCount(){
        // Debug.Log("upgradeCount" + numOfUpgrades);
        if (numOfUpgrades >= maxUpgrades){
            upgradeCanvas.SetActive(false);
            maxLvlCanvas.SetActive(true);
        }
    }
    
}

