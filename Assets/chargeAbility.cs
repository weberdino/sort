using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChhargeAbility : AbilityCore
{
    bool activated = false;

    void Update()
    {
        bool button = AbilityManagerNew.instance.button;

        if (button)
        {
            if (isReady())
            {
                activated = true;
            }
        }
        else if (activated)
        {
            use();
            activated = false;
        }
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
