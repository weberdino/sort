using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAbility : AbilityCore
{
    public GameObject prefab;
    float refreshRate;
    PlayerStats play;

    // Update is called once per frame
    void Update()
    {
        play = GetComponent<PlayerStats>();

        refreshRate -= Time.deltaTime;

        if (refreshRate <= 0)
        {
            if (noCooldown())
            {
                use();
                refreshRate = .5f;
            }
        }
    }

    public override void use()
    {
        InstantiateFunc(prefab);
        base.use();
    }
}
