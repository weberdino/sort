using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAbility : AbilityCore
{
    public int test;

    public override void use()
    {
        base.use();
        Debug.Log("do stuff now");

        test = 10;
    }

    public override void resetCooldown()
    {
        base.resetCooldown();
    }

    public override void resetDuration()
    {
        base.resetDuration();
        test -= 10;
    }
}
