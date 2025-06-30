using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item//, IDataPersistance
{
    //[ContextMenu("CreateModifier")]
    //  public List<Mods> modstest;    

    //private bool equipped = false;
    
    private LevelSystem levelSystem;
    //private EquipWindow equipWindow;

    public EquipmentSlot equipSlot;
    public weaponType wType;
    //public CastType castType;
    public GameObject ability;
    public MeshRenderer mesh;

    public TextMeshProUGUI text;

    //GameObject stabButton;
    //GameObject slashButton;
    GameObject parent;
    public CastType cType;
    //public CastTypeModule cTypeModule;
 
    public enum weaponType
    {
        Slash,
        Stab,
        Cyclone,
        Lacerate,
        Dash,
        Flicker,
        Throw,

        Bow,
    }

    public int addProjectiles;
    public int damageModifier;
    public int fireDamageModifier;
    public int lightningDamageModifier;
    public int burnModifier;
    public int chargedModifier;   //5

    public int mSpeedModifier; 
    public int critChanceModifier;

    public int multiHitModifier;
    public int bleedChanceModifier;
    public int attackSpeedModifier; //10

    public int maxHealthModifier;  
    public int armorModifier;
    public int hpRegeneration;
    public int fireResistanceModifier;
    public int lightningResistanceModifier;//15
    [Header("Mods")]
    public int hpMultiplier;
    public int armorMultiplier;
    public int regenerationMultiplier;
    public int damageMultiplier;//20
    public int attackSpeedMultiplier;
    public int fireMultiplier;
    public int lightningMultiplier;
    public int amplifyModifier;
    public int critMultiplierModifier;//25

    public int lSteal;

    public int Strength;
    public int Dexterity;
    public int Intelligence;

    public bool doubleStat;

    [Header("Requirements")]
    public int Dex_Requirement;
    public int Str_Requirement;
    public int Int_Requirement;
    public int levelRequirement;

    public bool unique;

    public bool Chain;
    public bool Pierce;
    public bool Return;
    public bool Thorns;

    public int focus;
    public int disperse;

    [Header("UniqueAbility")]
    //public Skills skill;

    //Crafting crafting;

    public bool crafted;

    //public UI_Skilltree uiTree;

    public List<int> modifiers = new List<int>();
    public List<string> names = new List<string>();  

    void useTest()
    {
        base.Use();
        RemoveFromInventory();
        EquipmentManager.instance.Equip(this);
    }

    public override void Use()
    {
        useTest();
        if (GameVariables.Dex >= Dex_Requirement)
        {
            if (GameVariables.Str >= Str_Requirement)
            {
                if (GameVariables.Int >= Int_Requirement)
                {
                    if (GameVariables.Level >= levelRequirement) // Checked for all requierements
                    {
                        if (this.equipSlot.ToString() == "Weapon")
                        {
                            base.Use();
                            //EquipmentManager.instance.Equip(this);
                            AssignAbility();
                            RemoveFromInventory();
                            //uiTree.skills = skill;
                            //uiTree.SpecialSkill();

                            //text.text = damageModifier.ToString();
                            parent = GameObject.Find("combatButtonParent");
                           

                            #region Buttons
                            /* was the setup for attackbutton from weapon
                             * 
                             * parent.transform.GetChild(0).gameObject.SetActive(false);         
                             parent.transform.GetChild(1).gameObject.SetActive(false);
                             parent.transform.GetChild(2).gameObject.SetActive(false);
                             parent.transform.GetChild(3).gameObject.SetActive(false);
                             parent.transform.GetChild(4).gameObject.SetActive(false);
                             parent.transform.GetChild(5).gameObject.SetActive(false);
                             parent.transform.GetChild(6).gameObject.SetActive(false);

                             if (this.wType.ToString() == "Slash")
                             {
                                 //slashButton.gameObject.SetActive(true);
                                 parent.transform.GetChild(0).gameObject.SetActive(true);
                             }
                             if (this.wType.ToString() == "Stab")
                             {
                                 //stabButton.gameObject.SetActive(true);
                                 parent.transform.GetChild(1).gameObject.SetActive(true);
                             }
                             if (this.wType.ToString() == "Cyclone")
                             {
                                 //cyclone
                                 parent.transform.GetChild(2).gameObject.SetActive(true);
                             }
                             if (this.wType.ToString() == "Lacerate")
                             {
                                 //lace
                                 parent.transform.GetChild(3).gameObject.SetActive(true);

                             }
                             if (this.wType.ToString() == "Dash")
                             {
                                 //dash
                                 parent.transform.GetChild(4).gameObject.SetActive(true);
                             }
                             if (this.wType.ToString() == "Flicker")
                             {
                                 //sweep
                                 parent.transform.GetChild(5).gameObject.SetActive(true);

                             }
                             if (this.wType.ToString() == "Throw")
                             {
                                 //flicker
                                 parent.transform.GetChild(6).gameObject.SetActive(true);
                             }
                             if (this.wType.ToString() == "Bow")
                             {
                                 //flicker
                                 parent.transform.GetChild(7).gameObject.SetActive(true);
                             }*/
                            #endregion
                        }
                        else
                        {
                            if (this.equipSlot.ToString() == "Amulet" || this.equipSlot.ToString() == "Ring")
                            {
                                base.Use();
                                //EquipmentManager.instance.EquipJewellery(this);
                                AssignAbility();
                            }
                            else
                            {
                                base.Use();
                                //EquipmentManager.instance.EquipArmor(this);
                                AssignAbility();
                            }

                        }

                        RemoveFromInventory();
                    }
                }
            }
        }
        
    }

    void AssignAbility()
    {
       // globalAbilities.instance.Assign(5);


        switch (cType)
        {
            /*case CastType.onCrit: globalAbilities.instance.crit = PlayerManager.instance.player.GetComponent<PlayerStats>().ability; break;
            case CastType.onKill: globalAbilities.instance.kill = PlayerManager.instance.player.GetComponent<PlayerStats>().ability; break;

            case CastType.mana: globalAbilities.instance.mana = PlayerManager.instance.player.GetComponent<PlayerStats>().ability; break;
            case CastType.whileRegeneration: globalAbilities.instance.regen = PlayerManager.instance.player.GetComponent<PlayerStats>().ability; break;
            case CastType.onWalk: globalAbilities.instance.walk = PlayerManager.instance.player.GetComponent<PlayerStats>().ability; break;
            case CastType.onStop: globalAbilities.instance.stop = PlayerManager.instance.player.GetComponent<PlayerStats>().ability; break;
            case CastType.onBlock: globalAbilities.instance.block = PlayerManager.instance.player.GetComponent<PlayerStats>().ability; break;
            case CastType.auto: globalAbilities.instance.auto = PlayerManager.instance.player.GetComponent<PlayerStats>().ability; break;

            case CastType.charge: globalAbilities.instance.charge = PlayerManager.instance.player.GetComponent<PlayerStats>().ability; break;*/

           // CastType.outOfCombat
           // CastType.triggerZone
           // CastType.onStun
           // CastType.
        }
    }
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }
}

public enum EquipmentSlot { Helmet, Armor, Weapon, Glove, Boots,  Amulet, Ring, Node }
public enum CastType {EMPTY, onCrit, onKill, onStun, charge, onBlock, ultGauge, mana, outOfCombat, whileInCombat, onWalk, onStop, whileRegeneration, auto, triggerZone,onHitWIP }

