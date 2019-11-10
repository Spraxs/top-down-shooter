using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    
    public Hotbar hotbar;

    [SerializeField]
    public GameObject hand;

    private HandHeld[] handHelds;

    void Start()
    {
        handHelds = hand.GetComponentsInChildren<HandHeld>();
        hotbar.SelectItemAction += HoldItem;
        hotbar.inventory.updateInventory += UpdateItem;

        foreach (HandHeld handHeld in handHelds)
        {
            handHeld.gameObject.SetActive(false);
        }
    }

    public void HoldItem(ItemStack itemStack)
    {
        
        for (int i = 0; i < handHelds.Length; i++)
        {
            handHelds[i].ActivateItem(itemStack);
        }
    }

    public void UpdateItem(int slot)
    {
        slot = -1;

        for (int i = 0; i < hotbar.displayedSlots.Count; i++)
        {
            HotbarSlot hSlot = hotbar.displayedSlots[i];

            if (hSlot.selected)
            {
                slot = hSlot.inventorySlot;
                break;
            }
        }

        if (slot == -1) return;

        for (int i = 0; i < handHelds.Length; i++)
        {

            if (hotbar.inventory.contents.TryGetValue(slot, out ItemStack itemStack))
            {
                handHelds[i].ActivateItem(itemStack);
            }
            else
            {
                handHelds[i].gameObject.SetActive(false);
            }
        }
    }

}
