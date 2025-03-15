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
        Debug.Log("testC");

    }

    public override void setBack()
    {
        base.setBack();
    }
    public override void resetDuration()
    {
        base.resetDuration();
    }
}
