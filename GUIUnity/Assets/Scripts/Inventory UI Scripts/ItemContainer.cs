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
    public Transform contentWindow; //GridLayoutWindow used to display our UIItemSlots.

    [Header("Container Details")]
    public string containerName;
    public GameObject SlotPrefab; //UIItemSlots prefab

    List<ItemSlot> items = new List<ItemSlot>();

    private void Awake() {
        //SlotPrefab = Resources.Load<GameObject>("Prefabs/UIItemSlot");

        #region Demonstration Code
    
        Item[] tempItems = new Item[6];
        tempItems[0] = Resources.Load<Item>("Items/strange_skull");
        tempItems[1] = Resources.Load<Item>("Items/sahred_indigo");
        tempItems[2] = Resources.Load<Item>("Items/shard_orange");
        tempItems[3] = Resources.Load<Item>("Items/ribbon");
        tempItems[4] = Resources.Load<Item>("Items/bone");
        tempItems[5] = Resources.Load<Item>("Items/wand");
    
    
        for (int i = 0; i < 48; i++) {
            int index = Random.Range(0, tempItems.Length);
            int amount = Random.Range(1, tempItems[index].itemMaxStack);
            int condition = tempItems[index].itemMaxDurability;
    
            items.Add(new ItemSlot(tempItems[index].name, amount, condition));
            
        }
        #endregion
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.P))
            CloseContainer();
    
        if (Input.GetKeyDown(KeyCode.I))
            OpenContainer(items);
    }
    
    List<UIItemSlot> UISlots = new List<UIItemSlot>();

    public void OpenContainer(List<ItemSlot> slots) {
        Debug.Log("Opened Inventory");

        parentUI.SetActive(true);
        title.text = containerName.ToUpper(); //Set the name of the container
    
        // Loop through each item in the given items list and instantiate a new UIItemSlot prefab for each one.
        for (int i = 0; i < slots.Count; i++) {
            GameObject newSlot = Instantiate(SlotPrefab, contentWindow); //Make sure our GridLayoutWindow is set as the parent of the new UIItemSlot object.
            
            newSlot.name = i.ToString();                      //Name the new slot with its index in the list so we have a way of identifying it.
            UISlots.Add(newSlot.GetComponent<UIItemSlot>()); //Add the new slot to our UISlots list so we can find it later.
            slots[i].AttachUI(UISlots[i]);                  //Attach the UIItemSlot to the ItemSlot it corresponds to.
        }
    }
    
    public void CloseContainer() {
        Debug.Log("Closed Inventory");

        foreach (UIItemSlot slot in UISlots) { //For each slot in the UI
            if (slot.itemSlot != null) {      //If the slot is not NULL
                slot.itemSlot.DetachUI();    //Detach it from the UI
            }
            Destroy(slot.gameObject); //Delete the gameobject
        }
        UISlots.Clear();            //Clear the UISlots list
        parentUI.SetActive(false); //Deactivate/close the window
    }
}