using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityHierarchie : MonoBehaviour
{
    public List<GameObject> hierarchies;
    public List<GameObject> always;
    float minTimer;

    public int currentAbilityIndex = 0;

    private void FixedUpdate()
    {
        if (AbilityManagerNew.instance.button)
        {
            if (minTimer < 0)
            {
                loadList(currentAbilityIndex);
                minTimer = 1;
            }
        }

        minTimer -= Time.deltaTime;
    }

    public void loadList(int index)
    {
        for (int i = index; i < hierarchies.Count; i++)
        {
            GameObject hierarchie = hierarchies[i];
            AbilityCore ac = hierarchie.GetComponent<AbilityCore>();
            currentAbilityIndex = i+1;
            if(currentAbilityIndex == hierarchies.Count)
            {
                currentAbilityIndex = 0;
            }

            if (ac.useable())
            {
                ac.use();
                return;
            }
            
        }
    }

    void unloadList()
    {
        //
    }
}
