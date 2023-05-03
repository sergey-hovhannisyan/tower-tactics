using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    public GameObject buyButton;
    public GameObject purchasedButton;
    public GameObject backgroundImage;
    public int itemID;
    public string itemName;
    public TMP_Text name;
    public TMP_Text priceTxt;
    public GameObject ShopManager;
    
    void Update()
    {
        name.text = itemName;
        priceTxt.text = ShopManager.GetComponent<ShopManager>().shopItems[1, itemID].ToString();
    }

    public void AfterPurchase()
    {
        backgroundImage.GetComponent<Image>().color = new Color32(21, 96, 99, 255);
        buyButton.SetActive(false);
        purchasedButton.SetActive(true);
    }
}
