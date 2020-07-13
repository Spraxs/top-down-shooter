using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    // Inventory slot id 
    public int slot;

    // Inventory
    private Inventory inventory;

    // Text for display of item amount
    [SerializeField]
    private Text itemAmount;

    // Image for display of item sprite
    [SerializeField]
    private Image itemImage;

    void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
        inventory.updateInventory += UpdateItemSlot;
    }

    void OnEnable()
    {
        // Get correct item from inventory

        // Check if item is null
        if (!inventory.contents.TryGetValue(slot, out var itemStack))
        {
            // Set text empty if was not empty
            if (itemAmount.text != "")
            {
                itemAmount.text = "";
            }

            // Set image empty if was not empty
            if (!itemImage.sprite.Equals(inventory.emptySlotSprite))
            {
                itemImage.sprite = inventory.emptySlotSprite;
            }

            return;
        }

        // Set image if image of itemStack do not match
        if (!itemStack.sprite.Equals(itemImage.sprite))
        {
            itemImage.sprite = itemStack.sprite;
        }

        // Set amount if amount of itemStack do not match
        if (!itemAmount.text.Equals(itemStack.amount.ToString()))
        {
            itemAmount.text = itemStack.amount.ToString();
        }
    }

    public void UpdateItemSlot(int _slot)
    {
        if (_slot == -1)
        {
            // Clear inventory slot
            UpdateItem(null);
        }
        if (slot == _slot)
        {
            if (!inventory.contents.TryGetValue(_slot, out var itemStack))
            {
                UpdateItem(null);
            }
            else
            {
                UpdateItem(itemStack);
            }
        }
    }

    private void UpdateItem(ItemStack itemStack)
    {
        if (itemStack == null)
        {
            itemAmount.text = "";

            itemImage.sprite = inventory.emptySlotSprite;

            return;
        }

        if (itemStack.amount <= 1)
        {
            itemAmount.text = "";
        }
        else
        {
            itemAmount.text = itemStack.amount.ToString();
        }
       
        itemImage.sprite = itemStack.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left");
        } else if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventory.RemoveItemsFromSlot(slot, 1);
        }
    }
}
