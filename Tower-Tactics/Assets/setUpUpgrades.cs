using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class setUpUpgrades : MonoBehaviour
{
    public ShopManager shopManager;
    public Upgrade upgrade;
    private GameObject range;
    private GameObject damage;
    private GameObject firerate;
    public bool hasFireRate;
    public String name;
    // Start is called before the first frame update
    void Start()
    {
        shopManager = GameObject.FindGameObjectWithTag("Core").GetComponent<ShopManager>();  
        range = gameObject.transform.GetChild(0).gameObject;
        range.GetComponent<Button>().onClick.AddListener(delegate{attemptRangeUpgrade();});
        damage = gameObject.transform.GetChild(2).gameObject;
        damage.GetComponent<Button>().onClick.AddListener(delegate{attemptDamageUpgrade();});
        if (hasFireRate) 
        {
            firerate = gameObject.transform.GetChild(4).gameObject;
            firerate.GetComponent<Button>().onClick.AddListener(delegate{attemptFireRateUpgrade();});
        }
    }

    private void attemptRangeUpgrade(){
        if(shopManager.PurchaseRangeUpgrade(name))
        {
            upgrade.IncreaseRange();
        }
    }
    private void attemptDamageUpgrade(){
        if(shopManager.PurchaseDamageUpgrade(name))
        {
            upgrade.IncreaseDamage();
        }
    }
    private void attemptFireRateUpgrade(){
        if(shopManager.PurchaseFireRateUpgrade(name))
        {
            upgrade.IncreaseFireRate();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
