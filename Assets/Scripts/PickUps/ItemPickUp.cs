using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    [SerializeField] private InventoryItem inventoryItem;

    public void OnTriggerEnter2D(Collider2D collder)
    {
        if (collder.gameObject.tag.ToLower() == "player")
        {
            collder.gameObject.GetComponent<PlayerInventory>().hotbar.inventory.AddItem(new ItemStack(inventoryItem, 1));
            Destroy(gameObject);
        }
    }
}
