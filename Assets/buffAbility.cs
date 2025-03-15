using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAbility : AbilityCore
{
    public int test;

    public override void use()
    {
        base.use();
        Debug.Log("testB");
        test = 10;
    }

    public override void setBack()
    {
        base.setBack();
    }

    public override void resetDuration()
    {
        base.resetDuration();
        test -= 10;
    }
}
