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
        // When Inventory updates, the Hotbar also updates
        inventory.updateInventory += OnInventoryUpdate;
    }

    // Looks for correct slot on inventory update
    public void OnInventoryUpdate(int slot)
    {
        foreach (HotbarSlot displayedSlot in displayedSlots)
        {
            if (displayedSlot.inventorySlot == slot)
            {
                if (inventory.contents.TryGetValue(slot, out var itemStack))
                {
                    UpdateItemSlotUi(displayedSlot, itemStack);
                }
                else
                {
                    UpdateItemSlotUi(displayedSlot, null);
                }

                return;
            }
        }
    }

    // Updates itemslot UI
    private void UpdateItemSlotUi(HotbarSlot hotbarSlot, ItemStack itemStack)
    {
        hotbarSlot.UpdateItemUi(itemStack);
    }

    // Resets all selected slots from hotbar
    public void ResetSelectedSlots()
    {
        for (int i = 0; i < displayedSlots.Count; i++)
        {
            displayedSlots[i].ResetSlotColor();
        }
    }
}
