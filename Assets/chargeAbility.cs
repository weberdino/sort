using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChhargeAbility : AbilityCore
{
    bool activated = false;
    float charge;

    public override void use()
    {
        if (!activated)
        {
            activated = true;
        }

        base.use();

    }

    public override void resetCooldown()
    {
        base.resetCooldown();
    }
    public override void resetDuration()
    {
        base.resetDuration();
    }

    private void Update()
    {
        if (AbilityManagerNew.instance.button)
        {
            charge += .2f;
        }
        else if(charge > 0)
        {
            charge -= .2f;
        }
    }
}
