using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopManager : MonoBehaviour
{
    private AudioManager _audioManager;
    public static int maxNumberOfItems = 7;
    private static int _newItemID = 0;
    private int _numberOfSelected = 0;
    public int maxNumberOfSelected = 4;
    [SerializeField] GameObject[] _selectedItemsObjects;
    [SerializeField] TMP_Text[] _selectedItemsTexts;
    private Item[] _selectedItems;
    public TMP_Text selectedCounter;

    public int gems;
    public TMP_Text gemsTxt;

    public int startCoins = 500;
    private int coins;
    public TMP_Text coinsTxt;
    public TMP_Text shopCoinsTxt;

    public TMP_Text messageTxt;

    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        RenderShopMoney();
        selectedCounter.text = _numberOfSelected.ToString() + "/" + maxNumberOfSelected.ToString();
        _selectedItems = new Item[maxNumberOfSelected];
    }

    public void RenderShopMoney()
    {
        gemsTxt.text = gems.ToString();
        shopCoinsTxt.text = startCoins.ToString();
    }

    public void StartGame()
    {
        coins = startCoins;
        coinsTxt.text = coins.ToString();
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
            if (item.isGold)
            {
                startCoins += item.placeablePrice;
                RenderShopMoney();
                _audioManager.PlayUnlockGold();
            }
            else
                _audioManager.PlayUnlockItem();
            gems -= item.price;
            item.unlocked = true;
            gemsTxt.text = gems.ToString();
            return true;
        }
        return false;
    }

    IEnumerator ShowMessageTemporarily(string message, float duration)
    {
        messageTxt.text = message;
        yield return new WaitForSeconds(duration);
        messageTxt.text = "";
    }

    public bool SelectItem(Item item)
    {
        if (item.unlocked && _numberOfSelected < maxNumberOfSelected)
        {
            for (int i = 0; i < maxNumberOfSelected; i++)
            {
                if (_selectedItems[i] == null)
                {
                    _selectedItems[i] = item;
                    _numberOfSelected++;
                    break;
                }
            }
            LeftShift();
            selectedCounter.text = _numberOfSelected.ToString() + "/" + maxNumberOfSelected.ToString();
            return true;
        }
        StartCoroutine(ShowMessageTemporarily("You can't select more items!", 2.0f));
        return false;
    }


    private void LeftShift()
    {   
        // Assuming that there are no more than 1 nulls in the middle of the array: 
        // Preserved order by shifting left for both selecting and deselecting
        for (int i = 0; i < maxNumberOfSelected - 1; i++)
        {
            if (_selectedItems[i] == null)
            {
                _selectedItems[i] = _selectedItems[i + 1];
                _selectedItems[i + 1] = null;
            }
        }
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
        LeftShift();
        selectedCounter.text = _numberOfSelected.ToString() + "/" + maxNumberOfSelected.ToString();
        messageTxt.text = "";
    }

    public GameObject GetSelectedItemPrefab(int index)
    {
        if (index >= 0 && index < _numberOfSelected)
            return _selectedItems[index].prefab;
        return null;
    }

    public void RenderSelectedItems()
    {
        for (int i = 0; i < maxNumberOfSelected; i++)
        {
            if (_selectedItemsObjects[i].GetComponent<Image>() != null && _selectedItems[i])
            {
                _selectedItemsObjects[i].SetActive(true);
                _selectedItems[i].RenderImage(_selectedItemsObjects[i].GetComponent<Image>());
                _selectedItemsTexts[i].text = _selectedItems[i].placeablePrice.ToString();
            }
            else 
                _selectedItemsObjects[i].SetActive(false);
        }
    }

    public bool CanAffordItem(int index)
    {
        if (index >= 0 && index < _numberOfSelected)
            return coins >= _selectedItems[index].placeablePrice;
        return false;
    }

    public bool CanAffordRangeUpgrade(int index)
    {
         if (index >= 0 && index < _numberOfSelected)
         {
            return coins >= _selectedItems[index].rangeUpgradePrice;
         }
        return false;
    }

    public bool PurchaseRangeUpgrade(String name) {
        for (int i = 0; i < maxNumberOfSelected; i++)
            {
                if (_selectedItems[i].name == name && CanAffordRangeUpgrade(i)){
                    coins -= _selectedItems[i].rangeUpgradePrice;
                    coinsTxt.text = coins.ToString();
                    _audioManager.PlayUpgradeSound();
                    return true;
                }
            }
            return false;
    }

    public bool CanAffordDamageUpgrade(int index)
    {
        if (index >= 0 && index < _numberOfSelected)
        {
            return coins >= _selectedItems[index].damageUpgradePrice;
        }
        return false;
    }

    public bool PurchaseDamageUpgrade(String name) {
        for (int i = 0; i < maxNumberOfSelected; i++)
        {
            if (_selectedItems[i].name == name && CanAffordDamageUpgrade(i)) {
                coins -= _selectedItems[i].damageUpgradePrice;
                coinsTxt.text = coins.ToString();
                _audioManager.PlayUpgradeSound();
                return true;
            }
        }
        return false;
    }

    public bool CanAffordFireRateUpgrade(int index)
    {
        if (index >= 0 && index < _numberOfSelected) 
        {
            return coins >= _selectedItems[index].fireRateUpgradePrice;
        }
        return false;
    }

    public bool PurchaseFireRateUpgrade(String name) {
        for (int i = 0; i < maxNumberOfSelected; i++)
            {
                if (_selectedItems[i].name == name && CanAffordFireRateUpgrade(i)){
                    coins -= _selectedItems[i].fireRateUpgradePrice;
                    coinsTxt.text = coins.ToString();
                    _audioManager.PlayUpgradeSound();
                    return true;
                }
            }
        return false;
    }

    public void PurchasePlaceableItem(int index)
    {
        if (index >= 0 && index < _numberOfSelected)
        {
            coins -= _selectedItems[index].placeablePrice;
            coinsTxt.text = coins.ToString();
        }
    }

    public void RefundPlaceableItem(int index)
    {
        if (index >= 0 && index < _numberOfSelected)
        {
            coins += (int)(_selectedItems[index].placeablePrice*0.8f);

            coinsTxt.text = coins.ToString();
        }
    }

    public int GetNumberOfSelectedItems()
    {
        return _numberOfSelected;
    }

    public int GetNumberOfSelectedTowers()
    {
        int count = 0;
        for (int i = 0; i < _numberOfSelected; i++)
        {
            if (_selectedItems[i].isTower)
                count++;
        }
        return count;
    }
}
