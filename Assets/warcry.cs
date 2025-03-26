using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warcry : MonoBehaviour
{
    public GameObject warCryPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AbilityManagerNew.instance.button)
        {
           // cast();
        }
    }

  
}
