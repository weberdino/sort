using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Description", menuName = "UI")]
public class descriptionObject : ScriptableObject
{
    public string tx;
    public GameObject unlockable;
    public Sprite img;
    public bool mainAbility;
}
