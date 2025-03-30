using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeVortex : AbilityCore
{
    public float charge;
     
    private void Update()
    {
        if (AbilityManager.instance.buttonPressed)
        {
            use();
        }
        else if(charge > 0)
        {
            charge -= .1f;
            //Debug.Log
        }

    }

    public override void use()
    {
        charge += 1f;

        if(charge <= 0)
        {
            base.use();
        }
    }
}
