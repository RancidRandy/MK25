using UnityEngine;

public class Item_Test01 : InteractableItem
{
    public override void Interact()
    {
        Debug.Log("Boop! Interacted with " + itemName);
        InventorySystem.Instance.OnAdd(this); // Example of how you'd call the add function        
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
    }
}