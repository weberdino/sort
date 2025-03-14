using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSystem
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    //private static readonly int[] experiencePerLevel = new[] { 100, 120, 140, 200 };
    public int level;
    public int points;
    public int experience;
    private int experienceToNextLevel;

    public TextMeshProUGUI text;
    public TextMeshProUGUI txt;



    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
    }

    public void AddExperience(int amount)
    {
        if (!IsMaxLevel())
        {
            Debug.Log(experience);
            experience += amount;
            Debug.Log(experience);
            while (!IsMaxLevel() && experience >= GetExperienceToNextLevel(level))
            {
                //Enough experience to level up
                experience -= GetExperienceToNextLevel(level);
                level++;
                points++;
                
                // txt.text = points.ToString();
                // text.text = level.ToString();
                if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
            }
            if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public int GetPoints()
    {
        return points;
    }

    public float GetExperienceNormalized()
    {
        if (IsMaxLevel())
        {
            return 1f;
        }
        else
        {
            return (float)experience / GetExperienceToNextLevel(level);
        }

    }

    public int GetExperienceToNextLevel(int level)
    {
        return level * 100 * level / 10;
        /*
         if (level < experiencePerLevel.Length){
        return experiencePerLevel[level];
        }
        else
        {
        Debug.LogErrore("Level invalied" + level);
        return 100; // never reaches this part.
        }
        */
    }

    public bool IsMaxLevel()
    {
        return IsMaxLevel(level);
    }
    public bool IsMaxLevel(int level)
    {
        return level == 40;
        // return level == experiencePerLevel.Length - 1;
    }
}
