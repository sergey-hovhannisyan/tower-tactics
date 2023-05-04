using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopManager : MonoBehaviour
{
    public static int totalNumberOfItems = 6;
    private static int _newItemID = 0;
    private Item[] _shopItems = new Item[totalNumberOfItems];

    public int gems;
    public TMP_Text gemsTxt;

    void Start()
    {
        gemsTxt.text = gems.ToString();
    }

    public Item InstantiateItem(GameObject itemPrefab)
    {
        if (_newItemID >= totalNumberOfItems)
            throw new System.Exception("Too many items in the shop! Increase totalNumberOfItems in ShopManager.cs");
        Item item = Instantiate(itemPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Item>();
        item.itemID = _newItemID++;
        Debug.Log("Instantiated item with ID: " + item.itemID);
        //_shopItems[item.itemID] = item; 
        return item;
    }


    public bool Buy(Item item)
    {
        if (gems >= item.price)
        {
            gems -= item.price;
            item.unlocked = true;
            gemsTxt.text = gems.ToString();
            return true;
        }
        return false;
    }

    public Item[] shopItems
    {
        get { return _shopItems; }
    }
}
