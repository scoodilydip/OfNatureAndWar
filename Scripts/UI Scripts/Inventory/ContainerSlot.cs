using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContainerSlot : MonoBehaviour
{
    public Image icon;
    //public Button removeButton;
    public TextMeshProUGUI amount;
    public ContainerUI conUI;
    public ContainerScript conScript;

    Item item;


    public void AddItem(Item newItem)
    {
        item = newItem;

        //print(item.name);
        icon.sprite = item.icon;
        icon.GetComponent<Image>().enabled = true;
        amount.GetComponent<TMP_Text>().enabled = true;
        //removeButton.interactable = true;


    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        //removeButton.interactable = false;
        amount.GetComponent<TMP_Text>().enabled = false;
    }

    public void OnClickItem()
    {
        if (item != null)
        {
            Inventory.instance.Add(item);
            conScript.Remove(item);
        }
        
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
