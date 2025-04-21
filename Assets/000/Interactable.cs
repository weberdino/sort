using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool hasInteracted = false;
    bool isFocus = false;

    Transform player;

    //public EquipmentSlot equipSlot;

    /*public virtual void OnInteraction()
    {
        //Debug.Log("interaction");//
    }*/

    public virtual void Interact (CharacterStats.atkart hitType, int modifier, GameObject obj)
    {
        //This method is meant to be overwritten
    }
    void Update ()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (interactionTransform == null)
                interactionTransform = transform;

            if (distance <= radius)
            {
                    Interact(CharacterStats.atkart.hit, 1, null);
                    hasInteracted = true;
            }                           
        }
     }

    public enum EquipmentSlot { Helmet, Armor, Weapon, Boots, Glove, Amulet, Ring }
}