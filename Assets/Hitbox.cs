using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    //public List<Globals.CurseType> curse;
    public List<EnemyStats> enemyStats;
    public Globals.CurseType[] curseType;
    public float radius;

    private void OnTriggerEnter(Collider other)
    {
        EnemyStats eStats = other.GetComponent<EnemyStats>();
        enemyStats.Add(eStats);
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
                dest.Interact(CharacterStats.atkart.hit, 1); //((int)pStats.modifier
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 6);
    }
}
