using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public Dictionary<Item, int> items = new Dictionary<Item, int>();

    public InventoryUI invUI;

    void Start()
    {
        invUI.Init();
    }

    public void AddItemFromChest(Item item)
    {
        Add(item);
    }
    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            // check for available space
            if (items.Count >= space)
            {
                Debug.Log("Not enough room");
                return false;
            }

            if (items.ContainsKey(item))
                items[item]++;
            else
            {
                items.Add(item, 1);
                print(item.ToString());
            }
                


            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }


    public void Remove(Item item)
    {
        if (items[item] > 1)
            items[item]--;
        else
            items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
