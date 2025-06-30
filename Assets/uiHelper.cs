using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiHelper : MonoBehaviour
{
    public Transform parent;

    public void EnableAndDeactivateElse(Transform targetButton)
    {
        foreach (Transform t in parent)
        {
            t.gameObject.SetActive(false);
        }

        targetButton.gameObject.SetActive(true);
    }
    
}
