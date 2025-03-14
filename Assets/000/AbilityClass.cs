using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AbilityClass
{
    public AbilityObject abilityObject;
    public float remainingCooldown;
    public AbilityUI abilityUI;
    public int charges;
    public float duration;

    public int getCharges()
    {
        return charges;
    }

    public bool isReady()
    {
        if (inDuration())
        {
            return true;
        }
        else
        {
            return remainingCooldown <= 0;
        }      
    }

    public bool inDuration()
    {
        if (charges > 0)
        {
            return true;
        }
        else
        {
            return duration > 0;
        }      
    }

    // Update is called once per frame
    public void Update()
    {
        

        if(!isReady())
        {
            updateCd();
        }
        else if(duration > 0 )
        {
            updateDuration();
        }
        else if(abilityUI != null && duration <= 0 && charges != 0)
        {
            updateCharge();
        }
        else if(isReady())
        {
            destroyUi();
        }
    }

    void updateCd()
    {                
        remainingCooldown -= Time.deltaTime;      
    }
    void updateDuration()
    {
        if (!AbilityManager.instance.buttonPressed)
        {
            duration -= Time.deltaTime;
           
        }       
    }
    void updateCharge()
    {
        charges--;
        duration = abilityObject.duration;
    }

    public void startCharge()
    {
        if (isReady())
        {
            if (abilityObject.charges == 0)
            {
                duration = abilityObject.duration;
            }
            else
            if (charges < abilityObject.charges)
            {
                if (duration < abilityObject.duration)
                {
                    duration += Time.deltaTime;
                    Debug.Log(duration + "dura1");
                }
                if (duration >= abilityObject.duration)
                {
                    Debug.Log(duration + "dura21");
                    duration = 0.1f;
                    Debug.Log(duration + "dura22");
                    charges++;
                    Debug.Log(duration + "dura2");
                }
            }
            
        }
    }

    public void StartCooldown()
    {
        if(isReady())
        {
            int multi = 1;
            if(charges > 0)
            {
                multi = charges;
            }         
            remainingCooldown = abilityObject.cd; //+ (duration * multi)
            createUI();          
        }     
    }

    public void createUI()
    {
        AbilityManager am = AbilityManager.instance;
        am.createUI(this);      
    }

    public void destroyUi()
    {
        AbilityManager am = AbilityManager.instance;
        am.destroyUI(this, abilityUI.gameObject.transform.parent.transform.parent.gameObject);
    }
}
