# Inventory System

Features
------

### Item Objects
* [ItemStack](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/ItemStack.cs)
 is an object class that stores data about a item, such as amount, id and sprite. This is not a scriptable object because I want to change the data for the object itself and not the 'prefab' of a scripable object.

* [InventoryItem](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/InventoryItem.cs)
 is a scriptable object that stores data for a ItemStack, this is because I want to set the data of ItemStack within the inspector. 
 
 ### Inventory
* [Inventory](https://github.com/Spraxs/top-down-shooter/tree/master/Assets/Scripts/Inventory/Inventory) is the component that handles all Inventory logic such as item storage, removing items, getting items and contains UpdateInventory delegates so UI classes can listen to the updates.
 
* [InventorySlot](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/Inventory/InventorySlot.cs) is a component for every UI slot of the Inventory it handles item display, such as item sprite display, item amount etc. This component listens to the UpdateInventory delegate in the Inventory component.
 
 ### Hotbar
* [Hotbar](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/Hotbar/Hotbar.cs) is a component that mirrors the Inventory contents by listening to the UpdateInventory delegate.
 
* [HotbarSlot](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/Hotbar/HotbarSlot.cs) is a component for every UI slot of the Hotbar it handles item display, such as item sprite display, item amount etc. The Hotbar component calls function in this component to update the UI for the item display. This component handles item selection and calls a SelectItemAction delegate to display a items in the player's hand.
 
 ### Player
* [HandHeld](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/Player/HandHeld.cs) is a component that sits on every item the player can hold and use. It contains a function to be activated so you can see the item in the player his hand.

* [PlayerInventory](https://github.com/Spraxs/top-down-shooter/blob/master/Assets/Scripts/Inventory/Player/PlayerInventory.cs) is a component that sits on the player. It contains the player his hand object, hotbar for listening to the SelectItemAction and UpdateInventory delegate and also contains all the HandHeld components the player can hold. This component contains the logic to update the holding items and to hold other items.

Software Analyse
------
Ik heb ervoor gekozen om dit systeem in Unity te maken, omdat ik al bezig was met dit project en een Inventory System nodig had. Het was mooi meegenomen dat dit ook nog een opdracht was voor school!

Zelf heb ik ook het meeste ervaring in Unity qua game engines. Daarom heb ik niet voor Unreal Engine gekozen, omdat ik hier nog te weinig ervaring in heb om een Inventory System te maken. Natuurlijk zal het mogelijk zijn, maar ik vond dit onzin aangezien de back-end structuur van een Inventory System bij elke taal wel op het zelfde neer komt (als je het aan mij vraagt).

In Java heb ik zelf veel ervaring, maar ik heb hier toch niet voor gekozen. Omdat ik graag een Inventory System met UI wou maken en dit nogal veel werk is in Java vergeleken met Unity. Ook gebruik ik Java zelf meer voor server-side calculaties en database handling en laat ik de echte gameplay systemen liever aan een game engine over.

Leerdoelen
------
* Delegates voor Inventory updates.
* Objecten maken voor item data.
* Player item in hand veranderen.
* Nieuwe items maken met ScripableObjects
* Hotbar die specifieke slots van inventory weerspiegeld.
* Usable items door middel van geactiveerde scripts op GameObject.
* Opslaan van objecten onder een ID.

Planning
------

|    Maandag    |    Dinsdag    |    Woensdag    | Donderdag | Vrijdag |
| ------------- | ------------- | ------------- | ------------- | ------------- |
| Back-end voor inventory, item opslag en inventory edit functies af | Inventory UI d.m.v Action delegates af | Hotbar UI d.m.v Action delegates af | Player item in hand functie d.m.v Action delegates af | Last bug fixes |

Bronnen
------
* [Inventory UI voorbeeld](https://www.youtube.com/watch?v=-xB4xEmGtCY)
* [Dictionary voor item opslag](https://www.tutorialsteacher.com/csharp/csharp-dictionary)
* [Action delegates](https://www.tutorialsteacher.com/csharp/csharp-action-delegate)
* [Right-click inventory](https://forum.unity.com/threads/can-the-ui-buttons-detect-a-right-mouse-click.279027/)
