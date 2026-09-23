using UnityEngine;

public abstract class InteractableItem : InteractableTemplate, IInteractable
{
    [Header("Item Data")]
    public string itemName;
    //[TextArea(3, 5)] // Gives you a larger text box in the inspector
    public string itemDescription;
    public Sprite itemMenuIcon;
    [SerializeField] InteractableTemplate itemUseTarget;
    //public GameObject itemModelPrefab; // The 3D model to show in the UI
    public bool canUse = false;

    public override void Interact()
    {
        Debug.Log("Boop! Interacted with " + itemName);                
        //InventorySystem.Instance.OnAdd(this); // Example of how you'd call the add function
    }

    public virtual void InventoryUse()
    {        
        InventorySystem inventory = FindFirstObjectByType<InventorySystem>();
        if (itemUseTarget != null)
        {
            if (itemUseTarget == inventory.targetObject)
            {
                Debug.Log($"Used {itemName} on {inventory.targetObject}");
                canUse = true;
                // Unlock stuff
            }
            else
            {
                //Debug.Log("That doesn't seem to fit.");
                canUse = false;
            }
        }
        else
        { 
            Debug.Log("Consumed " + itemName);
            canUse = true;
        }        
    }
}