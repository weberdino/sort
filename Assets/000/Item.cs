using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(PlayerStats))]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    //public bool Heal = false;

    public string Description;

    Transform target;
    CharacterCombat combat;

    void Start()
    {
        //combat = GetComponent<CharacterCombat>();
        target = PlayerManager.instance.player.transform;
    }


    public virtual void Use()
    {
       // RemoveFromInventory();
        Debug.Log("using " + name);

        Healing();
    }

    public virtual void Add()
    {

    }
    /*public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }*/

    public void Healing()
    {
        //PlayerStats targetStats = target.GetComponent<PlayerStats>();

        //if (targetStats != null)
        {
            ///combat.Heal(targetStats);
        }
    } 
}
