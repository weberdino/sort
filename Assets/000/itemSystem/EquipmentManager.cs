using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour, IDataPersistance
{

    #region Singleton

    public Equipment[] defaultEquipment;

    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public Equipment[] currentEquipment; // Items we currently have equipped
    Equipment Secondary;
    public GameObject secondaryImage;
    MeshRenderer[] currentMeshes;
    public SkinnedMeshRenderer targetMesh;
    public Transform parent;
    //public Transform armor;

   // public UI_Skilltree uiTree;

    Item item;

    public bool Unique;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new MeshRenderer[numSlots];

        EquipDefaults();
        //LoadEquip();
    }

    public void LoadData(GameData data)
    {
        /*foreach(Equipment equip in data.savedEquipment)
        {
            if(equip.id != 0)
            {
                
                LoadEquip(equipList.items[equip.id]);
            }
        }*/
        defaultEquipment = data.savedEquipment;
        //currentEquipment = data.savedEquipment;
        EquipDefaults();
    }

    public void SaveData(ref GameData data)
    {
        data.savedEquipment = this.currentEquipment;
    }

    /*public void LoadEquip(Equipment newEquip)
    {
        Equip(newEquip);
    }*/

    public void weaponSwap()
    {
        //secondaryOn = !secondaryOn;

        int slotindex = 2;

        Equipment oldItem = currentEquipment[slotindex];
        currentEquipment[slotindex] = Secondary;
        onEquipmentChanged.Invoke(Secondary, oldItem);
        Secondary = oldItem;
        if(Secondary != null || currentEquipment[slotindex] != null ) {
            secondaryImage.SetActive(!secondaryImage.active);
        }
        
    }

    public void Equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        
        Equipment oldItem = Unequip(slotIndex);
        if(oldItem != null)
        {
            //RemoveAbility(oldItem);
        }
        Debug.Log("numb" + slotIndex + newItem + oldItem + onEquipmentChanged);
        //Equipment oldItem = UnequipArmor(slotIndex);

        //uiTree.eqUnlock(newItem.skill.skillType, newItem.skill.sprite); //UNIQUE SKILLS FROM ITEM !!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        /*if (parent.transform.childCount >= 1)                 //Mesh pos for Items !!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            Destroy(parent.transform.GetChild(0).gameObject);
        }*/

        currentEquipment[slotIndex] = newItem;

      
        //var child = parent.GetChild(0);               !!!!!!!!
        //Destroy(child);
        //!!!!! 
       
        //MeshRenderer newMesh = Instantiate<MeshRenderer>(newItem.mesh, parent);

        //newMesh.transform.parent = targetMesh.transform;       
    }

    public void EquipArmor(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Debug.Log("numb" + slotIndex);
        UnequipArmor(slotIndex);
        Equipment oldItem = UnequipArmor(slotIndex);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void EquipJewellery(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;        
        Debug.Log("numb" + slotIndex);
        UnequipJewellery(slotIndex);
        Equipment oldItem = UnequipJewellery(slotIndex);       

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void AddNode(Equipment newItem)
    {


    }

    /*public void RemoveAbility(Equipment oldItem)
    {
        switch (oldItem.cType)
        {
            case CastType.onCrit: globalAbilities.instance.crit = null; break;
            case CastType.mana: globalAbilities.instance.crit = null; break;
            case CastType.whileRegeneration: globalAbilities.instance.regen = null; break;
            case CastType.onWalk: globalAbilities.instance.walk = null; break;

            case CastType.auto: globalAbilities.instance.auto = null; break;

            case CastType.onKill: globalAbilities.instance.kill = null; break;
        }      
    }*/

    public Equipment Unequip(int slotIndex)
    {
        Equipment oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            //uiTree.RemoveSkillFromList(oldItem.skill);            //UI SKILL.. !!!!!!!!!!!!!!!!!!!!!!!!
            //uiTree.RemoveSkillIcon(oldItem.skill.sprite);

            /*if (oldItem.skill != null)
            {
                uiTree.RemoveSkillFromItemList(oldItem.skill);
                Debug.Log("skilltesten");
            }*/
            //inventory.AddArmor(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
        return oldItem;
    }

    public Equipment UnequipArmor(int slotIndex)
    {
        Equipment oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddArmor(oldItem);

            currentEquipment[slotIndex] = null;

            if(onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
        return oldItem;
    }

    public Equipment UnequipJewellery(int slotIndex)
    {
        Equipment oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddJewellery(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
        return oldItem;
    }

    void EquipDefaults()
    {
        foreach (Equipment e in defaultEquipment)
        {
            if (e != null)
            {
                Equip(e);
                e.Use();
                Debug.Log("equip: " + e);
            }
        }
    }

    public void UnequipAll ()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {            
            if(i == 2)
            {
                Unequip(i);
            }
            if(i >= 4)
            {
                UnequipJewellery(i);
            }
            else
            {
                UnequipArmor(i);
            }          
        }
    }

    void Update () 
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            weaponSwap();
        }

        //useless right now !!!!!!!!!!!!!!!!
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();

        /*for (int i = 0; i < currentEquipment.Length; i++)
        {
           // Debug.Log("eqlol" + currentEquipment[i].unique);
            if (currentEquipment[i].unique)
            {
                Debug.Log("eqlol1" + currentEquipment[i].skill);
                //Unique = true;
                //uiTree.skills = currentEquipment[i].skill;

                //uiTree.eqUnlock(currentEquipment[i].skill.skillType, currentEquipment[i].skill.sprite);

                //Debug.Log("skill" + currentEquipment[i].skill);
                break;
            }
            else
            {
                Unique = false;
            }*/
        //}

        /*uiTree.skills = currentEquipment[2].skill;
        if (uiTree.skills != null)
        {
            uiTree.SpecialItemSkill();
        }*/
    }
}


