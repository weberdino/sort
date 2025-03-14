using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SetTarget : MonoBehaviour
{
    public float fireCooldown = 0f;
    public float fireRate = 0.1f;
    public Transform spawnPoint;

    public GameObject projectilePrefab;

    private Quaternion targetRotation = new Quaternion();

    public List<GameObject> inRangeMonsters = new List<GameObject>();

    public int Stacks;
    public bool notTurret;

    public bool passivTrackPlayer;

    void OnDisable()
    {
        inRangeMonsters.Clear();
    }

    void Update()
    {
        if (inRangeMonsters.Count > 0)
        {
            fireCooldown += Time.deltaTime;

            while(fireCooldown >= fireRate)
            {
                LookAt(inRangeMonsters[0]);
                fireCooldown -= fireRate;
            }
        }
        else
        {
            if (passivTrackPlayer)
            {
                fireCooldown += Time.deltaTime;
                while (fireCooldown >= fireRate)
                {
                    LookAt(PlayerManager.instance.player.gameObject);
                    fireCooldown -= fireRate;
                }
            }
            else
            {


                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        GameObject go = collision.gameObject;

        if (go.CompareTag("Enemy"))
        {
            inRangeMonsters.Add(go);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        GameObject go = collision.gameObject;

        if (go.CompareTag("Enemy"))
        {
            inRangeMonsters.Remove(go);
        }
    }

    public void LookAt(GameObject targetObject)
    {
        //Debug.Log(targetObject);
        //Debug.Log(transform.rotation);

        Quaternion lookRotation = Quaternion.LookRotation(targetObject.transform.position - transform.position);
        //lookRotation = new Quaternion(0, lookRotation.y, 0, 100);

        //targetRotation = Quaternion.LookRotation(targetObject.transform.position);
       

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        //Debug.Log("rottes " + tr.x + " " + tr.y + " " + tr.z);

        if (!notTurret)
        {
            var projetileInstance = Instantiate(
            projectilePrefab,
            spawnPoint.position,
            lookRotation);
        }
        else
        {
            //LookAtConstraint la = GetComponent<LookAtConstraint>();           
            float dis = Vector3.Distance(this.transform.position, targetObject.transform.position);
           // Debug.Log("tes" + dis);
            if (dis < 6)
            {
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            
        }
        
    }
}


