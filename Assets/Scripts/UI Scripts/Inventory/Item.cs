using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public int itemAmount = 1;

    public virtual void Use()
    {
        //Use the item
        //Something might happen

        Inventory.instance.Remove(this);
        Debug.Log("Using " + name);
    }
}
