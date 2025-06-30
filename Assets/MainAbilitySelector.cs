using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainAbilitySelector : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Transform activesParent;
    int currentCount = 0;

    public Transform up;
    public Transform down;

    void Start()
    {
       text =  GetComponent<TextMeshProUGUI>();
    }

    public void addAbility(Transform newActive)
    {
        var newA = Instantiate(newActive);
        newA.transform.parent = activesParent;
        newA.transform.name = newActive.name;
    }

    public void arrowButton(bool countUp)
    {
        if (countUp)
        {
            if (currentCount + 1 < activesParent.childCount)           
                currentCount++;
                setAbility(currentCount);         
        }
        else 
        {
            if (currentCount > 0) 
                currentCount--;
                setAbility(currentCount);           
        }
       // manageVisual(countUp);
    }

    void manageVisual(bool upb)
    {
        if (upb)
        {
            if(currentCount + 1 == activesParent.childCount)
            {
                up.gameObject.SetActive(false);
            }
            else
            {
                down.gameObject.SetActive(true);
            }
        }
        else 
        { 
            if(currentCount == 0)
            {
                down.gameObject.SetActive(false);
            }
            else
            {
                up.gameObject.SetActive(true);
            }
        }
    }

    void setAbility(int childIndex)
    {
        foreach(Transform i in activesParent)
        {
            i.gameObject.SetActive(false);
        }
        activesParent.GetChild(childIndex).gameObject.SetActive(true);
        text.text = activesParent.GetChild(childIndex).gameObject.name;
    }
}
