using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AbiliyCooldown : MonoBehaviour
{
    float maxCd;
    float cdTimer;
    Image img;
    bool activated;

    public void initiate(float cd)
    {
        maxCd = cd;
        cdTimer = cd;
        img = GetComponent<Image>();
    }


    // Update is called once per frame
    void Update()
    {
        if (cdTimer > 0) {
            activated = true;
            cdTimer -= Time.deltaTime;
            img.fillAmount = cdTimer / maxCd;
        }
        if (activated)
        {
            if (cdTimer < 0)
            {
                GameObject obj = this.gameObject.transform.parent.gameObject;
                Destroy(obj);
            }
        }
    }
}
