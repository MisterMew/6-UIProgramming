using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour {
    /// Variables
    public static bool inventoryIsOpen = false;

    [Header("Container UI Elements")]
    public GameObject parentUI;
    public Transform inventoryWindow;  //GridLayoutWindow used to display our UIItemSlots.

    [Header("Container Details")]
    public GameObject SlotPrefab; //UIItemSlots prefab

    List<ItemSlot> items = new List<ItemSlot>();
    List<UIItemSlot> UISlots = new List<UIItemSlot>();

    private void Awake() {
        Item[] tempItems = new Item[4];

        tempItems[0] = Resources.Load<Item>("Items/strange_skull");
        tempItems[1] = Resources.Load<Item>("Items/pickaxe");
        tempItems[2] = Resources.Load<Item>("Items/shovel");
        tempItems[3] = Resources.Load<Item>("Items/wand");
    
        for (int i = 0; i < 40; i++) {
            int index = Random.Range(0, tempItems.Length);
            int amount = Random.Range(1, tempItems[index].itemMaxStack);
            int durability = Random.Range(1, tempItems[index].itemMaxDurability);
    
            items.Add(new ItemSlot(tempItems[index].name, amount, durability));
        }
        
    }

    private void Start() {
        InstantiateSlots(items);
    }

    void InstantiateSlots(List<ItemSlot> slots) {
        // Loop through each item in the given items list and instantiate a new UIItemSlot prefab for each one.
        for (int i = 0; i < slots.Count; i++) {
            GameObject newSlota = Instantiate(SlotPrefab, inventoryWindow);  //Make sure our GridLayoutWindow is set as the parent of the new UIItemSlot object.

            newSlota.name = i.ToString();                       //Name the new slot with its index in the list so we have a way of identifying it.
            UISlots.Add(newSlota.GetComponent<UIItemSlot>()); //Add the new slot to our UISlots list so we can find it later.
            slots[i].AttachUI(UISlots[i]);                  //Attach the UIItemSlot to the ItemSlot it corresponds to.
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (PauseMenu.gameIsPaused) { return; }

            if (!inventoryIsOpen) {
                OpenContainer(items);
            } else {
                CloseContainer();
            }
        }
    }


    public void OpenContainer(List<ItemSlot> slots) {
        Debug.Log("Opened Inventory");

        parentUI.SetActive(true);

        inventoryIsOpen = true;
    }
    
    public void CloseContainer() {
        Debug.Log("Closed Inventory");

        parentUI.SetActive(false);  //Deactivate/close the window

        inventoryIsOpen = false;
    }

    private void OnApplicationQuit() {
        foreach (UIItemSlot slot in UISlots) { //For each slot in the UI
            if (slot.itemSlot != null) {      //If the slot is not NULL
                slot.itemSlot.DetachUI();    //Detach it from the UI
            }
            Destroy(slot.gameObject); //Delete the gameobject
        }
        UISlots.Clear();            //Clear the UISlots list
    }
}