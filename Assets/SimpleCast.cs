using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCast : AbilityCore
{
    public GameObject Prefab;
    public Animator anim;

    public override void use()
    {
        var hitbox = Instantiate(Prefab);
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
