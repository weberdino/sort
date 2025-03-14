using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsHandler : MonoBehaviour
{
    public int Level;
    public int baseStat;
    public int modStat;
    public int maxStats;

    public int dmgStat;

    public static EnemyStatsHandler instance;

    void Awake()
    {
        instance = this;
        Debug.Log("instance");
    }

    public GameObject overrideEnemy;

    public void SetStat(EnemyStats es)
    {
        //int lv = PlayerManager.instance.player.GetComponent<EnemyStatsHandler>().Level;
       // Level = lv;
        int maxBase = baseStat + maxStats;
        int maxMod = modStat + maxStats;

        int val = Random.Range(baseStat, maxBase);
        int lvvalue = Level * 20;
        int val2 = val + lvvalue;
        modStat = Random.Range(modStat, maxMod);

        es.maxHealth.AddModifier(val2);
        es.currentHealth = es.maxHealth.GetValue();

        es.damage.AddModifier(dmgStat);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
