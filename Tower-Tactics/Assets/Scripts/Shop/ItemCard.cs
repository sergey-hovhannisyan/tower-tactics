using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCard : MonoBehaviour
{
    public GameObject buyButton;
    public GameObject purchasedButton;
    public GameObject backgroundImage;
    public Image itemImage;
    public TMP_Text nameTxt;
    public GameObject itemPrefab;
    private Item _item;
    public TMP_Text priceTxt;

    private ShopManager _shopManager;
    
    void Awake()
    {
        _shopManager = FindObjectOfType<ShopManager>();
        _item = _shopManager.InstantiateItem(itemPrefab);
        itemImage.sprite = _item.image;
        priceTxt.text = _item.price.ToString();
        nameTxt.text = _item.name;
    }

    public void Buy()
    {
        if (_shopManager.Buy(_item))
        {
            backgroundImage.GetComponent<Image>().color = new Color32(21, 96, 99, 255);
            buyButton.SetActive(false);
            purchasedButton.SetActive(true);
        }
    }

    public void RenderItemInfo()
    {
        InfoTab infoTab = GameObject.FindGameObjectWithTag("InfoTab").GetComponent<InfoTab>();
        infoTab.RenderItemInfo(_item);
    }
}
