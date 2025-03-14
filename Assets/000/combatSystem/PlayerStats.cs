using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : CharacterStats
{
    public GameObject healVfx;

    public bool main;
    //CharacterStats hp;
    //ScoreManager scoreManager;
    private Player player;
    CharacterStats myStats;
    int damageStat;

    float cd;

    public UnityEvent OnBlock;

    private float attackCooldown;
    //public List<Nodes> nodes;
    public List<CastType> castTypes;
    public GameObject ability;
    public GameObject baseAbility;

    public Transform animParent;

    Equipment[] eM;

    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        //eM = EquipmentManager.instance.currentEquipment;


        //EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        player = GetComponent<Player>();
        myStats = GetComponent<CharacterStats>();
      
        /*scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.Score = PlayerPrefs.GetInt("Score", 0);
        scoreManager.Score2 = PlayerPrefs.GetInt("Score2", 0);
        scoreManager.Score3 = PlayerPrefs.GetInt("Score3", 0);
        scoreManager.Score4 = PlayerPrefs.GetInt("Score4", 0);
        scoreManager.Score5 = PlayerPrefs.GetInt("Score5", 0);
        scoreManager.Score6 = PlayerPrefs.GetInt("Score6", 0);
        scoreManager.Score7 = PlayerPrefs.GetInt("Score7", 0);
        scoreManager.Score8 = PlayerPrefs.GetInt("Score8", 0);*/


        //hp = GetComponent<CharacterStats>();
    }       

    private void Update()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            bool active1 = eM[i] != null;
            parent.transform.GetChild(i).gameObject.SetActive(active1);
        }

        cd -= Time.deltaTime;

        if(cd <= 0)
        {
            if (player.CanUseRage())
            {
                RageBuff(50);
                cd = 25;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Healing(10);
            Debug.Log("heal");
        }
        Debug.Log("noheal");

        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0)
        {
            StartCoroutine(Regenerate());
            attackCooldown = 1;
        }
        //Debug.Log(currentHealth);
        //Debug.Log(damage.GetValue());
        damageStat = damage.GetValue();
        damageStat += damage.GetValue() * damageMultiplier.GetValue() / 100;
        /*scoreManager.Score  = maxHealth.GetValue();
        scoreManager.Score2 = damage.GetValue();
        scoreManager.Score3 = fireDamage.GetValue();
        scoreManager.Score4 = mSpeed.GetValue();
        scoreManager.Score5 = critChance.GetValue();
        scoreManager.Score6 = critMultiplier.GetValue();
        scoreManager.Score7 = bleedChance.GetValue();
        scoreManager.Score8 = damageMultiplier.GetValue();*/

        //if (player.CanUseDamage)
        {
            //damageMultiplier.AddModifier();
        }

        GameVariables.Dex = Dex.GetValue();
        GameVariables.Str = Str.GetValue();
        GameVariables.Int = Int.GetValue();

        //damage

    }

    public void Assign()
    {
        ability = PlayerManager.instance.player.GetComponent<PlayerStats>().ability;
    }

    void regenCast()
    {
        var obj = Instantiate(ability);
        obj.transform.position = this.transform.position;
        obj.transform.rotation = this.transform.rotation;
    }

    public void Healing(int heal)
    {
        Heal(heal);
        Debug.Log(this.gameObject);
        var he = Instantiate(healVfx);
        he.transform.position = this.transform.position;
        Destroy(he, 1);
    }

    void RageBuff(int value)
    {
        mSpeed.AddModifier(value);
        StartCoroutine(BuffDecay(value));
    }

    IEnumerator BuffDecay(int value)
    {
        var wait = new WaitForSeconds(5f);
        yield return wait;

        mSpeed.RemoveModifier(value);
    }
        
    IEnumerator Regenerate()
    {
        yield return new WaitForSeconds(2);
        if (currentHealth < maxHealth.GetValue())
        {
            currentHealth += hpRegeneration.GetValue(); // do as coroutine, add regen cast
            PlayerStats ps = PlayerManager.instance.player.GetComponent<PlayerStats>(); 
            if(ps.castTypes.Contains(CastType.whileRegeneration))
             {
                //StartCoroutine damage
                //GameObject Ability = globalAbilities.instance.regen;

               /* var obj = Instantiate(Ability);
                obj.transform.position = this.transform.position;
                obj.transform.rotation = this.transform.rotation;*/
            }
        }
        if (currentHealth > maxHealth.GetValue())
        {
            currentHealth = maxHealth.GetValue();
        }
    }
    public void SetStrength()
    {
        damage.AddModifier(10);
        Debug.Log(damage.GetValue());
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        Debug.Log("psOnEqChange" + oldItem);
       //  int slotIndex = (int)newItem.equipSlot;
       /* if (slotIndex == 7)
        {
            maxHealth.AddModifier(newItem.maxHealthModifier);
            armor.AddModifier(newItem.armorModifier);
            fireResistance.AddModifier(newItem.fireResistanceModifier);
            lightningResistance.AddModifier(newItem.lightningResistanceModifier);

            damage.AddModifier(newItem.damageModifier);
            fireDamage.AddModifier(newItem.fireDamageModifier);
            lightningDamage.AddModifier(newItem.lightningDamageModifier);

            mSpeed.AddModifier(newItem.mSpeedModifier);
            multiHit.AddModifier(newItem.multiHitModifier);
            critChance.AddModifier(newItem.critChanceModifier);
            critMultiplier.AddModifier(newItem.critMultiplierModifier);
            bleedChance.AddModifier(newItem.bleedChanceModifier);
            attackSpeed.AddModifier(newItem.attackSpeedModifier);
            hpMultiplier.AddModifier(newItem.hpMultiplier);
        }
        else*/
        {
            if (newItem != null)
            {
                maxHealth.AddModifier(newItem.maxHealthModifier);
                armor.AddModifier(newItem.armorModifier);
                fireResistance.AddModifier(newItem.fireResistanceModifier);
                lightningResistance.AddModifier(newItem.lightningResistanceModifier);

                damage.AddModifier(newItem.damageModifier);
                fireDamage.AddModifier(newItem.fireDamageModifier);
                lightningDamage.AddModifier(newItem.lightningDamageModifier);

                mSpeed.AddModifier(newItem.mSpeedModifier);
                multiHit.AddModifier(newItem.multiHitModifier);
                critChance.AddModifier(newItem.critChanceModifier);
                critMultiplier.AddModifier(newItem.critMultiplierModifier);
                bleedChance.AddModifier(newItem.bleedChanceModifier);
                attackSpeed.AddModifier(newItem.attackSpeedModifier);
                hpMultiplier.AddModifier(newItem.hpMultiplier);
                hpRegeneration.AddModifier(newItem.regenerationMultiplier);

                projectileMod.AddModifier(newItem.addProjectiles);

                //castTypes.Add(newItem.cType);
                // ability = newItem.ability;
            }

            if (oldItem != null)
            {
                maxHealth.RemoveModifier(oldItem.maxHealthModifier);
                armor.RemoveModifier(oldItem.armorModifier);
                fireResistance.RemoveModifier(oldItem.fireResistanceModifier);
                lightningResistance.RemoveModifier(oldItem.lightningResistanceModifier);

                damage.RemoveModifier(oldItem.damageModifier);
                fireDamage.RemoveModifier(oldItem.fireDamageModifier);
                lightningDamage.RemoveModifier(oldItem.lightningDamageModifier);

                mSpeed.RemoveModifier(oldItem.mSpeedModifier);
                multiHit.RemoveModifier(oldItem.multiHitModifier);
                critChance.RemoveModifier(oldItem.critChanceModifier);
                critMultiplier.RemoveModifier(oldItem.critMultiplierModifier);
                bleedChance.RemoveModifier(oldItem.bleedChanceModifier);
                attackSpeed.RemoveModifier(oldItem.attackSpeedModifier);
                hpMultiplier.RemoveModifier(oldItem.hpMultiplier);
                hpRegeneration.RemoveModifier(oldItem.hpRegeneration);

                projectileMod.RemoveModifier(oldItem.addProjectiles);

                //castTypes.Remove(oldItem.cType);
                //ability = oldItem.ability;
            }

            /*if (newItem == oldItem) //overrides if item is the same / got the same values
            {
                maxHealth.AddModifier(newItem.maxHealthModifier);
                armor.AddModifier(newItem.armorModifier);
                fireResistance.AddModifier(newItem.fireResistanceModifier);
                lightningResistance.AddModifier(newItem.lightningResistanceModifier);

                damage.AddModifier(newItem.damageModifier);
                fireDamage.AddModifier(newItem.fireDamageModifier);
                lightningDamage.AddModifier(newItem.lightningDamageModifier);

                mSpeed.AddModifier(newItem.mSpeedModifier);
                multiHit.AddModifier(newItem.multiHitModifier);

                critChance.AddModifier(newItem.critChanceModifier);
                critMultiplier.AddModifier(newItem.critMultiplierModifier);
                bleedChance.AddModifier(newItem.bleedChanceModifier);

                attackSpeed.AddModifier(newItem.attackSpeedModifier);

                castTypes.Add(newItem.cType);
                ability = newItem.ability;  
            }*/
        }
        if (newItem != null)
        {
            getCastType(newItem.cType);
        }
        if (oldItem != null) { 

            removeOldCastType(oldItem.cType);
        }
    }

    void getCastType(CastType casttype)
    {
        if(casttype == null)
        {
            return;
        }
        //castTypes.Clear();
        castTypes.Add(casttype);
    }

    void removeOldCastType(CastType casttype)
    {
        if (casttype == null)
        {
            return;
        }
        castTypes.Remove(casttype);
    }
    public override void BreakShield()
    {
        block.RemoveModifier(100);
    }

    public override void Die()
    {
        if(main)
        {
            base.Die();
            PlayerManager.instance.KillPlayer();
            //PlayerManager.instance.KillSubPlayer();

            //currentHealth = maxHealth.GetValue() / 2;
        }
        else
        {
            PlayerManager.instance.KillSubPlayer();

            currentHealth = maxHealth.GetValue() / 4;
            //if(player.CanUseRevenge())
            //{
                //if(main)
                //{
                  //  Heal(50);
               // }
            //}
        }
    }
}
