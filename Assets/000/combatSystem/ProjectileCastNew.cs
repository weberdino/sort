using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCastNew : MonoBehaviour
{
    public GameObject projPrefab;
    public enum castType  {rifle, volley, spear, split, random}
    public castType cType;
    // public castType usecast;
    [Header("All Projectile Prefabs")]

   // int track;

    
    public InsideHitbox ih;
    public float manualDestroyAfter;

    // movementController move;

    public List<GameObject> volleyParent;
    public GameObject volleyParentAOE;
    float cd;

    float countdown;
    float countdown2;

    //public posUpdater Tornado;

    public Animator anim;
    public bool roundNumb;
    PlayerStats play;
    public bool AimAssist;
    public bool lookAtRotation;

    public int baseProj = 1;
    public List<Quaternion> rota;


    public void SetProjectile(GameObject go)
    {
        projPrefab = go;
    }

    public void Button(bool _pressed)
    {
       // pressed = _pressed;
    }

    public void Update()
    {
        play = GetComponent<PlayerStats>();

        countdown -= Time.deltaTime;

        if (AbilityManager.instance.buttonPressed)
        {
            if (countdown <= 0)
            {
                cast();
                countdown = .5f;
            }
        }
    }

    void cast()
    {
        switch (cType)
        {
            case castType.rifle:
                SimpleCast();
                return;
            case castType.volley:
                castProjectile(projPrefab);
                return;
            case castType.spear:
                ThrowSpears();
                return;
            case castType.split:
                Splitshot(projPrefab);
                return;
            case castType.random:
                castWithRandomRota(projPrefab);
                return;
       }
    }

    //adjust values for spear
    public int numSpears = 5;       // Number of spears to throw
    public float spacing = 1f;      // Distance between each spear
    public Vector3 spawnOffset = Vector3.forward;
    public void ThrowSpears()
    {
        if (numSpears <= 0) return;

        Vector3 playerPosition = transform.position + spawnOffset;

        int middleIndex = numSpears / 2;

        for (int i = 0; i < numSpears; i++)
        {
            float offset = (i - middleIndex) * spacing;
            if (numSpears % 2 == 0)
            {
                offset += spacing / 2f;
            }

            Vector3 spearPosition = playerPosition + (transform.right * offset);
            Instantiate(projPrefab, spearPosition, transform.rotation);

        }
    }

    void SimpleCast()
    {
        int rn = Random.Range(0, ih.enemys.Capacity + 1);
        var p = Instantiate(projPrefab);
        p.transform.position = this.transform.position;
        p.transform.rotation = this.transform.rotation;
        destroyProj(p);
        /*if (ih.enemys.Capacity > 0)
        {
            if (ih.enemys[0] != null)
            {
                p.transform.LookAt(ih.enemys[rn].transform.position); 
            }
        }*/
    }

    public void Splitshot(GameObject obj)
    {
        int arrowCount = 3;

        int rn = Random.Range(0, ih.enemys.Capacity + 1);
        if (ih.enemys.Capacity > 0) ;

        int amount = ih.enemys.Capacity;
        int projAmount = amount;
        if (amount > play.projectileMod.GetValue())
        {
            projAmount = play.projectileMod.GetValue(); ;
        }

        List<GameObject> availableTargets = new List<GameObject>(ih.enemys);

        for (int i = 0; i < projAmount; i++)
        {
            if (availableTargets.Count == 0)
            {
                // Wenn keine verfügbaren Ziele mehr vorhanden sind, fülle die Liste neu
                availableTargets = new List<GameObject>(ih.enemys);
            }
            int randomIndex = Random.Range(0, availableTargets.Count);
            GameObject targetEnemy = availableTargets[i];

            var obj2 = Instantiate(obj);
            obj2.transform.position = this.transform.position;
            obj2.transform.LookAt(targetEnemy.transform.position);

            availableTargets.RemoveAt(randomIndex);
        }
    }

    public void castProjectile(GameObject obj)
    {
        float anzahl = play.projectileMod.GetValue();

        Vector3 playerPosition = transform.position;
        Quaternion playerRotation = transform.rotation;

        if (anzahl % 2 == 1)
        {
            // Ungerade Anzahl von Projektile

            float angleStep = 90f / (anzahl - 1);
            float halfAngle = 45f;
            for (int i = 0; i < anzahl; i++)
            {
                float angle = -halfAngle + i * angleStep;
                if (anzahl == 1)
                {
                    angle = 0;
                }

                Quaternion rotation = Quaternion.Euler(0, angle, 0) * playerRotation;
                Vector3 projectileDirection = rotation * Vector3.forward;
                Vector3 projectilePosition = playerPosition + projectileDirection * 1;
                Debug.Log(i + " " + obj + " " + angle + "(" + -halfAngle + " + " + i + " * " + angleStep);

                Instantiate(obj, projectilePosition, rotation);
            }
        }
        else
        {
            // Gerade Anzahl von Projektilen
            float angleStep = 90f / anzahl;
            float halfAngle = angleStep * (anzahl - 1) / 2;
            for (int i = 0; i < anzahl; i++)
            {
                float angle = -halfAngle + i * angleStep;
                Quaternion rotation = Quaternion.Euler(0, angle, 0) * playerRotation;

                Vector3 projectileDirection = rotation * Vector3.forward;
                Vector3 projectilePosition = playerPosition + projectileDirection * 1;

                Instantiate(obj, projectilePosition, rotation);
            }
        }
    }

    void castWithRandomRota(GameObject obj)
    {
        float anzahl = play.projectileMod.GetValue();

        for (int i = 0; i < anzahl; i++)
        {
            float random = Random.RandomRange(-180, 180);
            Quaternion rotation = Quaternion.Euler(0, random, 0);

            var p = Instantiate(obj);
            p.transform.position = this.transform.position;
            p.transform.rotation = rotation;
        }
    }

    IEnumerator bulletdance(GameObject obj)
    {
        float anzahl = play.projectileMod.GetValue();

        for (int i = 0; i < anzahl; i++)
        {
            yield return new WaitForSeconds(.2f);

            float random = Random.RandomRange(-180, 180);
            Quaternion rotation = Quaternion.Euler(0, random, 0);

            var p = Instantiate(obj);
            p.transform.position = this.transform.position;
            p.transform.rotation = rotation;
        }
    }

    void lockMovement()
    {
        PlayerManager.instance.player.GetComponent<movementController>().enabled = false;
        Invoke("unlock", .2f);
    }

    void unlock()
    {
        PlayerManager.instance.player.GetComponent<movementController>().enabled = true;
    }

    void aimAssist()
    {
        int rn = Random.Range(0, ih.enemys.Capacity + 1);
        if (ih.enemys.Capacity > 0)
        {
            if (ih.enemys[0] != null)
            {
               PlayerManager.instance.player.transform.LookAt(ih.enemys[rn].transform.position);
            }
        }
    }

    void destroyProj(GameObject proj)
    {
        if (manualDestroyAfter != 0)
        {
            //Destroy(proj, manualDestroyAfter);
        }
    }
}
