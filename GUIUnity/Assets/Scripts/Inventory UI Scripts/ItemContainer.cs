using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour {
    /// Variables
    public static bool inventoryIsOpen = false;

    [Header("Container UI Elements")]
    public GameObject parentUI;
    public Text title;
    public Transform contentWindow;  //GridLayoutWindow used to display our UIItemSlots.

    [Header("Container Details")]
    public string containerName;
    public GameObject SlotPrefab; //UIItemSlots prefab

    List<ItemSlot> items = new List<ItemSlot>();
    List<UIItemSlot> UISlots = new List<UIItemSlot>();

    private void Awake() {
        Item[] tempItems = new Item[10];
        tempItems[0] = Resources.Load<Item>("Items/crystal_cyan");
        tempItems[1] = Resources.Load<Item>("Items/crystal_brown");
        tempItems[2] = Resources.Load<Item>("Items/crystal_indigo");
        tempItems[3] = Resources.Load<Item>("Items/crystal_orange");
        tempItems[4] = Resources.Load<Item>("Items/crystal_purple");

        tempItems[5] = Resources.Load<Item>("Items/shard_cyan");
        tempItems[6] = Resources.Load<Item>("Items/shard_brown");
        tempItems[7] = Resources.Load<Item>("Items/shard_indigo");
        tempItems[8] = Resources.Load<Item>("Items/shard_orange");
        tempItems[9] = Resources.Load<Item>("Items/shard_purple");
    
        for (int i = 0; i < 64; i++) {
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
        title.text = containerName.ToUpper();  //Set the name of the container

        // Loop through each item in the given items list and instantiate a new UIItemSlot prefab for each one.
        for (int i = 0; i < slots.Count; i++) {
            GameObject newSlota = Instantiate(SlotPrefab, contentWindow);  //Make sure our GridLayoutWindow is set as the parent of the new UIItemSlot object.

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