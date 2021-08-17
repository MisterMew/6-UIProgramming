using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject {
    public string itemName;
    [TextArea] public string itemDescription;

    public Sprite itemSprite;
    public int itemStack;
    public bool isStackable { get { return (itemStack > 1); } } //Return true if item is stackable
}
