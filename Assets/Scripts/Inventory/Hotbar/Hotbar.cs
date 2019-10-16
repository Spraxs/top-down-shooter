using System;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public Inventory inventory;

    public List<HotbarSlot> displayedSlots = new List<HotbarSlot>();

    public Sprite emptySlotSprite;

    public Color defaultColor;
    public Color selectedColor;

    public Action<ItemStack> SelectItemAction;

    void Start()
    {
        inventory.updateInventory += OnInventoryUpdate;
    }

    // Updates itemslot UI
    private void UpdateItemSlotUi(HotbarSlot hotbarSlot, ItemStack itemStack)
    {
        hotbarSlot.UpdateItemUi(itemStack);
    }

    // Looks for correct slot on inventory update
    public void OnInventoryUpdate(int slot)
    {
        foreach (HotbarSlot displayedSlot in displayedSlots)
        {
            if (displayedSlot.inventorySlot == slot)
            {
                ItemStack itemStack = inventory.contents[slot];
                UpdateItemSlotUi(displayedSlot, itemStack);

                return;
            }
        }
    }

    public void ResetSelectedSlots()
    {
        for (int i = 0; i < displayedSlots.Count; i++)
        {
            displayedSlots[i].ResetSlotColor();
        }
    }
}
