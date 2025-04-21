using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    //public List<Globals.CurseType> curse;
    public List<Interactable> interactables;
    public CharacterStats.atkart atkart;
    public Globals.CurseType[] curseType;
   // public float radius;
    public bool isDirty;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Interactable>() != null)
        {
            Interactable eStats = other.GetComponent<Interactable>();
            interactables.Add(eStats);
        }
    }

    void castCollider()
    {
        for (int i = 0; i < interactables.Count; i++)
        {
            interactables[i].Interact(atkart, 1, gameObject);
        }
    }

    private void OnEnable()
    {
        cast();
    }

    void cast()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 6);
        foreach (Collider nearbyObject in colliders)
        {
            foreach (Globals.CurseType cType in curseType)
            {
                EnemyStats eStats = nearbyObject.GetComponent<EnemyStats>();
                if (eStats != null) { eStats.curses.Add(cType); }               
            }

            Interactable dest = nearbyObject.GetComponent<Interactable>();
            if (dest != null)
            {
                dest.Interact(atkart, 1, gameObject); //((int)pStats.modifier
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 6);
    }
}
