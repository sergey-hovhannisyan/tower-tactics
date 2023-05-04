using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class InfoTab : MonoBehaviour
{
    public ShopManager shopManager;
    public TMP_Text nameTxt, descriptionTxt, priceTxt;
    public Image image;
    public void RenderInfo(String name, Sprite sourceImage, String description, int price = 0)
    {
        image.sprite = sourceImage;
        nameTxt.text = name;
        descriptionTxt.text = description;
        priceTxt.text = price.ToString();
    }

    public void RenderItemInfo(Item item)
    {
        RenderInfo(item.name, item.image, item.description, item.price);
    }
}
