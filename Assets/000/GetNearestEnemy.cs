using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetnearestEnemy : MonoBehaviour
{
    public GameObject closestEnemy;
    public GameObject rainZone;
    private List<GameObject> enemiesInRange = new List<GameObject>();
    public List<GameObject> sortedEnemies = new List<GameObject>();

    float time;
    public bool forProjectile;

    public GameObject Rain;
    public bool ready;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            UpdateClosestEnemy(); // Ensure nearestEnemy is updated
            SortEnemiesByDistance(); // Update sorted list
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            UpdateClosestEnemy(); // Update nearestEnemy in case the closest one left
            SortEnemiesByDistance(); // Update sorted list
        }
    }

    private void UpdateClosestEnemy()
    {
        closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy == null) continue; // Skip destroyed or null enemies

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        // Optionally adjust for projectiles here if needed
        if (closestEnemy != null && forProjectile && closestEnemy.transform.childCount > 0)
        {
            closestEnemy = closestEnemy.transform.GetChild(0).gameObject;
        }
    }

    private void SortEnemiesByDistance()
    {
        // Creates a new sorted list; does not modify the original enemiesInRange
        sortedEnemies = enemiesInRange
            .Where(e => e != null)
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .ToList();
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (closestEnemy != null)
        {
            // Perform actions using the closestEnemy GameObject
            Debug.Log("Closest enemy: " + closestEnemy.name);
            if (time < 0)
            {
                rainZone.transform.position = closestEnemy.transform.position;
                ready = true;
            }
        }
        else
        {
            if (time < 0)
            {
                rainZone.transform.position = this.transform.position;
                ready = false;
            }
        }
    }

    public void Button()
    {
        if (time < 0)
        {
            var r = Instantiate(Rain);
            r.transform.position = rainZone.transform.position;
            time = 5;
            Destroy(r, 5);
        }
    }
}
