using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour {
    /// Variables
    [Header("Item Details Printed")]
    [SerializeField] Text itemName;
    [SerializeField] Text itemDurability;
    [SerializeField] Text itemDescription;


    private void Update() {
        /* For the item the user is holding (CursorBounds)
         * Get the items name
         * Get the items current durability and max durability
         * Get the items description
         */
    }

    public void SetName(string name) { itemName.text = name.ToUpper(); }
}
