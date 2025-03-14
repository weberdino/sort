using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManagerNew : MonoBehaviour
{
    #region Singleton

    public static AbilityManagerNew instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public bool button;
    public Transform imageParent;
    public GameObject imagePrefab;
    public GameObject cdPrefab;

    public List<GameObject> images = new List<GameObject>();   

    public void setButton(bool pressed)
    {
        button = pressed;
    }
}
