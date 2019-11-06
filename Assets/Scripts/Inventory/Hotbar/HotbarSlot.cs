using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    // Slot id of hotbar
    public int inventorySlot;

    // Text for amount of item display
    [SerializeField] Text itemAmountText;

    // Image for item sprite display
    [SerializeField] Image itemImage;

    // Image for background selected color
    private Image backgroundImage;

    // Hotbar
    private Hotbar hotbar;

    // If item is selected
    [HideInInspector]
    public bool selected;

    void Awake()
    {
        hotbar = GetComponentInParent<Hotbar>();
        backgroundImage = GetComponent<Image>();


        itemImage.sprite = hotbar.emptySlotSprite;
        itemAmountText.text = "";
    }

    // Updates hotbar item UI
    public void UpdateItemUi(ItemStack itemStack)
    {
        if (itemStack == null)
        {
            itemAmountText.text = "";

            itemImage.sprite = hotbar.emptySlotSprite;

            return;
        }

        if (itemStack.amount <= 1)
        {
            itemAmountText.text = "";
        }
        else
        {
            itemAmountText.text = itemStack.amount.ToString();
        }


        itemImage.sprite = itemStack.sprite;
    }

    // Sets Hotbar selected color and calls SelectItemAction
    public void SelectItem()
    {
        hotbar.ResetSelectedSlots();
        backgroundImage.color = hotbar.selectedColor;

        selected = true;

        hotbar.SelectItemAction?.Invoke(GetInventoryItem());
    }

    // Resets selected color
    public void ResetSlotColor()
    {
        backgroundImage.color = hotbar.defaultColor;
        selected = false;
    }

    // Get InventoryItem from Inventory by inventorySlot
    private ItemStack GetInventoryItem()
    {
        return hotbar.inventory.GetItem(inventorySlot);
    }
}
