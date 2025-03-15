using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCore : MonoBehaviour 
{
    public float maxCd;
    float currentCd;
    public float maxDuration;

    GameObject imageInstance;
    public Sprite icon;
    public bool isReady()
    {
        return Time.time - currentCd >=  maxCd;
    }

    public virtual void use()
    {
        if (isReady())
        {
            currentCd = Time.time;
            createUi();
            Invoke("resetDuration", maxDuration);
        }
    }

    void createUi()
    {
        AbilityManagerNew manager = AbilityManagerNew.instance;
        GameObject obj = manager.imagePrefab;      
        imageInstance = Instantiate(obj, manager.imageParent);
        imageInstance.GetComponent<Image>().sprite = icon ;

        Invoke("setBack", maxCd);
    }

    public virtual void resetDuration()
    {
        GameObject cdInstance = Instantiate(AbilityManagerNew.instance.cdPrefab, imageInstance.transform);
        AbiliyCooldown aCd = cdInstance.GetComponent<AbiliyCooldown>();
        aCd.initiate(maxCd - maxDuration);
    }

    public virtual void setBack()
    {
        Destroy(imageInstance);
    }
}
