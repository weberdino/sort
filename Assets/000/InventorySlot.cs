using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;
    public TextMeshProUGUI infoText; //stats
    public Button removeButton;
    public Button setActiveButton;
  //  public Text amount;

    [Header("Crafting")]
    public Image craftImage;
    //public Crafting craft;

    int up;

   // public FuseItems fuse;

    //public GameObject parent;
    //public GameObject parent1;
    //GameObject stabButton;
    //GameObject slashButton;

    Equipment item;
    //Crafting table;

    //List<int> modifiers = new List<int>();
    public void AddItem (Equipment newItem)
    {
        item = newItem;

        //tooltipInfos ttInfo = GetComponent<tooltipInfos>();
        //ttInfo.active = true;
        //ttInfo.item = item;

        Debug.Log(text.text + " " + item.name);

        text.text = item.name;
        //infoText.text = item.armorModifier.ToString();
        icon.sprite = item.icon;
        icon.enabled = true;
       // removeButton.interactable = true;
       // setActiveButton.interactable = true;

        textstat();

        
    }

    public void ClearSlot()
    {
        item = null;

       // tooltipInfos ttInfo = GetComponent<tooltipInfos>();
       // ttInfo.active = false;

        text.text = null;
        icon.sprite = null;
        icon.enabled = false;
       // removeButton.interactable = false;
       // setActiveButton.interactable = false;

        textclear();
    }

    public void OnRemoveButton ()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem ()
    {
        //slashButton = GameObject.Find("SLASHButton");
        //stabButton = GameObject.Find("STABButton");

        //parent.transform.GetChild(0).gameObject.SetActive(true);
        //parent1.transform.GetChild(0).gameObject.SetActive(true);

        if (item != null)
        {
            item.Use();
        }        
    }

    /*public void AddCraft ()
    {
        if(item != null)
        {
            //item.UseCraft(); not necessary (does not exist)

            craftImage = GameObject.Find("Crafting").transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>();
            //craft = GameObject.Find("Crafting").GetComponent<Crafting>();

           // craft.item = item;
            craftImage.sprite = item.icon;
            item.RemoveFromInventory();
        }
    }*/

   /* public void AddFuse()
    {
        if (!item.crafted)
        {
            if (item != null)
            {
                if (!fuse.full)
                {
                    if (fuse.holdsItem)
                    {
                        fuse.item2 = item;

                        fuse.full = true;

                        //fuse.img2.sprite = item.icon;
                        item.RemoveFromInventory();
                    }
                    else
                    {
                        fuse.item = item;

                        //fuse.img.sprite = item.icon;
                        item.RemoveFromInventory();
                    }
                }
            }
        }
        else
        {
            Debug.Log("cant use crafted items");
        }

    }*/

    void textstat()
    {       
        textclear();
        up = -1;
        foreach(int modifiers in item.modifiers)
        {
            /*for (int i = 0; i == item.modifiers.Count; i++)
            {
                 //if (modifiers = )
                {
                    //string text = item.names[modifiers];
                }
            }*/
            up += 1;
            if (modifiers > 0)
            {
                infoText.text += item.names[up] + ": " + modifiers.ToString() + "\n";
            }
        }
        //item.modifiers[0];
    }

    void textclear()
    {
        //infoText.text = " ";
    }

    private void Update()
    {
        
    }
}
