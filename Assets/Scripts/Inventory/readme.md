### Inventory System

## Item Objects
[ItemStack](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/ItemStack.cs)
 is an object class that stores data about a item, such as amount, id and sprite. This is not a scriptable object because I want to change the data for the object itself and not the 'prefab' of a scripable object.

[InventoryItem](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/InventoryItem.cs)
 is a scriptable object that stores data for a ItemStack, this is because I want to set the data of ItemStack within the inspector. 
