using UnityEngine;



public class HandHeld : MonoBehaviour
{
    [SerializeField]
    private InventoryItem inventoryItem;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = inventoryItem.sprite;
    }

    // Activate gameObject so player can see item in hand.
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
