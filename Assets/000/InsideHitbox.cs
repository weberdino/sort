using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideHitbox : MonoBehaviour
{
    public List<GameObject> enemys;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= enemys.Capacity; i++)
        {
            if(enemys[i] == null)
            {
                enemys.Remove(enemys[i]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        if (go.CompareTag("Enemy"))
        {
            enemys.Add(go);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;

        if (go.CompareTag("Enemy"))
        {
            enemys.Remove(go);
        }
    }

    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.yellow;
       // Gizmos.DrawSphere(transform.position, 4);
    }
}
