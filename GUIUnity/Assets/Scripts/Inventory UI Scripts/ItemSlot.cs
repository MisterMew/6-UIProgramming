using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot {
    public Item item;
    private int mItemAmount;
    public int itemAmount {
        get { return mItemAmount; }
        set {
            if (item == null) mItemAmount = 0; // Can't have an amount of no item.
            else if (value > item.itemStack) mItemAmount = item.itemStack; // Ensure we don't end up with more items than can stack.
            else if (value < 1) mItemAmount = 0; // Can't have a minus amount of something.
            else mItemAmount = value;
            RefreshUISlot();
        }
    }
    private UIItemSlot uiItemSlot;
    public bool ContainsItem {get { return (item != null); }}

    // If the items or condition are different--or one item is null--this ItemSlot is treated as different.
    public static bool Compare(ItemSlot slotA, ItemSlot slotB) {
        if (slotA.item != slotB.item) { return false; }

        return true;

    }

    // Swaps the contents of two ItemSlots.
    public static void Swap(ItemSlot slotA, ItemSlot slotB) {

        // Cache slotA's values.
        Item _item = slotA.item;
        int _amount = slotA.itemAmount;

        // Copy slotB's values to slotA.
        slotA.item = slotB.item;
        slotA.itemAmount = slotB.itemAmount;

        // Copy the cached slotA values to slotB.
        slotB.item = _item;
        slotB.itemAmount = _amount;

        // Refresh both.
        slotA.RefreshUISlot();
        slotB.RefreshUISlot();
    }

    public void AttachUI(UIItemSlot uiSlot) {
        uiItemSlot = uiSlot;
        uiItemSlot.itemSlot = this;
        RefreshUISlot();
    }

    public void DetachUI() {
        uiItemSlot.ClearSlot();
        uiItemSlot = null;
    }

    // Bool to quickly check if ItemSlot is currently attached to a UIItemSlot.
    private bool isAttachedToUI { get { return (uiItemSlot != null); } }

    public void RefreshUISlot() {
        // If we're not attached to a UIItemSlot, there's nothing to refresh.
        if (!isAttachedToUI) { return; }

        uiItemSlot.RefreshSlot();
    }

    /* Clears this ItemSlot of its contents */
    public void Clear() {
        item = null;
        itemAmount = 0;
        RefreshUISlot();

    }

    /// /// /// /// /// ///

    private Item FindByName(string itemName) {
        itemName = itemName.ToLower(); //Validate string is lowercase
        Item mItem = Resources.Load<Item>(string.Format("Items/{0}")); //Load item from resources folder

        if (mItem == null) {
            Debug.LogWarning(string.Format("Item Missing: \"{0}\". Slot is empty.", itemName));
        }
            return mItem;
    }

    public ItemSlot(string itemName, int amount = 1) {
        Item mItem = FindByName(itemName);

        if (mItem == null) {
            item = mItem;
            amount = itemAmount = 0;
            return;
        }
        else {
            item = mItem;
            amount = itemAmount;
            return;
        }
    }

    public ItemSlot() {
        item = null;
        itemAmount = 0;
    }
}
