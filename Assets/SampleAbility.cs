using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAbility : AbilityCore
{
    public int test; 
    /*void Update()
    {
        if (AbilityManagerNew.instance.button)
        {
            if (isReady())
            {
                use();
                
            }
        }
    }*/

    public override void use()
    {
        base.use();
        Debug.Log("do stuff now");
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
