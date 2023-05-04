using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public String name; 
    public String description;
    public int price;
    public Sprite image;
    public GameObject prefab;
    
    private static int _numberOfItems;
    private int _itemID;
    private bool _unlocked = false;
    private bool _selected = false;
    private int _level = 1;

    public int itemID
    {
        get { return _itemID; }
        set { _itemID = value;
              _numberOfItems++;}
    }

    public bool unlocked
    {
        get { return _unlocked; }
        set { _unlocked = value; }
    }

    public bool selected
    {
        get { return _selected; }
        set { _selected = value; }
    }

    public int level
    {
        get { return _level; }
        set { _level = value; }
    }

    public static int numberOfItems
    {
        get { return _numberOfItems; }
    }
}
