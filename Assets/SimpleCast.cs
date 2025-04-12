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
        InstantiateFunc(Prefab);
        //buff 
        base.use();
    }

    public override void resetDuration()
    {
        //reset buff

        //base.resetDuration();
    }
}
