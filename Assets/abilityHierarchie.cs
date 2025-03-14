using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityHierarchie : MonoBehaviour
{
    public List<GameObject> hierarchies = new List<GameObject>();

    void loadList()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }


        for (int i = 0; i < hierarchies.Count; i++)
        {
            hierarchies[i].SetActive(true);
        }
    }
}
