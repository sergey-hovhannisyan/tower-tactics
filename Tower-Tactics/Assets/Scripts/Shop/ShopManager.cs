using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopManager : MonoBehaviour
{
    public static int maxNumberOfItems = 6;
    private static int _newItemID = 0;
    private int _numberOfSelected = 0;
    public int maxNumberOfSelected = 4;
    [SerializeField] Image[] _selectedItemsImages;
    private Item[] _selectedItems;

    public int gems;
    public TMP_Text gemsTxt;

    void Start()
    {
        gemsTxt.text = gems.ToString();

        _selectedItems = new Item[maxNumberOfSelected];
    }

    public Item InstantiateItem(GameObject itemPrefab)
    {
        if (_newItemID >= maxNumberOfItems)
            throw new System.Exception("Too many items in the shop! Increase maxNumberOfItems in ShopManager.cs");
        Item item = Instantiate(itemPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Item>();
        item.itemID = _newItemID++;
        return item;
    }

    public bool Unlock(Item item)
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

    public void SelectItem(Item item)
    {
        if (item.unlocked)
            _selectedItems[_numberOfSelected++] = item;
    }

    public void DeselectItem(Item item)
    {
        if (item.unlocked)
        {
            for (int i = 0; i < _numberOfSelected; i++)
            {
                if (_selectedItems[i] == item)
                {
                    _selectedItems[i] = null;
                    _numberOfSelected--;
                    break;
                }
            }
        }
    }

    public GameObject GetSelectedItemPrefab(int index)
    {
        if (index >= 0 && index < _numberOfSelected)
            return _selectedItems[index].prefab;
        return null;
    }

    public void RenderSelectedItems()
    {
        for (int i = 0; i < _numberOfSelected; i++)
        {
            Debug.Log("Rendering selected item " + _selectedItems[i].name + " with ID " + _selectedItems[i].itemID);
            _selectedItems[i].Render(_selectedItemsImages[i]);
        }
    }
}
