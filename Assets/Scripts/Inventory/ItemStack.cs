using UnityEngine;

public class ItemStack
{
    public ItemStack(InventoryItem inventoryItem, int amount)
    {
        id = inventoryItem.id;
        sprite = inventoryItem.sprite;
        name = inventoryItem.name;

        this.amount = amount;
    }

    public int id { private set; get; }
    public Sprite sprite;

    public string name;
    public int amount;
    
}
