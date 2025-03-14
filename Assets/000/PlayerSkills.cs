using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills
{
    public event EventHandler OnSkillPointsChanged;
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs { public SkillType skillType; }
    public enum SkillType
    {
        None,
        BladeVortex,
        Sweep,
        Explosion,
        Bladestorm,
        Bomber,
        Aoe,
        Linkdash,
        Test,

        Rage,
        Revenge,
        warcry,
        banner,
        bladesedge,
        shatter,

        Shuriken,
        Smoke,
        Blindstrike,

        PalaShield,
        holyarea,
        defstance,

        elebuff,
        ElectroCast,
        Tornado,
        fireball,

        ReynaEye,
        NecromancerMinion,
        Essence,
        blindingeye,

        focus,
        ricochet,
        windshape,

        stealth,
        turrets,
        oilypoison,
        poisonstack,

        bulletdance,
        snipermarks,
        PunishShot,

        Heal,
        Buff,
        Dynamit,

        HolyFire,
        FallingStars,

        //Stun,


        //ProjectileCanHit,
        //ProjectileCanSpell,
            
        //HitCanProjectile,
        //HitCanSpell,

        //SpellCanProjectile,
        //SpellCanHit,

        CountAsHit,
        CountAsProjectile,
        CountAsSpell,

        CurseArrow,

        str,
    }

    public List<SkillType> unlockedSkillTypeList;

   /* public List<SkillType> berserkSkills;
    public List<SkillType> ninjaSkills;
    public List<SkillType> paladinSkills;

    public List<SkillType> hunterSkills;
    public List<SkillType> trapperSkills;
    public List<SkillType> cowboySkills;

    public List<SkillType> elementalistSkills;
    public List<SkillType> necromancerSkills;
    public List<SkillType> holySkills;*/


    public List<SkillType> unlockedItemSkillTypeList; //maybe can delete not sure rn ...

    public List<Sprite> unlockedImage; // HERE ABILITY ICONS
    private int skillPoints;

    private LevelSystem levelSystem;
    public LevelWindow levelWindow;


    public PlayerSkills()
    {
        unlockedSkillTypeList = new List<SkillType>();
        unlockedImage = new List<Sprite>();
    }   

    /*public void AddSkillPoint()
    {
        skillPoints++;
        OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetSkillPoints()
    {
        return skillPoints;
        Debug.Log("sp" + skillPoints);
    }*/

    private void UnlockSkill(SkillType skillType, Sprite sprite)
    {
        if (!IsSkillUnlocked(skillType))
        {
            unlockedSkillTypeList.Add(skillType);
            unlockedImage.Add(sprite);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
        }
    }

    //Check this part to make upgrade Nodes
    /*public int UpgradeAnount(Skills skillint)
    {
        return skillint.amount;
    }*/

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillTypeList.Contains(skillType);
    }

    #region itemUnlock_Cloned
    /*
    private void UnlockItemSkill(SkillType skillType, Sprite sprite)
    {
        if (!IsItemSkillUnlocked(skillType))
        {
            unlockedItemSkillTypeList.Add(skillType);
            unlockedImage.Add(sprite);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
        }
    }

    public bool IsItemSkillUnlocked(SkillType skillType)
    {
        return unlockedItemSkillTypeList.Contains(skillType);
    }

    public bool CanUnlockItem(SkillType skillType)
    {
        SkillType skillRequirement = GetSkillRequirement(skillType);

        if (skillRequirement != SkillType.None)
        {
            if (IsSkillUnlocked(skillRequirement))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public bool TryUnlockItemSkill(SkillType skillType, Sprite sprite)
    {
        if (CanUnlock(skillType))
        {
            //if(skillPoints > 0){}
            UnlockItemSkill(skillType, sprite);
            return true;
        }
        //else {return false;}
        else
        {
            return false;
        }
    }
    */
    #endregion

    public void ResetSkillsAll()
    {
        unlockedSkillTypeList.Clear();

        //OnSkillRemove...
    }  

    public bool CanUnlock(SkillType skillType)
    {
        SkillType skillRequirement = GetSkillRequirement(skillType);

        if (skillRequirement != SkillType.None)
        {
            if (IsSkillUnlocked(skillRequirement))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public SkillType GetSkillRequirement(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.CurseArrow: return SkillType.None;
            case SkillType.str: return SkillType.None;

            case SkillType.Bomber: return SkillType.None;
            case SkillType.Aoe: return SkillType.None;
            case SkillType.Explosion: return SkillType.None;
            case SkillType.Bladestorm: return SkillType.None;
            case SkillType.BladeVortex: return SkillType.None;
            case SkillType.Sweep: return SkillType.None;
            case SkillType.Linkdash: return SkillType.None;
            case SkillType.CountAsHit: return SkillType.None;
            case SkillType.CountAsSpell: return SkillType.None;
            case SkillType.CountAsProjectile: return SkillType.None;

        }
        return SkillType.None;
    } 

    public bool TryUnlockSkill(SkillType skillType, Sprite sprite)
    {
        levelWindow = GameObject.Find("CanvasUI").GetComponentInChildren<LevelWindow>();
        skillPoints = levelWindow.levelSystem.points;
        //  skillPoints = levelSystem.GetPoints(); 
        Debug.Log("skilltest" + skillPoints);
        if (CanUnlock(skillType))
            {
            if(skillPoints > 0)
            {
                //LevelSystem.GetPoints();
                levelWindow.levelSystem.points--;
                UnlockSkill(skillType, sprite); //twice
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }  
    }

    public bool TryUnlockSkillWithRequierement(SkillType skillType, Sprite sprite)
    {
        if (CanUnlock(skillType))
        {
            if (skillPoints > 0)
            {
                UnlockSkill(skillType, sprite);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
