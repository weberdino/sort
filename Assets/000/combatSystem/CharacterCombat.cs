using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    //public float attackSpeed = 1f;
    //private float attackCooldown = 0f;

    public bool InCombat { get; private set; }
    public event System.Action OnAttack;

    private float attackCooldown;

    const float combatCooldown = 5;
    float lastAttackTime;
    //new Animator animation;
    GameObject stabButton;

    CharacterStats myStats;
    PlayerStats pStats;
    //OnDeathSpawner atk;

    enum atkart
    {
        hit,
        spell,
        projectile
    }

    void Start ()
    {
        myStats = GetComponent<CharacterStats>();
        pStats = GetComponent<PlayerStats>();
        //animation = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }

        attackCooldown -= Time.deltaTime * 10;
    }

    public void Heal (CharacterStats targetstats)
    {
        DoHeal(targetstats);
    }

    void DoHeal(CharacterStats stats)
    {
        stats.Heal(10);
    }

    public void onHit(CharacterStats stats, CharacterStats.atkart hitCheck)
    {
        stats.TakeDamage(0, 0, 0, 0, 0, 0, 0, hitCheck);
    }
    public void Attack (CharacterStats targetstats, CharacterStats.atkart hitCheck, int modifier)
    {
        //StartCoroutine(DoDamage(targetstats));

        //stabButton = GameObject.Find("STABButton");        
        if (attackCooldown <= 0)
        {
            Debug.Log("attack" + targetstats.damage);
            DoDamage(targetstats, hitCheck, modifier);
            if (OnAttack != null)
                OnAttack();
        }

        InCombat = true;
        lastAttackTime = Time.time;
    }

    //IEnumerator DoDamage (CharacterStats stats)
    void DoDamage(CharacterStats stats, CharacterStats.atkart hitCheck, int modifier )
    {
        if(modifier == 0)
        {
            modifier = 1;//+ ((int)stats.modifier);
        }
        // modifier = ((int)stats.modifier);
        // yield return new WaitForSeconds(myStats.attackSpeed.GetValue());

        stats.TakeDamage(myStats.damage.GetValue() * modifier , myStats.critChance.GetValue(), myStats.critMultiplier.GetValue(), myStats.bleedChance.GetValue(), myStats.damageMultiplier.GetValue(), myStats.exeDamage.GetValue(), myStats.hitrate.GetValue(), hitCheck );
        if (myStats.currentHealth < myStats.maxHealth.GetValue())
        {
            myStats.LifeSteal(myStats.lifeSteal.GetValue(), myStats.damage.GetValue());
            Debug.Log("noBlock");
        }
        if (stats.currentHealth <= 0)
        {
            InCombat = false;
        }
        if (stats.block.GetValue() >= 10) // always reflect, shouldnt stay like this -> should be block
        {
            myStats.Reflect(myStats.damage.GetValue());
            Debug.Log("block");
        }
    }

    public void FireAttack(CharacterStats targetstats)
    {
        //StartCoroutine(DoDamage(targetstats));

        //stabButton = GameObject.Find("STABButton");        
        if (attackCooldown <= 0)
        {
            DoFireDamage(targetstats);
        }

        InCombat = true;
        lastAttackTime = Time.time;
    }

    public void DoFireDamage (CharacterStats targetstats)
    {
        targetstats.TakeFireDamage(myStats.fireDamage.GetValue(), myStats.critChance.GetValue());
    }

    public void LightningAttack(CharacterStats targetstats)
    {
        if (attackCooldown <= 0)
        {
            DoLightningDamage(targetstats);
        }

        InCombat = true;
        lastAttackTime = Time.time;
    }

    public void DoLightningDamage(CharacterStats targetstats)
    {
        targetstats.TakeLightningDamage(myStats.lightningDamage.GetValue());
    }
}
