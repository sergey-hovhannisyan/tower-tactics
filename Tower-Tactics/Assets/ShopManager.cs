using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    private static int itemInfoDepth = 2;
    private static int numberOfItems = 6;
    public int[,] shopItems = new int[itemInfoDepth, numberOfItems];
    public bool[] unlockedItems = new bool[numberOfItems];
    public int gems;
    public TMP_Text gemsTxt;



    void Start()
    {
        gemsTxt.text = gems.ToString();

        // IDs
        shopItems[0,0] = 0;
        shopItems[0,1] = 1;
        shopItems[0,2] = 2;
        shopItems[0,3] = 3;
        shopItems[0,4] = 4;
        shopItems[0,5] = 5;

        //Price
        shopItems[1,0] = 10;
        shopItems[1,1] = 20;
        shopItems[1,2] = 30;
        shopItems[1,3] = 40;
        shopItems[1,4] = 50;
        shopItems[1,5] = 60;

        //Unlocked
        unlockedItems[0] = false;
        unlockedItems[1] = false;
        unlockedItems[2] = false;
        unlockedItems[3] = false;
        unlockedItems[4] = false;
        unlockedItems[5] = false;

    }


    public void Buy()
    {
        GameObject button = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        ItemInfo info = button.transform.parent.gameObject.GetComponent<ItemInfo>();
        int itemID = info.itemID;
        if (gems >= shopItems[1, itemID])
        {
            gems -= shopItems[1, itemID];
            unlockedItems[itemID] = true;
            gemsTxt.text = gems.ToString();
            info.AfterPurchase();
        }
    }
}
