using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCore : MonoBehaviour 
{
    public float maxCd;
    float currentCd;
    public float maxDuration = .1f;
    float currentDuration;

    GameObject imageInstance;
    public Sprite icon;

    public bool noCooldown()
    {
        Debug.Log(this.name);
        return Time.time - currentCd >=  maxCd;
    }

    public bool noDuration()
    {
        return Time.time - currentDuration >= maxDuration;
    }

    public bool useable()
    {
        
        return noDuration() || noCooldown();
    }

    public virtual void use()
    {
        Debug.Log(this.name + "use");
        if (useable())
        {
            Debug.Log(this.name + "used");
            currentDuration = Time.time;         
            Invoke("resetDuration", maxDuration);
        }
    }

    public virtual void resetDuration()
    {
        createUi();
        currentCd = Time.time;

        if (imageInstance != null)
        {
            GameObject cdInstance = Instantiate(AbilityManagerNew.instance.cdPrefab, imageInstance.transform);
            AbiliyCooldown aCd = cdInstance.GetComponent<AbiliyCooldown>();
            aCd.initiate(maxCd);
        }
    }
    void createUi()
    {
        AbilityManagerNew manager = AbilityManagerNew.instance;
        GameObject obj = manager.imagePrefab;
        imageInstance = Instantiate(obj, manager.imageParent);
        imageInstance.GetComponent<Image>().sprite = icon;

        Invoke("resetCooldown", maxCd);
    }

    public virtual void resetCooldown()
    {
        Destroy(imageInstance);
    }
}
