using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Inventory : MonoBehaviour
{
    private Action<InventoryItem> useItemAction;

    public List<InventoryItem> items { get; private set; }

    public List<GameObject> itemObjects { get; private set; }

    private string selectedColorCode = "0EFF00";
    private string unselectedColorCode = "FFFFFF";
    private int collectingIndex;

    public Inventory(List<GameObject> objects)
    {
        itemObjects = objects ?? new List<GameObject>();

        items = new List<InventoryItem>();
    }

    public void AddItemBy(string tag)
    {
        // Find if inventory currently have item with the same tag
        int index = items.FindIndex(
            item => InventoryItem.TagFromType(item.type) == tag);

        // If in inventory && stackable, just increament the amount
        if (index > -1 && items[index].IsStackable())
        {
            items[index].ModifyAmount(1);

            // Get the slot
            GameObject inventorySlot = itemObjects[index];

            // Get the button children
            GameObject itemButton = inventorySlot.transform.GetChild(0).gameObject;

            // Amount child
            GameObject amountTextUI = itemButton.transform.GetChild(1).gameObject;

            amountTextUI.GetComponent<TextMeshProUGUI>().SetText(items[index].amount.ToString());
            amountTextUI.SetActive(true);

            // Set active
            inventorySlot.SetActive(true);
        }
        else // Add to new slot if not in inventory or not stackable
        {
            InventoryItem newItem = new InventoryItem(InventoryItem.TypeFromTag(tag));
            items.Add(newItem);

            // Find the last active index
            int lastIndex = itemObjects.FindIndex(item => !item.activeSelf);

            // Get the slot
            GameObject inventorySlot = itemObjects[lastIndex];

            // Get the button children
            GameObject itemButton = inventorySlot.transform.GetChild(0).gameObject;

            // Get the image child
            GameObject imageUI = itemButton.transform.GetChild(0).gameObject;

            // Amount child
            GameObject amountTextUI = itemButton.transform.GetChild(1).gameObject;
            amountTextUI.SetActive(false);

            // Get the image component, set to new sprite
            Image imageComponent = imageUI.GetComponent("Image") as Image;
            imageComponent.sprite = newItem.GetSprite();

            // Set active
            inventorySlot.SetActive(true);
        }
    }

    public void RemoveItem()
    {
        // When remove check the reorder in list make sure that all are in the correct order

        //if (item.IsStackable())
        //{
        //    InventoryItem itemInInventory = null;
        //    foreach (InventoryItem inventoryItem in items)
        //    {
        //        if (inventoryItem.type == item.type)
        //        {
        //            inventoryItem.amount -= item.amount;
        //            itemInInventory = inventoryItem;
        //        }
        //    }
        //    if (itemInInventory != null && itemInInventory.amount <= 0)
        //    {
        //        items.Remove(itemInInventory);
        //    }
        //}
        //else
        //{
        //    items.Remove(item);
        //}
        //OnItemListChanged?.Invoke(this, EventArgs.Empty);
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
