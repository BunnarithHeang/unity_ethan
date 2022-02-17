using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItem
{
    public enum ItemType
    {
        Health, Sword, Meat1, Meat2
    }

    private static Dictionary<string, ItemType> itemsTypes = new Dictionary<string, ItemType>() {
        { "ItemPotionHealth", ItemType.Health },
        { "ItemSword", ItemType.Sword },
    };

    public ItemType type { get; private set; }
    public int amount { get; private set; }

    public InventoryItem(ItemType type)
    {
        this.type = type;
        this.amount = 1;
    }

    public void ModifyAmount(int amount)
    {
        this.amount += amount;
    }

    static public string TagFromType(ItemType type)
    {
        return itemsTypes.First(e => e.Value == type).Key;
    }

    static public ItemType TypeFromTag(string tag)
    {
        return itemsTypes[tag];
    }

    public bool IsStackable()
    {
        switch (type)
        {
            case ItemType.Health:
            case ItemType.Meat1:
            case ItemType.Meat2:
                return true;
            default:
                return false;
        }
    }

    public Sprite GetSprite()
    {
        switch (type)
        {
            case ItemType.Sword: return ItemAssets.Instance.swordSprite;
            case ItemType.Health: return ItemAssets.Instance.healthSprite;
            case ItemType.Meat1: return ItemAssets.Instance.meat1Sprite;
            case ItemType.Meat2: return ItemAssets.Instance.meat2Sprite;
            default:
                return ItemAssets.Instance.healthSprite;
        }
    }

    public static bool IsInventoryItem(string tag)
    {
        return itemsTypes.Keys.ToList().Contains(tag);
    }
}
