using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public GameObject itemObject;
}
