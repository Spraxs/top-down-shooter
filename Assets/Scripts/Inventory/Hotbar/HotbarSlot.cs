using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{

    public int inventorySlot;

    [SerializeField] Text itemAmountText;

    [SerializeField] Image itemImage;

    private Image image;

    private Hotbar hotbar;


    [HideInInspector]
    public bool selected;

    void Awake()
    {
        hotbar = GetComponentInParent<Hotbar>();
        image = GetComponent<Image>();

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
        image.color = hotbar.selectedColor;

        selected = true;

        hotbar.SelectItemAction?.Invoke(GetInventoryItem());
    }

    // Resets selected color
    public void ResetSlotColor()
    {
        image.color = hotbar.defaultColor;
        selected = false;
    }

    // Get InventoryItem from Inventory by inventorySlot
    private ItemStack GetInventoryItem()
    {
        return hotbar.inventory.GetItem(inventorySlot);
    }
}
