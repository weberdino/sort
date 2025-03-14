using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;
    // Start is called before the first frame update
    /*protected override void Start()
    {
        base.Start();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        foreach (WeaponAnimations a in weaponAnimations)
        {
            weaponAnimationsDict.Add(a.weapon, a.clips);
        }
    }

    // Update is called once per frame
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 1);
            if (weaponAnimationsDict.ContainsKey(newItem))
            {
                currentAttackAnimSet = weaponAnimationsDict[newItem];
            }
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 0);
        }

        if (newItem != null && newItem.equipSlot == EquipmentSlot
    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}*/
