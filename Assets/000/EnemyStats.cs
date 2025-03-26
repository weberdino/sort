using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


public class EnemyStats : CharacterStats
{
    public Stat monsterLevel;

    private Vector3 position;
    public UnityEvent OnDie;
    public UnityEvent OnHit;
    public UnityEvent OnProjectileHit;
    public UnityEvent OnCrit;

    public PlayerStats playerStats;
    public Player player; //private

    public bool TestDummy;

    public GameObject Dynamite;
    public EnemyStatsHandler esh;

    private LevelSystem levelSystem;

    public LevelWindow levelWindow;
    float coold;
    float hpReg;

    float stuncd;

    public int charge;

    int ran;
    public List<Globals.CurseType> curses;

    public void GetStatHandler()
    {
        esh.SetStat(this);
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        //Set the LevelSystem object
        this.levelSystem = levelSystem;
    }

    void Start()
    {
       // esh = PlayerManager.instance.esh;
        //esh.SetStat(this);

       // levelWindow = GameObject.Find("CanvasUI").GetComponentInChildren<LevelWindow>();

       // player = PlayerManager.instance.player.GetComponent<Player>(); //GameObject.Find("Player").GetComponent<Player>();
       // playerStats = player.GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Poisoned)
        {
            poison();
        }

        ran = UnityEngine.Random.Range(1, 3);

        coold -= Time.deltaTime;
        
        if(Stun)
        {
            NavMeshAgent ag = this.GetComponent<NavMeshAgent>();
            ag.enabled = false;

            StartCoroutine(removeStun());
        }
        else
        {
            NavMeshAgent ag = this.GetComponent<NavMeshAgent>();
            ag.enabled = true;
        }

        if (coold <= 0)
        {
            if (currentHealth <= maxHealth.GetValue())
            {
                currentHealth += hpReg;
                coold = .7f;
            }
        }

        if(TestDummy)
        {
            float diff = Mathf.Max(currentHealth, maxHealth.GetValue()) - Mathf.Min(maxHealth.GetValue(), currentHealth);

            hpReg = diff;
        }
    }

    void poison()
    {
        StartCoroutine(KothTimer2(100, 1));
        Poisoned = false;
    }

    IEnumerator KothTimer2(int damage, int critMultiplier)
    {
        var wait = new WaitForSeconds(1);
        for (int i = 0; i < 10; i++)
        {
            currentHealth -= (damage * critMultiplier) / 10;
            Debug.Log("bleed " + damage * critMultiplier / 10);

            yield return wait;
        }
    }

    IEnumerator removeStun()
    {
        var wait = new WaitForSeconds(.8f);

        yield return wait;
        Stun = false;
    }

    public override void Hit()
    {                
        base.Hit();

        if (this.gameObject != null)
        {
           // ec.AddUp(this.gameObject);
        }
    }

    public override void ProjectileHit()
    {
        base.ProjectileHit();
        OnProjectileHit.Invoke();
    }

    public override void critHit()
    {
        base.critHit();
        OnCrit.Invoke();

        Debug.Log("crittedOnE");    
    }

    public override void Die()
    {
        if (TestDummy == false)
        {
            base.Die();
            OnDie.Invoke();


            int mlevel = monsterLevel.GetValue();
            //mlevel -= GameVariables.Level;

            if(mlevel == 0)
            {
                mlevel = 1;
            }
            if(levelWindow != null)
            {
                levelWindow.xp(mlevel);
                Debug.Log("xpStat: " + mlevel);
            }

            Debug.Log("dieInfo");

            HealthUI hui = GetComponentInChildren<HealthUI>();
            hui.DestroyUI();

            Destroy(this.gameObject); //-> healthUI  ?? changedm note outdated
        }

        if(Dynamit)
        {
            var proj = Instantiate(Dynamite);
            Dynamite.transform.position = this.transform.position;
            Destroy(proj, 2);
        }
    }

        public List<GameObject> lights;
        public int[] table =
        {
            60,
            30,
            10
        };

        public int total;
        public int randomNumber;

        private void StartLootdrop()
        {
            position = transform.position + new Vector3(0, 0, 0);   
        
            foreach (var iteam in table)
            {
                total += iteam;
            }

            randomNumber = UnityEngine.Random.Range(0, total);

            for (int i = 0; i < table.Length; i++)
            {
                if (randomNumber <= table[i])
                {
                    Instantiate(lights[i], position, Quaternion.identity);
                    return;
                }
                else
                {
                    randomNumber -= table[i];
                }
            }
        }

    public void damageOnDeath(int damage)
    {
        currentHealth -= damage;
    }

    public void Hit(int damage)
    {
        //currentHealth -= damage;
        Debug.Log("aoe " + this.gameObject);
        Debug.Log("noCritOnE");
    }

    public void Crit(int damage)
    {
        Debug.Log("critted2OnE hit");
    }
}
