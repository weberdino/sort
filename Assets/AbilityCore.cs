using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCore : MonoBehaviour 
{
    public bool aura;
    public float maxCd;
    float currentCd;
    public float maxDuration = .1f;
    float currentDuration;

    public GameObject imageInstance;
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
        return noDuration() && noCooldown();
    }

    public virtual void use()
    {
        createUi();
        if (useable())
        {
            Debug.Log(this.name + "used");
            currentDuration = Time.time;
            
            Invoke("resetDuration", maxDuration);                   
        }
    }

    public virtual void resetDuration()
    {
        currentCd = Time.time;
        
        if (imageInstance != null)
        {
            Debug.Log(gameObject.name);
            //
            GameObject cdInstance = Instantiate(AbilityManagerNew.instance.cdPrefab, imageInstance.transform);
            AbiliyCooldown aCd = cdInstance.GetComponent<AbiliyCooldown>();
            aCd.initiate(maxCd);
        }    
    }
    public void createUi()
    {
        Debug.Log(this.name);
        //
        AbilityManagerNew manager = AbilityManagerNew.instance;
        GameObject obj = manager.imagePrefab;
        imageInstance = Instantiate(obj, manager.imageParent);
        imageInstance.GetComponent<Image>().sprite = icon;

        if (!aura)
        {
            Invoke("resetCooldown", maxCd);
        }
    }

    public virtual void resetCooldown()
    {
        Destroy(imageInstance);
    }

    /*public void InstantiateFunc(GameObject obj)
    {
        var hitbox = Instantiate(obj);
        hitbox.transform.position = this.transform.position;
    }*/

    public void stanceDisable()
    {
        gameObject.SetActive(false);
    }
}
