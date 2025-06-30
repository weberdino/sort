using UnityEngine;

public class ItemPickup : Interactable
{
    public Equipment item; //why not Item ?
    //public Item items;
    //public bool heal;
    PlayerStats myStats;
    public bool keyitem;

    public override void OnInteraction()   
    {
        base.OnInteraction();

        PickUp();
    }

    void PickUp ()
    {
        //EquipmentSlot test = test.Weapon;
        //int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        //string itemType = EquipmentSlot.Weapon.ToString();

        //Debug.Log(item.equipSlot.ToString() + " = " + equipSlot.ToString());

        if (keyitem == true)
        {
            GameVariables.keyCount += 1;
            Destroy(gameObject);
        }

        else
        {
            if(item.equipSlot.ToString() == "Weapon")
            {
                Debug.Log("Picking up" + item.name);
                bool wasPickedUp = Inventory.instance.Add(item);

                //wasPickedUp = Inventory.instance.Add(items);

                if (wasPickedUp)
                Destroy(gameObject);
            }
            else
            {
                if (item.equipSlot.ToString() == "Amulet" || item.equipSlot.ToString() == "Ring")
                {
                    bool wasPickedUp = Inventory.instance.AddJewellery(item);
                    if (wasPickedUp)
                        Destroy(gameObject);
                }
                else
                {
                    //Add item to Equip List
                    bool wasPickedUp = Inventory.instance.AddArmor(item);

                    if (wasPickedUp)
                        Destroy(gameObject);
                }
            }
        }

        //if(heal == true)
        {
            //myStats = GetComponent<CharacterStats>();
            //Healing();
            //Destroy(gameObject);
        }       
    }
}
