using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChhargeAbility : AbilityCore
{
    bool activated = false;

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
            charger += .2f;
        }
        else if(charger > 0)
        {
            charger -= .2f;
        }
    }
}
