using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHeld : MonoBehaviour
{
    [SerializeField]
    private InventoryItem inventoryItem;

    public bool ActivateItem(ItemStack itemStack)
    {
        if (itemStack == null)
        {
            gameObject.SetActive(false);
            return false;
        }

        if (inventoryItem.id ==
            itemStack.id)
        {
            gameObject.SetActive(true);
            return true;
        }

        gameObject.SetActive(false);
        return false;
    }
}
