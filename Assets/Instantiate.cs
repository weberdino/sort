using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public Transform parent;

    public void InstantiateButton(GameObject obj)
    {
       Instantiate(obj, parent);
       
    }
}
