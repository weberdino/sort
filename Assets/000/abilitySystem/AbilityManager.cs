using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;


public class AbilityManager : MonoBehaviour
{
    #region Singleton

    public static AbilityManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public bool buttonPressed;
    public string[] defaults = { "Sword", "Knife", "Axe", "Scythe", "Sickel", "Mace", "Spear", "Bow", "Wand" };

    //enum Default { sword, bow, wand }
    public List<AbilityClass> abilities = new List<AbilityClass>();
    public List<AbilityUI> singleCheck = new List<AbilityUI>();
    public List<AbilityClass> instanceCheck = new List<AbilityClass>();

    public GameObject imageHolder;
    public Transform abilityBar;
    public AbilityClass prefabClass;

    public List<AbilityClass> hierarchie;
    public List<int> hierarchieCounter;
    public Transform hierarchieParent;

    public TextMeshProUGUI text;
    int numb = -1;

    private void Update()
    {
        if(buttonPressed)
        {
            useCharges();
            useHierarchie();       
        }

        for(int i = 0; i < abilities.Count; i++)
        {
            abilities[i].Update();
        }
    }

    public void changeDefault(int change)
    {  
        numb += change;
        if(numb < 0) {
            numb = 0;
        }
        else if(numb < defaults.Length)
        {           
            text.text = defaults[numb];
            attackManager.instance.SetAttack(numb);
        }
        else if(numb > defaults.Length)
        {
            numb = defaults.Length -1;
        }
    }

    public void Button(bool pressed)
    {
        buttonPressed = pressed;
    }

    public bool abilityIsUnlocked(AbilityObject ability)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].abilityObject == ability) 
            return true;
        }
        return false;
    }

    public bool abilityIsReady(AbilityObject ability)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].abilityObject == ability)
                return abilities[i].isReady();
        }
        return false;
    }

    public void Add(AbilityObject ability)
    {      
        if (abilityIsUnlocked(ability))
        {
            Debug.Log("skill bereits gelernt");
        }
        else
        {
            AbilityClass abilityClass = new AbilityClass();

            abilityClass.abilityObject = ability;
            abilities.Add(abilityClass);
            hierarchie.Add(abilities[0]);
            for (int i = 0;i < hierarchie.Count;i++)
            {
                
                // if(i == hierarchie.Count - 1)
                {
                    awakeHierachie(i);
                }
            }
        }
    }

    void useCharges()
    {
        for(int i = 0; i < abilities.Count; i++)
        {
            abilities[i].startCharge();
        }
    }

    public void useAbilities()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].StartCooldown();
           // AbilityObject ao = abilities[i];
           // setAbilityCooldown(ao);
        }

    }

    public void useHierarchie()
    {
        //bool exit = false;
        ignoreHierarchie();
        for (int i = 0; i < hierarchie.Count; i++)
        {
            //if (!exit)
            {              
                if (hierarchie[i].isReady() && !hierarchie[i].abilityObject.ignoreHierarchie)
                {
                    hierarchie[i].StartCooldown();
                    return;
                }         
            }                 
        }
    }

    void ignoreHierarchie()
    {
        for (int i = 0; i < hierarchie.Count; i++)
        {
            if (hierarchie[i].abilityObject.ignoreHierarchie )
            {
                hierarchie[i].StartCooldown();
            }
        }
    }

    void awakeHierachie(int slot)
    {
        hierarchieCounter[slot] = slot;
        hierarchie[slot] = abilities[hierarchieCounter[slot]];
        Image img = hierarchieParent.GetChild(slot).GetComponent<Image>();
        img.sprite = hierarchie[slot].abilityObject.icon;

    }

    public void hierarchieUp(int slot)
    {
        if (hierarchieCounter[slot] < hierarchie.Count - 1)
        {
            hierarchieCounter[slot]++;
        }

        for (int i = 0; i < slot; i++)
        {
            if (hierarchie.Count < slot)
            {
                hierarchie.Add(abilities[0]);
            }
        }
        
        hierarchie[slot] = abilities[hierarchieCounter[slot]];
        Image img = hierarchieParent.GetChild(slot).GetComponent<Image>();
        img.sprite = hierarchie[slot].abilityObject.icon;
    }
    public void hierarchieDown(int slot)
    {
        if (hierarchieCounter[slot] > 0)
        {
            hierarchieCounter[slot]--;
        }

        for (int i = 0; i < 5; i++)
        {
            if (hierarchie[slot] == null)
            {
                hierarchie.Add(abilities[0]);
            }
        }

        hierarchie[slot] = abilities[hierarchieCounter[slot]];
        Image img = hierarchieParent.GetChild(slot).GetComponent<Image>();
        img.sprite = hierarchie[slot].abilityObject.icon;        
    }

    public void createUI(AbilityClass abilityClass)
    {
        if (instanceCheck.Contains(abilityClass))
        {
            if (abilityClass.isReady())
            {                 
                //abilityClass.StartCooldown();
                //abilityClass.abilityUI.Initialize(abilityClass, null);
            }
            else
            {
                
            }
        }
        else
        {
            Transform img = Instantiate(imageHolder).transform;
            img.GetComponentInChildren<Image>().sprite = abilityClass.abilityObject.icon;
            //img.GetComponentInChildren<TextMeshProUGUI>().text = abilityClass.abilityObject.charges.ToString();
            AbilityUI imgCd = img.transform.GetComponentInChildren<AbilityUI>();
            imgCd.Initialize(abilityClass, img);
            instanceCheck.Add(abilityClass);

            img.parent = abilityBar;
        }
    }

    public void destroyUI(AbilityClass abilityClass, GameObject go)
    {
        instanceCheck.Remove(abilityClass);
        Destroy(go);
    }
}
