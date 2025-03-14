using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDataPersistance
{
    private LevelSystem levelSystem;
    private PlayerSkills playerSkills;

   // public int PlayerNumber; //no need ?

    public name compareClass;
    public name avoidClass;

    public Transform list;
    Image icon;

    PlayerStats playerStats;

    public List<PlayerSkills.SkillType> skilltest;
    public List<Sprite> imgTest;

    //Get class to set the tree bools
    //use enum with classes and set the enum object from "ui / skilltreeuiswap"

    public void GetpSkills(PlayerSkills newPSkills)
    {
        newPSkills = playerSkills;
    }

    public enum name
    {
        Berserk,
        Ninja,
        Paladin,

        Hunter,
        Trapper,
        Cowboy,

        Necromancer,
        Elementalist,
        Holy,

        universal,
    }

    private void Awake()
    {
        playerSkills = new PlayerSkills();
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        //levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged; !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        playerStats = GetComponent<PlayerStats>();

        //playerSkills.UnlockSkill(playerSkills.unlockedSkillTypeList[0], playerSkills.unlockedImage[0]);
        //playerSkills.TryUnlockSkill(test.skillType, test.sprite);
        //playerSkills.unlockedSkillTypeList.Remove(test.skillType);
    }

    public void Update()
    {
        //Debug.Log("skillimage: " + playerSkills.unlockedImage.Count);
        for (int i = 0; i < playerSkills.unlockedSkillTypeList.Count; i++)
        {
            skilltest[i] = playerSkills.unlockedSkillTypeList[i];
        }

        //for (int i = 0; i < playerSkills.unlockedImage.Count; i++)
        //{
        //   imgTest[i] = playerSkills.unlockedImage[i];
        //}

        int b = playerSkills.unlockedSkillTypeList.Count;
        //Debug.Log("skill_" + b);
        //Debug.Log("skill_ " + playerSkills.unlockedSkillTypeList[b]);
    }

    /*public void GetPoints()
    {
        playerSkills.GetSkillPoints();
    }*/

    public void LoadData(GameData data)
    {
        playerSkills.unlockedSkillTypeList = data.unlockedSkills;
        playerSkills.unlockedImage = data.unlockedImage;
        Debug.Log("load");
    }

    public void SaveData(ref GameData data)
    {
        data.unlockedSkills = playerSkills.unlockedSkillTypeList;
        data.unlockedImage = playerSkills.unlockedImage;
        Debug.Log("save");

        data.scene = SceneManager.GetActiveScene().buildIndex;
    }

    public void Button()
    {
        playerSkills.ResetSkillsAll();
    }

   // #region PlayerSkills

    //private void PlayerSkills_OnSkillRemove(...)

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        switch (e.skillType)
        {
            case PlayerSkills.SkillType.str:
                break;
        }


        for (int i = 0; i < playerSkills.unlockedImage.Capacity; i++)
        {
            if (playerSkills.unlockedImage[i] != null) // Set icons for Abilities
            {
                icon = list.transform.GetChild(i).GetComponentInChildren<Image>();
                Debug.Log("list: " + list);

                //icon = list.transform.GetComponentInChildren<Image>();

                icon.sprite = playerSkills.unlockedImage[i];
            }
        }

        #region order
        /*case PlayerSkills.SkillType.Linkdash:
            CanUseLinkdash();
            break;

        case PlayerSkills.SkillType.CurseArrow:
            CanUseCurseArrow();
            break;

        case PlayerSkills.SkillType.Bladestorm:
            CanUseBladeStorm();
            break;

        case PlayerSkills.SkillType.Sweep:
            CanUseSweep();
            break;

        case PlayerSkills.SkillType.Explosion:
            CanUseExplosion();
            break;
        case PlayerSkills.SkillType.Aoe:
            CanUseAoe();
            break;
        case PlayerSkills.SkillType.BladeVortex:
            CanUseBladeVortex();
            break;
        case PlayerSkills.SkillType.Bomber:
            CanUseBomber();
            break;*/


        /*case PlayerSkills.SkillType.admg1:
            SetDamage();
            break;
        case PlayerSkills.SkillType.admg2:
            SetDamage();
            break;
        case PlayerSkills.SkillType.admg3:
            SetDamage();
            break;
        case PlayerSkills.SkillType.astr1:
            SetStrength();
            break;
        case PlayerSkills.SkillType.astr2:
            SetStrength();
            break;
        case PlayerSkills.SkillType.astr3:
            SetStrength();
            break;

        case PlayerSkills.SkillType.astr4:
            SetStrength();
            break;
        case PlayerSkills.SkillType.admg4:
            SetDamage();
            break;
        case PlayerSkills.SkillType.admg5:
            SetDamage();
            break;
        case PlayerSkills.SkillType.alsteal1:
            SetLifeSteal();
            break;
        case PlayerSkills.SkillType.areg1:
            SetRegeneration();
            break;
        case PlayerSkills.SkillType.areg2:
            SetRegeneration();
            break;
        case PlayerSkills.SkillType.admg6:
            SetDamage();
            break;
        case PlayerSkills.SkillType.admg7:
            SetDamage();
            break;


        case PlayerSkills.SkillType.bstr1:
            SetStrength();
            break;
        case PlayerSkills.SkillType.bstr2:
            SetStrength();
            break;
        case PlayerSkills.SkillType.bexedmg1:
            SetExeDamage();
            break;
        case PlayerSkills.SkillType.bstr3:
            SetStrength();
            break;
        case PlayerSkills.SkillType.bstr4:
            SetStrength();
            break;
        case PlayerSkills.SkillType.blsteal1:
            SetLifeSteal();
            break;
        case PlayerSkills.SkillType.blsteal2:
            SetLifeSteal();
            break;


        case PlayerSkills.SkillType.chp1:
            SetMaxHp();
            break;
        case PlayerSkills.SkillType.chp2:
            SetMaxHp();
            break;
        case PlayerSkills.SkillType.cstr1:
            SetStrength();
            break;
        case PlayerSkills.SkillType.cblock1:
            SetBlock();
            break;
        case PlayerSkills.SkillType.cblock2:
            SetBlock();
            break;
        case PlayerSkills.SkillType.cfres1:
            SetDex(); // USELESS quasi
            break;
        case PlayerSkills.SkillType.clres1:
            SetDex();
            break;
        case PlayerSkills.SkillType.cflres1:
            SetDex();
            break;


        case PlayerSkills.SkillType.dreg1:
            SetRegeneration();
            break;
        case PlayerSkills.SkillType.dreg2:
            SetRegeneration();
            break;
        case PlayerSkills.SkillType.dblock1:
            SetBlock();
            break;
        case PlayerSkills.SkillType.dhp1:
            SetMaxHp();
            break;
        case PlayerSkills.SkillType.dhp2:
            SetMaxHp();
            break;
        case PlayerSkills.SkillType.dblock2:
            SetBlock();
            break;
        case PlayerSkills.SkillType.dhp3:
            SetMaxHp();
            break;
        case PlayerSkills.SkillType.dhp4:
            SetMaxHp();
            break;

        case PlayerSkills.SkillType.ecritC1:
            SetCritChance();
            break;
        case PlayerSkills.SkillType.ecritC2:
            SetCritChance();
            break;
        case PlayerSkills.SkillType.ecritC3:
            SetCritChance();
            break;
        case PlayerSkills.SkillType.edex1:
            SetDex();
            break;
        case PlayerSkills.SkillType.edex2:
            SetDex();
            break;
        case PlayerSkills.SkillType.edex3:
            SetDex();
            break;

        case PlayerSkills.SkillType.edex4:
            SetDex();
            break;
        case PlayerSkills.SkillType.ecritD1:
            SetCritDamage();
            break;
        case PlayerSkills.SkillType.ecritD2:
            SetCritDamage();
            break;
        case PlayerSkills.SkillType.ecritD3:
            SetCritDamage();
            break;
        case PlayerSkills.SkillType.edex5:
            SetDex(); 
            break;
        case PlayerSkills.SkillType.edex6:
            SetDex();
            break;
        case PlayerSkills.SkillType.ereg1:
            SetRegeneration();
            break;
        case PlayerSkills.SkillType.ereg2:
            SetRegeneration();
            break;


        case PlayerSkills.SkillType.fdex1:
            SetInt();
            break;
        case PlayerSkills.SkillType.fdex2:
            SetInt();
            break;
        case PlayerSkills.SkillType.fmspd1:
            SetCritChance();
            break;
        case PlayerSkills.SkillType.fmspd2:
            SetCritChance();
            break;
        case PlayerSkills.SkillType.fcritD1:
            SetCritDamage();
            break;
        case PlayerSkills.SkillType.fcritD2:
            SetCritDamage();
            break;
        case PlayerSkills.SkillType.faspd1:
            SetAttackSpeed();
            break;
        case PlayerSkills.SkillType.faspd2:
            SetAttackSpeed();
            break;


        case PlayerSkills.SkillType.ghp1:
            SetMaxHp();
            break;
        case PlayerSkills.SkillType.ghp2:
            SetMaxHp();
            break;
        case PlayerSkills.SkillType.gstr1:
            SetStrength();
            break;
        case PlayerSkills.SkillType.gblock1:
            SetBlock();
            break;
        case PlayerSkills.SkillType.gblock2:
            SetBlock();
            break;
        case PlayerSkills.SkillType.gfres1:
            SetInt();
            break;
        case PlayerSkills.SkillType.glres1:
            SetInt();
            break;


        case PlayerSkills.SkillType.hreg1:
            SetRegeneration();
            break;
        case PlayerSkills.SkillType.hreg2:
            SetRegeneration();
            break;
        case PlayerSkills.SkillType.hblock1:
            SetBlock();
            break;
        case PlayerSkills.SkillType.hhp1:
            SetMaxHp();
            break;
        case PlayerSkills.SkillType.hhp2:
            SetMaxHp();
            break;
        case PlayerSkills.SkillType.hblock2:
            SetBlock();
            break;
        case PlayerSkills.SkillType.hhp3:
            SetMaxHp();
            break;
        case PlayerSkills.SkillType.hhp4:
            SetMaxHp();
            break;*/
    }
    #endregion
    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    public bool CanCountAsProjectile()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.CountAsProjectile);
    }

    public bool CanUseSweep()
    {
        if (compareClass == name.Berserk)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Sweep);
        }
        else
        {
            return false;
        }
    }

    public bool Test()
    {
        if(compareClass == name.Cowboy)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Test);
        }
        else
        {
            return false;
        }
    }
   
    public bool CanUseExplosion()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Explosion);
    }
    public bool CanUseBladeVortex()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.BladeVortex);
    }
    public bool CanUseAoe()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Aoe);
    }
    public bool CanUseBladeStorm()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Bladestorm);
    }
    public bool CanUseBomber()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Bomber);
    }
    public bool CanUseLinkdash()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Linkdash);
    }
    
    public bool CanUseShuriken()
    {
        //if (compareClass == name.Ninja)
        //{
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Shuriken);
        //}
       // else
        //{
          //  return false;
        //}
    }
    public bool CanUseSmoke()
    {
        if(compareClass == name.Ninja || avoidClass == name.universal)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Smoke);
        }
        else
        {
            return false;
        }
    }

    public bool CanUseBlindstrike()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Blindstrike);
    }

    public bool CanUseRage()
    {
        if (compareClass == name.Berserk || avoidClass == name.universal)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Rage);
        }
        else
        {
            return false;
        }
    }

    public bool CanUseShield()
    {
        if (compareClass == name.Paladin || avoidClass == name.universal)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.PalaShield);
        }
        else
        {
            return false;
        }
    }

    public bool CanUseRevenge()
    {
        if (compareClass == name.Berserk || avoidClass == name.universal)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Revenge);
        }
        else
        {
            return false;
        }
    }
    

    public bool CanUseElectroCast()
    {
        //if(compareClass == name.Elementalist )
        //{
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.ElectroCast);
        //}
    }

    public bool CanUseTornado()
    {
        if(compareClass == name.Elementalist || avoidClass == name.universal)
        {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Tornado);
        }
        else
        {
            return false;
        }
    }

    public bool CanUseHolyFire()
    {
       // if (compareClass == name.Holy)
        //{
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.HolyFire);
        //}
        //else
        //{
        //    return false;
       // }
    }

    public bool CanUseFallingStars()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.FallingStars);
    }

    public bool CanUseMinion()
    {
        if (compareClass == name.Necromancer || avoidClass == name.universal)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.NecromancerMinion);
        }
        else
        {
            return false;
        }
    }

    public bool CanUseReynaEye()
    {
        if (compareClass == name.Necromancer || avoidClass == name.universal)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.ReynaEye);
        }
        else
        {
            return false;
        }
    }

    public bool CanUseEssence()
    {
        if(compareClass == name.Necromancer || avoidClass == name.universal)
       {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Essence);
       }
        else
        {
            return false;
        }
    }

    public bool CanUseTower()
    {
        if (compareClass == name.Trapper || avoidClass == name.universal)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Sweep);
        }
        else
        {
            return false;
        }
    }
    
    public bool CanUsePunish()
    {
        //if (compareClass == name.Cowboy)
        //{
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.PunishShot);
        //}
        //else
        //{
         //   return false;
        //}
    }

    public bool CanUseDynamit()
    {
        if (compareClass == name.Cowboy || avoidClass == name.universal)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Dynamit);
        }
        else
        {
            return false;
        }
    }

    public bool Heal()
    {
        if (compareClass == name.Holy || avoidClass == name.universal)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Heal);
        }
        else
        {
            return false;
        }
    }

    public bool CanUseBuff()
    {
        if (compareClass == name.Holy || avoidClass == name.universal)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Buff);
        }
        else
        {
            return false;
        }
    }

    //Special ITEMS
    public bool CanUseCurseArrow()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.CurseArrow);
    }

    public bool Fireball()
    {
        if (compareClass == name.Elementalist || avoidClass == name.universal)
        {
            return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Sweep);
        }
        else
        {
            return false;
        }
    }

    /*public bool SetMovementSpeed2()
    {
        //return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.MovementSpeed_2);
    }

    public bool SetMovementSpeed1()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.MovementSpeed_1);
    }*/

    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        Debug.Log("levelup");
        //playerSkills.AddSkillPoint();
        //effect;

        //SpawnParticleEffect();
        //Flash(new Color(1, 1, 1, 1));
    }

    /*private void SpawnParticleEffect()
    {
        Transform effect = Instantiate(pfEffect, transform);
        FunctionTimer.Create(() => Destroy(effect.gameObject), 3f);
    }

    private void Flash(Color flashColor)
    {
        materialTintColor = flashColor;
        Material.SetColor("_Tint", materialTintColor);
    }*/
}