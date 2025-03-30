using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warcry : AbilityCore
{
    public GameObject warCryPrefab;

    public override void use()
    {
        InstantiateFunc(warCryPrefab);
        base.use();
    }

}
