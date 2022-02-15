using UnityEngine;

public class InventoryItem
{
    public enum ItemType
    {
        Health, Sword, Meat1, Meat2
    }

    public ItemType type;
    public int amount;

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

    public Color GetColor()
    {
        switch (type)
        {
            default:
            case ItemType.Sword: return new Color(1, 1, 1);
            //case ItemType.HealthPotion: return new Color(1, 0, 0);
            //case ItemType.ManaPotion: return new Color(0, 0, 1);
            //case ItemType.Coin: return new Color(1, 1, 0);
            //case ItemType.Medkit: return new Color(1, 0, 1);
        }
    }
}
