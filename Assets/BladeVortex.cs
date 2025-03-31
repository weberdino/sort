using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeVortex : AbilityCore
{
    bool inUse;

    private void Update()
    {
        if (AbilityManagerNew.instance.button)
        {
            if (noCooldown())
            {
                getCharge();
            }
           
        }
        else 
        {
            loseCharge();
        }
    }

    void getCharge()
    {
        charger += .1f;
        inUse = true;
    }

    void loseCharge()
    {
        if (charger > 0)
        {
            charger -= .1f;

        }
        else if (inUse)
        {
            use();
            inUse = false;
        }
    }


    public override void use()
    {
        base.use();
    }
}
