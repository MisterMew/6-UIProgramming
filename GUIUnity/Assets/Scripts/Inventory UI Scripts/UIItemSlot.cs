using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
* This class represents an inventory/container/etc slot in the UI. This could be an inventory window, a container window, a toolbelt.
* UIItemSlots are "attached" to an ItemSlot when used, and the information from that ItemSlot is show in the UI Elements.
*/

public class UIItemSlot : MonoBehaviour {
    /// Variables
    public bool isCursor = false;
    
    public RectTransform slotRect;
    public ItemSlot itemSlot;
    public Image icon;
    public Text amount;
    public Image durability;

    /// AWAKE
    /* As soon as the game is awake */
    private void Awake() {
        slotRect = GetComponent<RectTransform>();
        if (isCursor) {
            itemSlot = new ItemSlot(); //Validates we never have a null ItemSlot
        }

        icon.gameObject.SetActive(true);
        amount.gameObject.SetActive(true);
        durability.gameObject.SetActive(true);
    }
    
     /// UPDATE
    /* Updates the UIItemSlot */
    private void Update() {
        if (!isCursor) { return; } 
        transform.position = Input.mousePosition; //Update position relative to mouse cursor
    }

     /// SLOT: Refresh
    /* Refresh the item slot */
    public void RefreshSlot() {
        UpdateIcon();
        UpdateAmount();
        UpdateDurability();
    }
    
     /// SLOT: Clear
    /* Clears the item slot */
    public void ClearSlot() {
        itemSlot = new ItemSlot();
        RefreshSlot();
    }
    
     /// UPDATE: Item Icon
    /* Update the items icon sprite */
    public void UpdateIcon() {
        if (itemSlot == null || !itemSlot.containsItem) { icon.enabled = false; }
        else {
            icon.enabled = true;
            icon.sprite = itemSlot.item.itemSprite;
        }
    }
    
     /// UPDATE: Item Amount
    /* Update the items amount */
    public void UpdateAmount() {
        if (itemSlot == null || !itemSlot.containsItem || itemSlot.itemAmount < 2) { amount.enabled = false; } //If the item is non-stackable, disbale the amount
        else {                                                                                                //Otherwise just display the amount.
            amount.enabled = true;
            amount.text = itemSlot.itemAmount.ToString();
        }
    }
    
     /// UPDATE: Item Durability
    /* Update the items durablity */
    public void UpdateDurability() {
        if (isCursor) { return; }
        if (itemSlot == null || !itemSlot.containsItem || !itemSlot.item.isDegradable) { durability.enabled = false; } //Cases where condition bar is not needed
        else {                                                                                                        //Otherwise calculate how much to display
            durability.enabled = true;

            float durabilityPercent = (float)itemSlot.itemDurability / (float)itemSlot.item.itemMaxDurability; //Get the normalised percentage of condition (0 - 1)
            float indicatorWidth = slotRect.rect.width * durabilityPercent;                                   //Multiply max width by that normalised percentage to get width

            durability.rectTransform.sizeDelta = new Vector2(indicatorWidth, durability.rectTransform.sizeDelta.y); //Transform the width. We have to pass in a Vector2 so keep same value for the y variable
            durability.color = Color.Lerp(Color.red, Color.cyan, durabilityPercent);                               //Lerp colour from blue to red as it becomes more degraded
        }
    }
}   