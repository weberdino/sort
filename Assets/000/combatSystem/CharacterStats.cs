using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private LevelSystem levelSystem;
    /*private Vector3 center;
    private Vector3 halfExtents;

    public float radius;*/

    //public int maxHealth;
    private Player player;
    public Stat maxHealth;
    public float currentHealth { get;  set; }
    public float modifier;

    [Header("BaseStat")]
    //public Stat hp;
    public Stat damage; //+ modifier / 100;
    public Stat fireDamage;
    public Stat lightningDamage;

    public Stat GlobalCD; 

    public Stat armor;
    public Stat fireResistance;
    public Stat lightningResistance;

    public Stat mSpeed;
    public Stat mSpeedPerc;
    public Stat multiHit;
    public Stat critChance;
    public Stat critMultiplier;

    public Stat bleedChance;
    public Stat attackSpeed;
    public Stat hpRegeneration;
    public Stat lifeSteal;    
    public Stat exeDamage;

    public Stat hitrate;
    public Stat dodge;
    public Stat block;

    public Stat projectileMod;
    //public Stat areaMod;
    public Stat cooldownMod;
    public Stat minionMod;

    public Stat Str;
    public Stat Dex;
    public Stat Int;
    [Header("Modifier")]
    public Stat damageMultiplier;
    public Stat amplifier; // mod to increase lightning damage, periodically

    //public Stat arrowCount;
    public Stat aoeMod;
    public Stat cooldownReduce;

    public Stat hpMultiplier;
    public Stat armorMultiplier;
    [Header("Debuffs/Buffs")]
    public bool Pierce;
    public bool Chain;
    public bool Stun;

    [Header("Curse")]
    public bool Blind;
    public bool Poisoned;

    public bool Weakness;
    public bool heat;
    public bool freeze;
    public bool stagger;
    public bool conduction;
    public bool Dynamit;

    float coold;

    public Stat CurseEfficency;
    //instantiate inst;
    public GameObject gunFireobj;


    //func var
    int avoid;

    public void rico()
    {
        Chain = true;
    }

    public void pois()
    {
        bleedChance.AddModifier(55);
    }

    public enum atkart
    {
        none,
        hit,
        spell,
        projectile,
    }

    // bool critted = false;
    //public Stat waveCountMulti; // always 0, just for waveMonster, didnt worked

    //public int heal = 10;

    public event System.Action<int, float> OnHealthChanged;

    public void Awake()
    {
       //maxHealth += waveCountMulti * GameVariables.waveCount * 10;
       //if(canuse)
       currentHealth = maxHealth.GetValue();
       currentHealth += maxHealth.GetValue() * hpMultiplier.GetValue();
    }

    private void Start()
    {
        //inst = PlayerManager.instance.player.GetComponent<instantiate>();
    }

    public void Update()
    {

        if (Poisoned)
        {
            poison();
        }
        //atkart.spell == 0;

        /*coold -= Time.deltaTime;

        if(coold <= 0)
        {
            currentHealth += hpRegeneration.GetValue();
            coold = .7f;
        }*/

        //if (Input.GetKeyDown(KeyCode.T))
        {
            //Heal(10);
        }
       
    }

    void poison()
    {
        Debug.Log("bleed00");
        StartCoroutine(KothTimer2(10, 10));
        
    }

    IEnumerator KothTimer2(int damage, int critMultiplier)
    {
        Debug.Log("bleed01");
        var wait = new WaitForSeconds(.3f);
        for (int i = 0; i < 10; i++)
        {
            currentHealth -= (damage * critMultiplier) / 10;
            Debug.Log("bleed " + damage * critMultiplier / 10);

            yield return wait;
        }
    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        /*if (maxHealth.GetValue() > currentHealth)
        {
            currentHealth += heal;
        }*/
    }

    public virtual void explosion(EnemyStats damageable) => damageable.damageOnDeath(damage.GetValue());

    public virtual void Hit(EnemyStats damageable) => damageable.Hit(damage.GetValue());

    //public virtual void critHit(EnemyStats damageable) => damageable.Crit(damage.GetValue());

    //public virtuel void onBlock(PlayerStats blockable) => blockable.Block( ());

    public void LifeSteal(int lifeSteal, int dmg)
    {

        float perc = (float)lifeSteal / 100;
        currentHealth += dmg * perc;
        Debug.Log("perc"+perc+lifeSteal);
    }

    public void Reflect(int damage)
    {
        currentHealth -= damage;
    }

    public void TakeDamage(int damage, int critChance, int critMultiplier, int bleedChance, int damageMultiplier, int exeDamage, int hitrate, atkart checkHit)
    {
        damage += damage * damageMultiplier / 100;
        if (currentHealth <= currentHealth / 4)
        {
            damage += damage * exeDamage;
        }

        if(Blind )    //make bool setup from estats in cstats
        {
            avoid -= 10 + CurseEfficency.GetValue() ;
            Debug.Log("hitr blind");
        }
       // if(conduction)
       // {
            // lightningDamage.GetValue() += 20;
        //}

        Debug.Log(hitrate + " " + avoid + "hitr");

        float randValue = Random.Range(1, 100);

        if (gameObject != null)
        {
            int hitC = Random.Range(1, hitrate);
            avoid = dodge.GetValue();
            if(avoid > 80)
            {
                avoid = 80;
            }

            if(hitrate >= avoid)
            {
                

                if (block.GetValue() <= randValue)
                {
                    if (randValue <= critChance)
                    {
                        Debug.Log("Crit");
                        critHit();
                        //critted = true;

                        //Debug.Log(randValue);
                        damage -= armor.GetValue();
                        damage = Mathf.Clamp(damage, 0, int.MaxValue);

                        currentHealth -= damage * critMultiplier;
                        Debug.Log(transform.name + " takes " + damage * critMultiplier + " damage.");
                        Debug.Log("take" + damage + " " + critMultiplier);

                        if (checkHit == atkart.spell)
                        {
                            Hit(); //explosion funciton/method , remove with Spell();
                        }
                        if(checkHit == atkart.projectile)
                        {
                            ProjectileHit();
                        }
                        if(checkHit == atkart.hit)
                        {
                           Hit();
                        }

                        if (randValue <= bleedChance)
                        {
                            StartCoroutine(KothTimer1());

                            IEnumerator KothTimer1()
                            {
                                var wait = new WaitForSeconds(1);
                                for (int i = 0; i < 10; i++)
                                {
                                    currentHealth -= (damage * critMultiplier) / 10;
                                    Debug.Log("bleed " + damage * critMultiplier / 10);

                                    yield return wait;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (checkHit == atkart.spell)
                        {
                                Hit(); // explosion funciton/method 
                        }
                        if(checkHit == atkart.projectile)
                        {
                                ProjectileHit();
                        }
                        if(checkHit == atkart.hit)
                        {
                                Hit();
                        }

                        Debug.Log("noCrit");
                        damage -= armor.GetValue();
                        damage = Mathf.Clamp(damage, 0, int.MaxValue);

                        currentHealth -= damage;
                        Debug.Log("damagetest: " + transform.name + " takes " + damage + " damage.");

                        if (randValue <= bleedChance)
                        {
                            StartCoroutine(KothTimer());

                            IEnumerator KothTimer()
                            {
                                var wait = new WaitForSeconds(1);
                                for (int i = 0; i < 10; i++)
                                {
                                        if(damage / 10 < 1)
                                        {
                                            damage = 10;
                                        }
                                    currentHealth -= damage / 10;
                                    Debug.Log("bleed " + damage / 10);                                

                                    yield return wait;
                                }
                            }
                        }
                    }
                    if (OnHealthChanged != null)
                    {
                        OnHealthChanged(maxHealth.GetValue(), currentHealth);
                    }
                    if (currentHealth <= 0)
                    {
                        Die();
                        //damageOnDeath();
                    }
                }
                else
                {
                    Debug.Log("blocked");
                        //Blocked();
                      //  BreakShield();
                }
            }
            else
            {
                Debug.Log("dodge");
                //onAvoid();
                onAvoid();
            }
        }       
    }

    public void TakeFireDamage(float fireDamage, int critChance)
    {
        float randValue = Random.Range(1, 100);

        if (gameObject != null)
        {
            float resPerc = fireResistance.GetValue() / 100;
            float fres = fireDamage * resPerc;
            fireDamage -= fres;
            if (randValue >= critChance)
            {
                currentHealth -= fireDamage;
                StartCoroutine(dotDamage());

                IEnumerator dotDamage()
                {
                    var wait = new WaitForSeconds(1);
                    for (int i = 0; i < 10; i++)
                    {
                        currentHealth -= fireDamage / 10;

                        yield return wait;
                    }
                }
            }
            else
            {
                currentHealth -= fireDamage;
            }         
        }
    }

    public void TakeLightningDamage(float lightningDamage)
    {
        if(gameObject != null)
        {
            float resPerc = lightningResistance.GetValue() / 100;
            float lres = lightningDamage * resPerc;
            
            lightningDamage -= lres;

            currentHealth -= lightningDamage;
            //amplify();
        }
    }  
    
    void amplify()
    {
        //Debug.Log("fgh: " + lightningDamage.GetValue()+ " " + amplifier.GetValue());
        //lightningDamage.AddModifier(amplifier.GetValue());
    }

    public virtual void Hit()
    {
        Debug.Log(transform.name + "hit");
    }

    public virtual void ProjectileHit()
    {

    }

    public virtual void critHit()
    {

    }

    /*public virtual void Blocked()
    {
        Debug.Log(transform.name + "hit");
    }*/
    
    public virtual void BreakShield()
    {

    }

    public virtual void Die()
    {
        //Die in some way
        //ment to be overwritten
        Debug.Log(transform.name + "died.");
    }

    //private bool _blind;
    public bool _Blind
    {
        get => Blind;
        set
        {
            if (Blind != value)
            {
                Blind = value;
                // Directly call a method to handle the blindness change.
                // This needs to be implemented in a way that it's accessible from here.
                OnBlindnessChanged(Blind);
            }
        }
    }

    protected virtual void OnBlindnessChanged(bool isBlind)
    {
        Debug.Log("bool");
      //  blindStrike bs = PlayerManager.instance.player.GetComponentInChildren<blindStrike>();
        //bs.Cast(this.transform);

        Invoke("blindreset", .5f);
    }

    protected virtual void OnDodge(bool isDodge)
    {
        // bulletDance bd = GetComponent<bulletDance>(); //like this never works, get child comp from player ....
        //bd.onDodge();
    }

    void blindreset()
    {
        Blind = false;
    }

   void onAvoid()
   {
        //bulletDance bd = GetComponent<bulletDance>(); //like this never works, get child comp from player ....
        //bd.onDodge();

        if (gunFireobj != null)
        {
            Debug.Log("gunfire");

           // inst.Button(gunFireobj);
        }    
   }
}
