using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelWindow : MonoBehaviour, IDataPersistance
{
    private TextMeshProUGUI levelText;
    public TextMeshProUGUI pointText;
    private Image experienceBarImage;
    public LevelSystem levelSystem;

    private void Awake()
    {
        //pointText = transform.Find("pointTxt").GetComponent<TextMeshProUGUI>();
        levelText = transform.Find("lvText").GetComponent<TextMeshProUGUI>();
        experienceBarImage = transform.Find("experienceBar").Find("bar").GetComponent<Image>();

        //transform.Find("experienceButton").GetComponent<Button_UI>.ClickFunc = () => levelSystem.AddExperience(5);
        
        //levelSystem = 
    }

    public void LoadData(GameData data)
    {
        levelSystem.level = data.level;
        SetLevelNumber(data.level);
        Debug.Log("loadlevel" + data.level);
    }

    public void SaveData(ref GameData data)
    {
        data.level = levelSystem.GetLevelNumber();
        Debug.Log("level " + data.level);
    }

    public void xp(int monsterLevel)
    {
        levelSystem.AddExperience(monsterLevel);
        //int test = levelSystem.GetLevelNumber();
        //int t = levelSystem.GetPoints();
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        Debug.Log("levelLV" + levelSystem.level);
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBarImage.fillAmount = experienceNormalized;
        levelText.text = "LEVEL " + levelSystem.level;
        pointText.text = "  " + levelSystem.points;
    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "LEVEL " + (levelNumber + 1);
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        //Set the LevelSystem object
        this.levelSystem = levelSystem; 

        //Update the starting values
        SetLevelNumber(levelSystem.GetLevelNumber()); 
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        //Subscribe to the changed events
        levelSystem.OnExperienceChanged += LevelSystem_OnExperiencedChanged; 
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        // Level changed, update Text
        SetLevelNumber(levelSystem.GetLevelNumber()); 

    }
    private void LevelSystem_OnExperiencedChanged(object sender, System.EventArgs e)
    {
        //Experience changed, update bar size
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }
}
