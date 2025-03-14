using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    Image img;
    float cd;
    public AbilityClass ability;
    TextMeshProUGUI text;
    public Image chargeImg;
    

    private void Start()
    {
        img = GetComponent<Image>();
    }

    public void Initialize(AbilityClass ac, Transform img)
    {
        ability = ac;
        ac.abilityUI = this;
        //img.GetComponentInChildren<Image>().sprite = ac.abilityObject.icon;
        text = img.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ability.inDuration())
        {
            img.fillAmount = 0;
            this.transform.parent.GetChild(1).gameObject.SetActive(true);
            text.text = ability.charges.ToString();
            chargeImg.fillAmount = ability.duration / ability.abilityObject.duration;
        }      
        else if(ability.charges == 0)
        {
            img.fillAmount = ability.remainingCooldown / ability.abilityObject.cd;
            this.transform.parent.GetChild(1).gameObject.SetActive(false);        
        }
    }
}
