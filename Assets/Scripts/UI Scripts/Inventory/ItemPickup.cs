using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public GameObject player;

    public override void Interact()
    {
        base.Interact();

        print("Interacting with " + transform.name);
        PickUp();

        PlayerSingleton._instance.GetComponent<Movement>().RemoveFocus();
    }

    void PickUp()
    {
        print("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
            Destroy(gameObject);
    }
}