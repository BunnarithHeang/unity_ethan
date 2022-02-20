using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Inventory : MonoBehaviour
{
    public event EventHandler OnUseItem;
    public event EventHandler OnDropItem;

    public List<InventoryItemUI> items { get; private set; } 

    private Color selectedColorCode = new Color(14, 255, 0, 255);
    private Color unselectedColorCode = new Color(255, 255, 255, 255);
    private int selectedIndex = 0;

    public Inventory(List<GameObject> objects)
    {
        items = new List<InventoryItemUI>();

        for (int i = 0; i < objects.Count; ++i)
        {
            items.Add(new InventoryItemUI(null, objects[i]));
        }
    }

    public void AddItemBy(string tag)
    {
        // Find if inventory currently have item with the same tag
        int index = items.FindIndex(
            item => InventoryItem.TagFromType(item.item?.type) == tag);

        // If in inventory && stackable, just increament the amount
        if (index > -1 && items[index].item.IsStackable())
        {
            items[index].item.ModifyAmount(1);

            UpdateItemSlotUI(index);
        }
        else // Add to new slot if not in inventory or not stackable
        {
            if (index == -1)
            {
                int lastIndex = items.FindIndex(item => !item.gameObject.activeSelf);

                items[lastIndex].item = new InventoryItem(InventoryItem.TypeFromTag(tag));

                UpdateItemSlotUI(lastIndex);
            }
            else return;
        }

        SetNewFocusUI();
    }

    public void SetNewFocusIndex()
    {
        int cnt = 0;
        for (int i = 0; i < items.Count; ++i) {
            if (items[i].gameObject.activeSelf)
                cnt += 1;
        }

        if (cnt > 0)
        {
            selectedIndex++;
            selectedIndex %= cnt;

            SetNewFocusUI();
        }
    }

    private void SetNewFocusUI()
    {
        items = items.OrderBy(x => !x.gameObject.activeSelf).ToList();

        for (int i = 0; i < items.Count; ++i)
        {
            ChangeTextLabelStatus(
                i,
                items[i].gameObject.activeSelf && i == selectedIndex);
        }
    }

    private void ChangeTextLabelStatus(int index, bool active)
    {
        GameObject itemButton = items[index].gameObject.transform.GetChild(0).gameObject;
        GameObject amountTextUI = itemButton.transform.GetChild(1).gameObject;

        amountTextUI.GetComponent<TextMeshProUGUI>().color = active ? selectedColorCode : unselectedColorCode;
        amountTextUI.SetActive(true);
    }

    public void UseItem()
    {
        if (items[selectedIndex].item == null) return;

        OnUseItem(items[selectedIndex].item, EventArgs.Empty);

        if (items[selectedIndex].item.IsStackable())
        {
            items[selectedIndex].item.ModifyAmount(-1);

            UpdateItemSlotUI(selectedIndex);

            if (items[selectedIndex].item.amount <= 0) 
                items[selectedIndex].SetEmpty();
        }
        else
        {
            items[selectedIndex].SetEmpty();
        }

        if (selectedIndex > 0 && items[selectedIndex].item == null)
            selectedIndex--;

        SetNewFocusUI();
    }

    public void DropItem()
    {
        if (items[selectedIndex].item == null) return;

        // Animate drop animation
        OnDropItem(items[selectedIndex].item, EventArgs.Empty);

        if (items[selectedIndex].item.IsStackable())
        {
            items[selectedIndex].item.ModifyAmount(-1);

            UpdateItemSlotUI(selectedIndex);

            if (items[selectedIndex].item.amount <= 0)
                items[selectedIndex].SetEmpty();
        }
        else
        {
            items[selectedIndex].SetEmpty();
        }
        

        if (selectedIndex > 0 && items[selectedIndex].item == null)
            selectedIndex--;

        SetNewFocusUI();
    }

    private void UpdateItemSlotUI(int index)
    {
        // Get the slot
        GameObject inventorySlot = items[index].gameObject;

        // Get the button children
        GameObject itemButton = inventorySlot.transform.GetChild(0).gameObject;

        // Get the image child
        GameObject imageUI = itemButton.transform.GetChild(0).gameObject;

        // Get the image component, set to new sprite
        Image imageComponent = imageUI.GetComponent("Image") as Image;
        imageComponent.sprite = items[index].item.GetSprite();

        // Amount child
        GameObject amountTextUI = itemButton.transform.GetChild(1).gameObject;
        amountTextUI.GetComponent<TextMeshProUGUI>().SetText(
            items[index].item.amount.ToString());
        amountTextUI.SetActive(true);

        // Set active
        inventorySlot.SetActive(true);
    }
}
