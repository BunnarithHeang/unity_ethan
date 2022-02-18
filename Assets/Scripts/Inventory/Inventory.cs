using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Inventory : MonoBehaviour
{
    public event EventHandler OnUseItem;
    public event EventHandler OnDropItem;

    public List<InventoryItem> items { get; private set; }

    public List<GameObject> itemObjects { get; private set; }

    private Color selectedColorCode = new Color(14, 255, 0, 255);
    private Color unselectedColorCode = new Color(255, 255, 255, 255);
    private int selectedIndex = 0;

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
            amountTextUI.GetComponent<TextMeshProUGUI>().SetText(newItem.amount.ToString());
            amountTextUI.SetActive(true);

            // Get the image component, set to new sprite
            Image imageComponent = imageUI.GetComponent("Image") as Image;
            imageComponent.sprite = newItem.GetSprite();

            // Set active
            inventorySlot.SetActive(true);
        }

        SetNewFocusUI();
    }

    public void SetNewFocusIndex()
    {
        selectedIndex++;

        int cnt = 0;
        for (int i = 0; i < itemObjects.Count; ++i) {
            if (itemObjects[i].activeSelf)
                cnt += 1;
        }

        selectedIndex %= cnt;

        SetNewFocusUI();
    }

    private void SetNewFocusUI()
    {
        for (int i = 0; i < itemObjects.Count; ++i)
        {
            ChangeTextLabelStatus(
                i,
                itemObjects[i].activeSelf && i == selectedIndex);
        }
    }

    private void ChangeTextLabelStatus(int index, bool active)
    {
        GameObject itemButton = itemObjects[index].transform.GetChild(0).gameObject;
        GameObject amountTextUI = itemButton.transform.GetChild(1).gameObject;

        amountTextUI.GetComponent<TextMeshProUGUI>().color = active ? selectedColorCode : unselectedColorCode;
        amountTextUI.SetActive(true);
    }

    public void UseItem()
    {
        OnUseItem(items[selectedIndex].type, EventArgs.Empty);
        
        //switch (item.type)
        //{
        //    case InventoryItem.ItemType.Health:
        //    case InventoryItem.ItemType.Meat1:
        //    case InventoryItem.ItemType.Meat2:
        //    case InventoryItem.ItemType.Sword:
        //        OnUseItem(item.type, EventArgs.Empty);
        //        break;
        //    default:
        //        break;
        //}
    }

    public void DropItem()
    {
        OnDropItem(items[selectedIndex].type, EventArgs.Empty); 
        // When remove check the reorder in list make sure that all are in the correct order
    }
}
