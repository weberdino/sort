using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;

    public bool Test;

   // public TextMeshProUGUI text;

    SetTarget setTarget;
    //public GameObject parent;

    float visibleTime = 5;
    float lastMadeVisibleTime;

    Transform ui;
    Image healthSlider;
    Transform cam;
    CharacterStats myStats;
    void Start()      
    {
       // parent = GameObject.Find("BomberParent");

        cam = Camera.main.transform;   

        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if(c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(true); // eigentlich false
                break;
            }
        }
        myStats = GetComponent<CharacterStats>(); //.OnHealthChanged += OnHealthChanged;
        //GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void Awake()
    {
        //setTarget = parent.GetComponentInChildren<SetTarget>();
    }
    
    private void OnDestroy()
    {
        Destroy(ui.gameObject);
    }

    //void OnHealthChanged() <- nicht sicher ob das so korrekt ist. vlt n

    void Update(/*int maxHealth, int currentHealth*/)
    {
        if (ui != null)
        {
            if (myStats.currentHealth <= 0)
            {
                if (!Test)
                {
                    Destroy(ui.gameObject);
                    //removeList();
                   // Destroy(gameObject);
                }
            }

            ui.gameObject.SetActive(true);
            lastMadeVisibleTime = Time.time;

            float healthPercent =  (float) myStats.currentHealth / myStats.maxHealth.GetValue();
            healthSlider.fillAmount = healthPercent;
        }
    }

    public void DestroyUI()
    {
       // Destroy(ui.gameObject);
    }

    void removeList()
    {
       // setTarget = GetComponent<SetTarget>();
        if (setTarget != null)
        {
            Debug.Log(this.gameObject);
            setTarget.inRangeMonsters.Remove(this.gameObject);
        }
    }

    void LateUpdate ()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;

            /*if (Time.time - lastMadeVisibleTime > visibleTime)
            {
                ui.gameObject.SetActive(false);
            }*/
        }
    }
}
