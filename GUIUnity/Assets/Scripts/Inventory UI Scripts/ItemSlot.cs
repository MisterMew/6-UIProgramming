using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot {
    /// Private Variables
    private UIItemSlot uiItemSlot;
    private bool isAttachedToUI { get { return (uiItemSlot != null); } }
    private int _durability;
    private int _amount;

    /// Public Variables
    public Item item;
    public int itemAmount {
        get { return _amount; }
        set { if (item == null) { _amount = 0; } else if (value > item.itemMaxStack) { _amount = item.itemMaxStack; } else { _amount = value; } RefreshUISlot(); }
    }
    public int itemDurability {
        get { return _durability; }
        set { if (item == null) { _durability = 0; } else if (value > item.itemMaxDurability) { _durability = item.itemMaxDurability; } else { _durability = value; } RefreshUISlot(); }
    }
    public bool containsItem { get { return (item != null); } }
    
     /// ITEM SLOT: Compare
    /* Compare two items and return if they are the same */
    public static bool AreEqual(ItemSlot slotA, ItemSlot slotB) {
        if (slotA.item != slotB.item) { return false; } //Also checks if the item is NULL
        else { return true; }
    }
    
     /// ITEM SLOT: Swap
    /* Swaps the contents of two ItemSlots */
    public static void SwapItems(ItemSlot slotA, ItemSlot slotB) {
        Item tempItem = slotA.item;                  //Cache slotA's item
        int tempAmount = slotA.itemAmount;          //Cache slotA's itemAmount
        int tempDurability = slotA.itemDurability; //Cache slotA's itemAmount
        
        slotA.item = slotB.item;                       //Copy slotB's item to SlotA
        slotA.itemAmount = slotB.itemAmount;          //Copy slotB's itemAmount to SlotA
        slotA.itemDurability = slotB.itemDurability; //Copy slotB's itemAmount to SlotA
    
        slotB.item = tempItem;                   //Copy slotA's item to SlotB
        slotB.itemAmount = tempAmount;          //Copy slotA's itemAmount to SlotB
        slotB.itemDurability = tempDurability; //Copy slotA's itemAmount to SlotB
    
        slotA.RefreshUISlot();
        slotB.RefreshUISlot();
    }
    
     /// UI: Attach
    /* Attaches the item to the UI Slot */
    public void AttachUI(UIItemSlot uiSlot) {
        uiItemSlot = uiSlot;
        uiItemSlot.itemSlot = this;
        RefreshUISlot();
    }

     /// UI: Detach
    /* Detaches the item to the UI Slot */
    public void DetachUI() {
        uiItemSlot.ClearSlot();
        uiItemSlot = null;
    }
    
    /* Refreshes the slot to update */
    public void RefreshUISlot() {
        if (!isAttachedToUI) { return; }
        uiItemSlot.RefreshSlot();
    }
    
    /* Clears this ItemSlot of its contents */
    public void Clear() {
        item = null;
        itemAmount = 0;
        RefreshUISlot();
    }
    
    ///// /// /// /// /// ///
    
     /// ITEM: Find by Name
    /* Finds the item by searching for its name */
    private Item FindByName(string name) {
        name = name.ToLower();                                                //Validate string is lowercase
        Item _item = Resources.Load<Item>(string.Format("Items/{0}", name)); //Load item from resources folder
    
        if (_item == null) { //If item wasn't found
            Debug.LogWarning(string.Format("Item Missing: \"{0}\". Slot is empty.", name));
        }
        
        return _item;
    }
    
     /// ITEM SLOT: Constructor
    /* Constructor for an item slot */
    public ItemSlot(string name, int amount = 1, int durability = 0) {
        Item _item = FindByName(name); //Find the item
    
        if (_item == null) { //If item is NOT found:
            item = null;
            itemAmount = 0;
            itemDurability = 0;
            return;
        }
        else {       /* Otherwise, if found: */
            item = _item;
            itemAmount = amount;
            itemDurability = durability;
            return;
        }
    }

     /// ITEM SLOT: Default Constructor
    /* Default constructor for an item slot */
    public ItemSlot() {
        item = null;
        itemAmount = 0;
        itemDurability = 0;
    }
}   