using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class InfoTab : MonoBehaviour
{
    public TMP_Text nameTxt, descriptionTxt;
    public Image image;
    public void RenderInfo(String name, Sprite sourceImage, String description)
    {
        image.sprite = sourceImage;
        nameTxt.text = name;
        descriptionTxt.text = description;
    }

    public void TestSergey()
    {
        Debug.Log("Sergey");
    }
}
