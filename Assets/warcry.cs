using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warcry : AbilityCore
{
    public GameObject warCryPrefab;

    public override void use()
    {
        var hitbox = Instantiate(warCryPrefab);
        hitbox.transform.position = this.transform.position;
        //buff 
        base.use();
    }

    public override void resetDuration()
    {
        //reset buff

        //base.resetDuration();
    }
}
