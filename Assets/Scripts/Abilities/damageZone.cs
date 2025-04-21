using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageZone : CharacterStats
{
    CharacterCombat combat;
    Transform target;
    float attackCooldown;
    float cd;

    public bool forEnemy;
    public CharacterStats.atkart atkarts;

    bool outside;
    PlayerStats pStats;

    public List<Enemy> es;

    private void Start()
    {
               
    }

    void Update()
    {
        pStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        cd -= Time.deltaTime;
        attackCooldown -= Time.deltaTime;
        combat = GetComponent<CharacterCombat>();

        if(cd < 0)
        {          
            for (int i = 0; i < es.Count; i++)
            {              
                if (es[i] != null)
                {
                    es[i].Interact( atkarts, 1,gameObject);
                    //combat.Attack(es[i], atkarts, 1);
                    cd = .5f;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (forEnemy)
        {
            es.Add(other.GetComponent<Enemy>());          
        }
    }

    // Dont forget CharacterCombat Component on MonoBehaviour
    private void OnTriggerStay(Collider other)
    {
        /*GameObject play = other.gameObject;

        pStats = play.GetComponent<PlayerStats>();
        Player player = PlayerManager.instance.player.GetComponent<Player>();
        
        //Debug.Log("targ: " + targetStats);
        Debug.Log("targ: " + combat);
        if (attackCooldown <= 0f)
        {
            if(forEnemy)
            {
                EnemyStats targetStats = play.GetComponent<EnemyStats>();
                combat.Attack(targetStats, atkarts);
                if (player.CanCountAsProjectile())
                {
                    combat.onHit(targetStats, CharacterStats.atkart.projectile);
                    Debug.Log("lol1234");
                }
                attackCooldown = .5f; // attackSpeed;
                                      //anim.Play("attack");
            }
            else
            {
                combat.Attack(pStats, CharacterStats.atkart.spell);
                attackCooldown = .5f;
            }
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        // outside = true;
        es.Remove(other.GetComponent<Enemy>());
    }
}
