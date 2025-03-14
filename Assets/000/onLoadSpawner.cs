using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class onLoadSpawner : MonoBehaviour
{
    public int maxAmount;
    int minAmount;
    public int RandomRange;
    public GameObject enemyPrefab;
    public GameObject parent;

    private Vector3 Min;
    private Vector3 Max;
    private float _xAxis;
    private float _zAxis;
    private Vector3 position;

    //public moveChild mc;

    public float delay;    

    public Quaternion setRota;

    Vector3 point;
    public bool respawn;
    public List<Collider> enem;

    #region Singleton

    public static onLoadSpawner instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (parent != null)
        {
            //mc = transform.Find("ui").GetComponent<moveChild>();

            //mc = PlayerManager.instance.player.GetComponentInChildren<moveChild>();               //This also
           // mc = moveChild.instance;

            //mc = PlayerManager.instance.player.gameObject.GetComponentInChildren<moveChild>();
          // enemyPrefab = mc.enemy.gameObject;                                                    //comment this out back WIP
            //enemyPrefab = parent.transform.GetChild(0).gameObject;
        }

        minAmount = Random.RandomRange(0, maxAmount);

        SetRanges();

       /* if(enemyPrefab = null)
        {
            enemyPrefab = parent.transform.GetChild(0).gameObject;
        }*/

        //for (int i = 0; i < randomAmount; i++)
        {
            if (enemyPrefab != null)
            {
                StartCoroutine(Spawn());
            }
        }
    }

    public void spawnReset()
    {
        for(int i = 0 ; i < enem.Count; i++)
        {
            Destroy(enem[i].gameObject);
        }
        enem.Clear();
    }

    IEnumerator Spawn()
    {
        if (!respawn)
        {
            yield return new WaitForSeconds(delay);
            for (int i = 0; i < maxAmount; i++)
            {
                yield return new WaitForSeconds(.1f);
                GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);
                parent = GameObject.Find("enemyHandler").gameObject;
                if (parent != null)
                {
                    if (GetRandomNavmeshPos(out point))
                    {
                        Debug.Log("yyyyy" + point + "info about sphere:" + Random.insideUnitSphere * RandomRange);
                        enemyClone.transform.position = point;
                    }
                    // enemyClone.transform.parent = parent.transform;
                }
                if (setRota != null)
                {
                    //enemyClone.transform.rotation = setRota;
                }


                //BakeNavMesh.instance.refreshNav();
                yield return new WaitForSeconds(.01f);
            }
        }
    }

    void SingleSpawn()
    {
        GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);
        parent = GameObject.Find("enemyHandler").gameObject;
        if (parent != null)
        {
            if (GetRandomNavmeshPos(out point))
            {
                Debug.Log("yyyyy" + point + "info about sphere:" + Random.insideUnitSphere * RandomRange);
                enemyClone.transform.position = point;
            }
            // enemyClone.transform.parent = parent.transform;
        }
    }


    bool GetRandomNavmeshPos(out Vector3 result)
    {
        //while(!check)
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = this.transform.position + Random.insideUnitSphere * RandomRange;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                //check = NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas);
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void SetRanges()
    {
        Min = new Vector3(-RandomRange, 0, -RandomRange); //Random value.
        Max = new Vector3(RandomRange, 0, RandomRange);
    }

    void Update()
    {
       // this.transform.position + insideUnitSphere * RandomRange;

       // mc = transform.Find("ui").GetComponent<moveChild>();
       // Debug.Log("tes" + PlayerManager.instance.player.GetComponent<GameObject>());

        _xAxis = Random.Range(Min.x, Max.x);
        _zAxis = Random.Range(Min.z, Max.z);
        position = transform.position + new Vector3(_xAxis, 0, _zAxis);

        if (respawn)
        {
            if (enem.Count < maxAmount)
            {
                Collider[] coll = Physics.OverlapSphere(this.transform.position, RandomRange);
            foreach (Collider collider in coll)
            {
                if (collider.name.Contains(enemyPrefab.name))
                {
                    enem.Add(collider);
                }
            }
            
                SingleSpawn();
            }
            else
            {
                for (int i = 0; i < enem.Count; i++)
                {
                    if (enem[i] == null)
                    {
                        enem.Remove(enem[i]);
                    }    
                }
            }
            
            
        }
    }

    public void extraSpawn(GameObject extra)
    {
        SetRanges();
        var ene = Instantiate(extra, position, Quaternion.identity);

        ene.transform.parent = this.gameObject.transform;
        //ene.transform.parent = parent.transform;
        ene.transform.position = this.transform.position;
    }

    private void OnDrawGizmos()
    {
        //Vector3 gizmo = Min + Max;

       // Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, Min * 2);
    }
}
