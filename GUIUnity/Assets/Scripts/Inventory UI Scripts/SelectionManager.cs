using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour {
    /// Variables
    GraphicRaycaster raycaster;
    PointerEventData pointer;
    EventSystem eventsystem;
    
    public UIItemSlot cursor; //The dragged item slot.
    
     /// AWAKE
    /* As soon as the Selection Manager exists */
    private void Awake() {
        raycaster = GetComponent<GraphicRaycaster>();
        eventsystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }
    
     /// UPDATE
    /* Updates the Selection Manager */
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // Set up new pointer event at the mouse position
            pointer = new PointerEventData(eventsystem);
            pointer.position = Input.mousePosition;
    
            List<RaycastResult> results = new List<RaycastResult>(); //Create a list to store the results of the raycast
            raycaster.Raycast(pointer, results);                    //Raycast from our pointer and pass the results in the list we created
    
            foreach (RaycastResult result in results) { Debug.Log(result.gameObject.name); }
    
            if (results.Count > 0 && results[0].gameObject.tag == "UIItemSlot") { //Check first raycast target tagged as 'UIItemSlot'
                ProcessClick(results[0].gameObject.GetComponent<UIItemSlot>());  //Process the raycast when clicked for a UIItemSlot
            }
        }
    }
    
     /// PROCESS CLICK
    /* Process the click by comparing and swapping items */
    private void ProcessClick(UIItemSlot clicked) {
        if (clicked == null) { //Catch any NULL errors
            Debug.LogWarning("UI element tagged as UIItemSlot has no UIItemSlot component");
            return;
        }
    
        /// Compare slots with different items
        if (!ItemSlot.CompareSlots(cursor.itemSlot, clicked.itemSlot)) { //If compared items are DIFFERENT
            ItemSlot.SwapItems(cursor.itemSlot, clicked.itemSlot);      //Swap them over
            cursor.RefreshSlot();                                      //Refresh the slot
            return;
        }
    
        /// Compare slots with like items
        if (ItemSlot.CompareSlots(cursor.itemSlot, clicked.itemSlot)) { //If compared items are THE SAME
            if (!cursor.itemSlot.item.isStackable) { return; } //If the item is not stackable, we don't need to do anything
    
            if (clicked.itemSlot.itemAmount == clicked.itemSlot.item.itemMaxStack) { return; } //Return if item amount is a full stack
            int totalAmount = cursor.itemSlot.itemAmount + clicked.itemSlot.itemAmount;       //Get the total amouunt of the items combined
            int maxItemStack = cursor.itemSlot.item.itemMaxStack;                            //Cache max stack amount for convenience
    
            if (totalAmount <= maxItemStack) {              //Validate the total amount isn't a full stack
                clicked.itemSlot.itemAmount = totalAmount; //Combine them into one slot
                cursor.itemSlot.Clear();                  //Clear the slot the user was holding
            } 
            else {                                                         /* Otherwise, if over a full stack */
                clicked.itemSlot.itemAmount = maxItemStack;               //Fill the stack the user clicked on
                cursor.itemSlot.itemAmount = totalAmount - maxItemStack; //Return the remainder to the inventory
            }
            cursor.RefreshSlot();
        }
    }
}