using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int cash;

    public bool reachedHideout;

    public Vector3 playerPos;
    public int scene;

    public Equipment[] savedEquipment;
    public List<Equipment> items = new List<Equipment>();
    public List<Equipment> accessoires = new List<Equipment>();
    public List<Equipment> armor = new List<Equipment>();
    public List<PlayerSkills.SkillType> unlockedSkills = new List<PlayerSkills.SkillType>();
    public List<Sprite> unlockedImage = new List<Sprite>();
    //public Dictionary<int, bool> equip;
    public GameData()
    {
        cash = 0;

        reachedHideout = false;

        scene = 0;
        this.level = 0;
        playerPos = Vector3.zero;
        savedEquipment = null;

        items = new List<Equipment>();
        accessoires = new List<Equipment>();
        armor = new List<Equipment>();

        unlockedSkills = null; // new List<PlayerSkills.SkillType>();
        unlockedImage = null;
        //equip = new Dictionary<int, bool>();

        //items/inventory
        //skilltree unlocks
    }
}
