using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


public class ContainerUI : MonoBehaviour
{

    public Transform itemsParent;

    public TextMeshProUGUI containerName;

    public ContainerScript containerScript;

    public GameObject containerUI;

    ContainerSlot[] slots;

    public bool inUI = false;

    public void Init()
    {
        print(containerScript.gameObject);
        containerScript.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<ContainerSlot>();

    }

    // Update is called once per frame

    private void UpdateUI()
    {
        //Debug.Log("UPDATING UI");
        for (var i = 0; i < slots.Length; i++)
        {
            if (i < containerScript.items.Count)
            {
                slots[i].AddItem(containerScript.items.ElementAt(i).Key);
                slots[i].amount.text = containerScript.items.ElementAt(i).Value.ToString();
                slots[i].conScript = containerScript;
            }
            else
            {
                slots[i].ClearSlot();
                slots[i].amount.text = "";
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        inUI = false;
    }
}
