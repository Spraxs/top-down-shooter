# Inventory System

### Item Objects
[ItemStack](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/ItemStack.cs)
 is an object class that stores data about a item, such as amount, id and sprite. This is not a scriptable object because I want to change the data for the object itself and not the 'prefab' of a scripable object.

[InventoryItem](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/InventoryItem.cs)
 is a scriptable object that stores data for a ItemStack, this is because I want to set the data of ItemStack within the inspector. 
 
 ### Inventory
 [Inventory](https://github.com/Spraxs/top-down-shooter/tree/master/Assets/Scripts/Inventory/Inventory) is the component that handles all Inventory logic such as item storage, removing items, getting items and contains UpdateInventory delegates so UI classes can listen to the updates.
 
 [InventorySlot](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/Inventory/InventorySlot.cs) is a component for every UI slot of the Inventory it handles item display, such as item sprite display, item amount etc. This component listens to the UpdateInventory delegate in the Inventory component.
 
 ### Hotbar
 [Hotbar](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/Hotbar/Hotbar.cs) is a component that mirrors the Inventory contents by listening to the UpdateInventory delegate.
 
 [HotbarSlot](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/Hotbar/HotbarSlot.cs) is a component for every UI slot of the Hotbar it handles item display, such as item sprite display, item amount etc. The Hotbar component calls function in this component to update the UI for the item display. This component handles item selection and calls a SelectItemAction delegate to display a items in the player's hand.
 
 ### Player
[HandHeld](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/Player/HandHeld.cs) is a component that sits on every item the player can hold and use. It contains a function to be activated so you can see the item in the player his hand.

[PlayerInventory](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/Player/PlayerInventory.cs) is a component that sits on the player. It contains the player his hand object, hotbar for listening to the SelectItemAction and UpdateInventory delegate and also contains all the HandHeld components the player can hold. This component contains the logic to update the holding items and to hold other items.
