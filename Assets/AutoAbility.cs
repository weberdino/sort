using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAbility : AbilityCore
{
    public GameObject prefab;
    float refreshRate;
    PlayerStats play;

    public bool useCollider;

    // Update is called once per frame
    void Update()
    {
        play = GetComponent<PlayerStats>();

        refreshRate -= Time.deltaTime;

        if (!useCollider)
        {
            if (refreshRate <= 0)
            {
                if (noCooldown())
                {
                    use();
                    refreshRate = .5f;
                }
            }
        }
        else if(noCooldown() && refreshRate <= 0)
        {       
            GetComponent<SphereCollider>().enabled = true;
            refreshRate = .5f;
        }
    }

    public override void use()
    {
        var hitbox = Instantiate(prefab);
        hitbox.transform.position = this.transform.position;
        base.use();
        GetComponent<SphereCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (useCollider) {
            if(other.GetComponent<Enemy>() != null)
            {
                if (noCooldown())
                {
                    use();
                }
            }
        }
    }
}
