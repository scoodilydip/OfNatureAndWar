using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> itemsInContainer = new List<Item>();
    public Dictionary<Item, int> items = new Dictionary<Item, int>();

    public ContainerUI conUI;

    public bool itemsHaveBeenPutIn = false;

    public void Init()
    {
        conUI.Init();

        if (!itemsHaveBeenPutIn)
        {
            putItemsInInventory();
            itemsHaveBeenPutIn = true;
        }
    }

    public bool putItemsInInventory()
    {
        foreach (Item item in itemsInContainer)
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
                    //print(item.ToString());
                }
                    
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }
        }
        return true;
        
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
                items.Add(item, 1);
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
