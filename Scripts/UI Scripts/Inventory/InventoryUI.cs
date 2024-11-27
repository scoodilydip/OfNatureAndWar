using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    Inventory inventory;

    public GameObject inventoryUI;

    InventorySlot[] slots;
    public void Init()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            print("EPIC BOIEH");

        }
    }

    private void UpdateUI()
    {
        Debug.Log("UPDATING UI");
        for (var i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items.ElementAt(i).Key);
                slots[i].amount.text = inventory.items.ElementAt(i).Value.ToString();
            }
            else
            {
                slots[i].ClearSlot();
                slots[i].amount.text = "";
            }
        }
    }
}
