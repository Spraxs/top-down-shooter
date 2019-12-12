   using System;
   using System.Collections;
   using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Action<int> updateInventory; // Integer Item Slot

    public Dictionary<int, ItemStack> contents = new Dictionary<int, ItemStack>();

    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public Sprite emptySlotSprite;

    public InventoryItem defaultItem;

    void Start()
    {
        Clear();

        if (defaultItem != null)
        {
            StartCoroutine(SetDefaultItem());
        }
    }

    private IEnumerator SetDefaultItem()
    {
        yield return new WaitForEndOfFrame();
        AddItem(new ItemStack(defaultItem, 1));
    }

    //TODO Fix item amounts // items now save on item prefab

    // Returns if inventory is full
    public bool AddItem(ItemStack itemStack)
    {

        // Scan for empty slot
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            int slot = inventorySlots[i].slot;

            if (!contents.TryGetValue(slot, out var invItemStack))
            {

                contents.Add(slot, itemStack);

                // Call update action
                updateInventory?.Invoke(slot);

                return true;
            }

            if (contents[inventorySlots[i].slot].id == itemStack.id)
            {
                // Increase inventory amount of itemStack
                invItemStack.amount += itemStack.amount;

                // Call update action
                updateInventory?.Invoke(slot);
                return true;
            }
        }

        // No available slots or items found!
        return false;
    }

    // Return if there where any items to remove
    public bool RemoveItems(ItemStack itemStack)
    {

        // Scan for empty slot
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            int slot = inventorySlots[i].slot;

            if (!contents.TryGetValue(slot, out var invItemStack)) continue;

            if (invItemStack.id == itemStack.id)
            {
                if (invItemStack.amount - itemStack.amount <= 0)
                {

                    // Remove from contents
                    contents.Remove(slot);

                    // Call update action
                    updateInventory?.Invoke(slot);

                    return true;
                }

                // Remove amount from itemStack
                invItemStack.amount -= itemStack.amount;

                // Call update action
                updateInventory?.Invoke(slot);

                return true;
            }
        }

        // No matched items to remove
        return false;
    }

    public bool RemoveItemsFromSlot(int slot, int amount)
    {
        // Slot out of inventory range
        if (slot >= inventorySlots.Count || slot < 0) return false;

        if (!contents.TryGetValue(slot, out var invItemStack)) return false;

        if (invItemStack.amount - amount <= 0)
        {
            // Remove itemStack from contents
            contents.Remove(slot);

            // Call update action
            updateInventory?.Invoke(slot);

            return true;
        }

        // Remove amount from contents
        invItemStack.amount -= amount;

        // Call update action
        updateInventory?.Invoke(slot);
        return true;
    }

    public bool SetItem(int slot, ItemStack itemStack)
    {
        if (slot >= inventorySlots.Count || slot < 0) return false;

        if (!contents.ContainsKey(slot))
        {
            // Add itemStack if not exist
            contents.Add(slot, itemStack);
        }
        else
        {
            // Set itemStack if exists
            contents[slot] = itemStack;
        }

        // Call update action
        updateInventory?.Invoke(slot);

        return true;
    }

    public ItemStack GetItem(int slot)
    {
        if (slot >= inventorySlots.Count || slot < 0) return null;

        return contents.ContainsKey(slot) ? contents[slot] : null;
    }

    private InventorySlot GetInventorySlot(int slot)
    {
        foreach (InventorySlot inventorySlot in inventorySlots)
        {
            if (inventorySlot.slot == slot)
            {
                return inventorySlot;
            }
        }

        return null;
    }

    public void Clear()
    {
        contents.Clear();

        updateInventory?.Invoke(-1);
    }
}
