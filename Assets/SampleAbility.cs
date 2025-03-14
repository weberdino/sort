using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAbility : AbilityCore
{
    public int test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady())
        {
            Debug.Log("do stuff now");
            test = 10;
        }
    }

    public override void setBack()
    {
        test -= 10;
    }
}
