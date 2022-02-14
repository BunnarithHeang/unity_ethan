using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<InventoryItem> itemList;

    public Inventory()
    {
        itemList = new List<InventoryItem>();

        Debug.Log("Inventory");
    }

    public void AddItem(InventoryItem item)
    {
        itemList.Add(item);
    }
}
