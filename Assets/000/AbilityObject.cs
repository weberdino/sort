using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]

public class AbilityObject : ScriptableObject
{
    public bool ignoreHierarchie;
    public float cd;
    public Sprite icon;
    public float duration;
    public int charges;

    // public enum castType { cd, charge, trigger}
    // public enum helpo { buff}



    // public Image cdIndicator;
    // public string name;
}
