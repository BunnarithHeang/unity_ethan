using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Inventory : MonoBehaviour
{
    public event EventHandler OnItemListChanged;
    private Action<InventoryItem> useItemAction;

    public List<InventoryItem> items { get; private set; }

    public List<GameObject> itemObjects { get; private set; }

    public Inventory(List<GameObject> objects)
    {
        itemObjects = objects ?? new List<GameObject>();

        items = new List<InventoryItem>();
    }

    public void SetItem(InventoryItem item)
    {
        itemObjects[0].SetActive(true);
        GameObject itemButton = itemObjects[0].transform.GetChild(0).gameObject;
        GameObject child = itemButton.transform.GetChild(0).gameObject;
        Image childImage = child.GetComponent("Image") as Image;

        item.type = InventoryItem.ItemType.Health;

        childImage.sprite = item.GetSprite();
        print(child.tag);
    }

    public void AddItem(InventoryItem item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (InventoryItem inventoryItem in items)
            {
                if (inventoryItem.type == item.type)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                items.Add(item);
            }
        }
        else
        {
            items.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(InventoryItem item)
    {
        if (item.IsStackable())
        {
            InventoryItem itemInInventory = null;
            foreach (InventoryItem inventoryItem in items)
            {
                if (inventoryItem.type == item.type)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                items.Remove(itemInInventory);
            }
        }
        else
        {
            items.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(InventoryItem item)
    {
        useItemAction(item);
    }

    public List<InventoryItem> GetItemList()
    {
        return items;
    }

}
