using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class firebreath : MonoBehaviour
{
    public GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fire.SetActive(AbilityManagerNew.instance.button);
    }
}
