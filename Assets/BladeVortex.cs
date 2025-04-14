using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeVortex : AbilityCore
{
    bool inUse;
    GameObject[] blades;
    float charge;

    private void Start()
    {
        blades = new GameObject[transform.childCount];

        for (int i = 0; i < this.transform.childCount; i++)
        {
            blades[i] = this.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if (AbilityManagerNew.instance.button)
        {
            if (noCooldown())
            {
                getCharge();
                bladeFunc();
            }
           
        }
        else 
        {
            loseCharge();
        }
    }

    void getCharge()
    {
        charge += .1f;
        inUse = true;
    }

    void loseCharge()
    {
        if (charge > 0)
        {
            charge -= .1f;
            bladeFunc();

        }
        else if (inUse)
        {
            use();
            inUse = false;
        }
    }

    void bladeFunc()
    {
        int activeBlades = (int)(charge / 20); // Berechnet, wie viele Blades aktiv sind

        for (int i = 0; i < blades.Length; i++)
        {
            blades[i].SetActive(i < activeBlades);
        }
    }

    public override void use()
    {
        base.use();
    }
}
