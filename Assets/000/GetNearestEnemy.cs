using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetNearestEnemy : AbilityCore
{
    [Header("GetNearestEnemy")]
    public GameObject closestEnemy;
    public GameObject target;
    public GameObject prefab;
    private List<GameObject> enemiesInRange = new List<GameObject>();
    public List<GameObject> sortedEnemies = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            enemiesInRange.Add(other.gameObject);
            UpdateClosestEnemy(); 
            SortEnemiesByDistance();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            enemiesInRange.Remove(other.gameObject);
            UpdateClosestEnemy(); 
            SortEnemiesByDistance();
        }
    }

    private void UpdateClosestEnemy()
    {
        closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy == null) continue; 

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
                target.transform.parent = closestEnemy.transform;
                target.transform.localPosition = Vector3.zero;
                use();
            }
        }
    }

    public override void use()
    {
        if (useable())
        {
            Instantiate(prefab, transform.parent);
        }
        base.use();
    }

    private void SortEnemiesByDistance()
    {
        sortedEnemies = enemiesInRange
            .Where(e => e != null)
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .ToList();
    }

    private void Update()
    {
        /*time -= Time.deltaTime;

        if (closestEnemy != null)
        {
            if (time < 0)
            {
                target.transform.position = closestEnemy.transform.position;
                ready = true;
            }
        }
        else
        {
            if (time < 0)
            {
                target.transform.position = this.transform.position;
                ready = false;
            }
        }*/
    }

    public void Button()
    {
        /*if (time < 0)
        {
            var r = Instantiate(Rain);
            r.transform.position = target.transform.position;
            time = 5;
            Destroy(r, 5);
        }*/
    }
}
