using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReynaEyeProjectile : MonoBehaviour
{
    public float speed;
    float timer;
    public float range;

    EnemyStats eStats;

    List<EnemyStats> enemies;

    void FixedUpdate()
    {
        transform.position += (transform.forward * speed) / 100;

        timer += Time.deltaTime;
        if(timer > range)
        {
           // transform.GetChild(0).gameObject.SetActive(true);
            //Destroy(this.gameObject);
            speed = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Enemy"))
       {
            eStats = other.GetComponent<EnemyStats>();

            eStats._Blind = true;
            enemies.Add(eStats);
       }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            eStats = other.GetComponent<EnemyStats>();

            eStats._Blind = false;

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Blind = false;
                enemies.Remove(enemies[i]);
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Blind = false;
            enemies.Remove(enemies[i]);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Blind = false;
            enemies.Remove(enemies[i]);
        }
    }
}
