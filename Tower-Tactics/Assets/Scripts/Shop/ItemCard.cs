using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCard : MonoBehaviour
{
    public GameObject unlockButton;
    public GameObject selectButton;
    public GameObject selectedCheckMarkButton;

    public GameObject backgroundImage;
    public Image itemImage;
    public TMP_Text nameTxt;
    public GameObject itemPrefab;
    public TMP_Text priceTxt;
    public GameObject gemsIcon;
    public GameObject price;

    private Item _item;


    private ShopManager _shopManager;
    
    void Awake()
    {
        _shopManager = FindObjectOfType<ShopManager>();
        _item = _shopManager.InstantiateItem(itemPrefab);
        Debug.Log("Instantiated item " + _item.name + " with ID " + _item.itemID);
        itemImage.sprite = _item.image;
        priceTxt.text = _item.price.ToString();
        nameTxt.text = _item.name;
    }

    public void Unlock()
    {
        if (_shopManager.Unlock(_item))
        {
            backgroundImage.GetComponent<Image>().color = new Color32(21, 96, 99, 255);
            unlockButton.SetActive(false);
            selectButton.SetActive(true);
        }
    }

    public void Select()
    {
        if (_item.unlocked)
        {
            // Managing button states
            _item.selected = true;
            selectButton.SetActive(false);
            price.SetActive(false);
            gemsIcon.SetActive(false);
            selectedCheckMarkButton.SetActive(true);

            // Adding item to selected items
            _shopManager.SelectItem(_item);

        }
    }

    public void Deselect()
    {
        _item.selected = false;
        selectButton.SetActive(true);
        price.SetActive(true);
        gemsIcon.SetActive(true);
        selectedCheckMarkButton.SetActive(false);
        _shopManager.DeselectItem(_item);
    }

    public void RenderItemInfo()
    {
        InfoTab infoTab = GameObject.FindGameObjectWithTag("InfoTab").GetComponent<InfoTab>();
        infoTab.RenderItemInfo(_item);
    }
}
