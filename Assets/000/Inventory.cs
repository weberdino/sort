using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IDataPersistance
{

    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Equipment> items = new List<Equipment>();
    public List<Equipment> accessoires = new List<Equipment>();
    public List<Equipment> armors = new List<Equipment>();

    public Equipment reloadItem;

    public InventoryUI ui;
    //public InventoryUI ui2; keine ahnung warum ich ein 2. ui update haben wollte 

    private void Start()
    {
        //int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        //Debug.Log(numSlots);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();        
    }

    void Update()
    {
        //ui.UpdateUI();
        //ui2.UpdateUI();
    }

    public void LoadData(GameData data)
    {
        items = data.items;
        accessoires = data.accessoires;
        armors = data.armor;     

       // onItemChangedCallback.Invoke();
    }
    public void SaveData(ref GameData data)
    {
        data.items = items;
        data.armor = armors;
        data.accessoires = accessoires;

        Debug.Log("savedInventory");
    }

    public bool AddArmor(Equipment item)
    {
        if(!item.isDefaultItem)
        {
            if(armors.Count >= space)
            {
                return false;
            }
            armors.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        if(item != null)
        {
            item.Add();
        }
        return true;    
    }

    public bool AddJewellery(Equipment item)
    {
        if (!item.isDefaultItem)
        {
            if (accessoires.Count >= space)
            {
                return false;
            }
            accessoires.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        if (item != null)
        {
            item.Add();
        }
        return true;
    }

    public bool Add(Equipment item)
    {
        if (!item.isDefaultItem)
        {  
            if (items.Count >= space)
            {
                Debug.Log("not enough room.");
                return false;
            }
            items.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        if(item != null)
        {
            item.Add();
        }
        return true;
    }

    public void Remove(Equipment item)
    {
        Debug.Log("remo: " + item.name);
        items.Remove(item);
        armors.Remove(item);
        accessoires.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
