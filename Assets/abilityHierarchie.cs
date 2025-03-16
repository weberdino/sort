using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityHierarchie : MonoBehaviour
{
    public List<GameObject> hierarchies;
    public List<GameObject> always;

    private void FixedUpdate()
    {
        if (AbilityManagerNew.instance.button)
        {
            loadList();
        }
    }

    public void loadList()
    {

        for (int i = 0; i < hierarchies.Count; i++)
        {
            GameObject hierarchie = hierarchies[i];
            AbilityCore ac = hierarchie.GetComponent<AbilityCore>();

            if (ac.noCooldown())
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
