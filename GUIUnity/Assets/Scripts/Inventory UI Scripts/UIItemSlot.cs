using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class represents an inventory/container/etc slot in the UI. This could be an inventory window, a
// container window, a toolbelt. UIItemSlots are "attached" to an ItemSlot when used, and the information
// from that ItemSlot is show in the UI Elements.
public class UIItemSlot : MonoBehaviour {
    /// Variables
    public bool isCursor = false;

    RectTransform slotRect;
    public ItemSlot itemSlot;
    public Image itemIcon;
    public Text amount;

    /// AWAKE
    /* As soon as UIItemSlot exists */
    private void Awake() {
        slotRect = GetComponent<RectTransform>();
        itemSlot = new ItemSlot(); //Validates we never have a null ItemSlot
    }

    /// UPDATE
    /* Updates the UIItemSlot */
    private void Update() {
        if (!isCursor) { return; } 
        transform.position = Input.mousePosition; //Update position relative to mouse cursor
    }


    /* Refresh the item slot */
    public void RefreshSlot() {
        UpdateAmount();
        UpdateIcon();
    }

    /* Clears the item slot */
    public void ClearSlot() {
        itemSlot = null;
        RefreshSlot();
    }

     /// UPDATE: Item Icon
    /* Update the items icon sprite */
    public void UpdateIcon() {
        if (itemSlot == null || !itemSlot.ContainsItem) { itemIcon.enabled = false; }
        else {
            itemIcon.enabled = true;
            itemIcon.sprite = itemSlot.item.itemSprite;
        }
    }

     /// UPDATE: Item Amount
    /* Update the items amount */
    public void UpdateAmount() {
        if (itemSlot == null || !itemSlot.ContainsItem || itemSlot.itemAmount < 2) { amount.enabled = false; } //If the item is non-stackable, disbale the amount
        else {                                                                                                //Otherwise just display the amount.
            amount.enabled = true;
            amount.text = itemSlot.itemAmount.ToString();
        }
    }
}