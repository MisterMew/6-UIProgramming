using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]

public class Item : ScriptableObject {
    /// Variables
    public string itemName;
    public Sprite itemSprite;

    [TextArea] //Space for an item description
     public string itemDescription;

    public int itemMaxStack;
    public bool isStackable { get { return (itemMaxStack > 1); } } //Return true if item is stackable

    public int itemMaxDurability;
    public bool isDegradable { get { return (itemMaxDurability > 1); } } //Return true if item is degradable
}
