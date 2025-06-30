using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    
    Inventory inventory;

    InventorySlot[] slots;

    public bool startUpdate;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        if (startUpdate == true)
        {
            inventory.onItemChangedCallback += UpdateUI;
        }

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }


    // Update is called once per frame
    void Button()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    public void UpdateUI()
    {
        
        // Loop through all the slots
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)  // If there is an item to add
            {
                slots[i].AddItem(inventory.items[i]);   // Add it
            }
            else
            {
                // Otherwise clear the slot
                slots[i].ClearSlot();
                //Debug.Log("remo" + inventory.items[i].name.ToString());
            }
        }
    }
}


