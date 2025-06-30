using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image img;
    public GameObject skill;
    public Transform parent;
    bool mainAbility;
    public MainAbilitySelector mainAbilitySelector;
    public void showDescription(descriptionObject obj)
    {
        text.text = obj.tx;
        img.sprite = obj.img;
        skill = obj.unlockable;
        mainAbility = obj.mainAbility;
    }

    public void Instantiation()
    { 
        if(!mainAbility)
        {
            Instantiate(skill, parent);
        }
        else
        {
            mainAbilitySelector.addAbility(skill.transform); 
        }
    }
}