using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    [Header("Inventory Setup")]
    public int itemCap = 8;
    public InteractableItem[] items; // Holds the actual item data
    public GameObject inventoryUIPanel;
    public Image[] slotUIImages;     // The 8 Image components in your UI grid    

    [Header("Highlight UI Displays")]
    public TextMeshProUGUI selectedName;
    public TextMeshProUGUI selectedDescription;
    //public Transform modelViewerContainer; // An empty GameObject in your UI to hold the spawned 3D model
    //private GameObject currentSelectedModel; // Tracks the currently displayed 3D model

    [Header("Interact Data")]
    [SerializeField] MouseLook mouseLook;
    [SerializeField] Transform interactorSource;
    [SerializeField] float interactRange;
    public InteractableTemplate targetObject;


    private void Awake()
    {
        // Check if an instance already exists, and destroy the duplicate if so
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }        
        Instance = this;

        if (items.Length < itemCap)
        { 
            // Initialize the array to hold exactly 8 items
            items = new InteractableItem[itemCap];
        }
        UpdateUI();
        ExitInventory();
    }

    // Returns true if added, false if inventory is full
    public bool OnAdd(InteractableItem itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                UpdateUI();
                return true;
            }
        }

        Debug.Log("Inventory is full! No space left.");
        return false;
    }

    public void OpenInventory()
    {
        Ray r = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out InteractableTemplate interactObj))
            {
                targetObject = interactObj;
            }
        }
        mouseLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        inventoryUIPanel.SetActive(true);
        // Gamepad: Controller settings switch to UI
    }

    public void ExitInventory()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.enabled = true;
        inventoryUIPanel.SetActive(false);
        // Gamepad: Controller settings switch to Gameplay
        if (targetObject != null)
        {
            targetObject = null;
        }
    }


    public void OnRemove(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < items.Length)
        {
            items[slotIndex] = null;
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slotUIImages.Length; i++)
        {
            if (items[i] != null)
            {
                // Slot has an item: assign the sprite and make it visible
                slotUIImages[i].sprite = items[i].itemMenuIcon;
                slotUIImages[i].enabled = true;
            }
            else
            {
                // Slot is empty: hide the icon
                slotUIImages[i].sprite = null;
                slotUIImages[i].enabled = false;
            }
        }
    }

    // Called when the mouse hovers over a specific UI grid slot
    public void OnHighlight(int slotIndex)
    {
        InteractableItem highlightedItem = items[slotIndex];

        // If hovering over an empty slot, clear the UI
        if (highlightedItem == null)
        {
            //selectedName.text = " ";
            //selectedDescription.text = " ";
            Debug.Log($"{slotIndex} slot is empty");
            //if (currentSelectedModel != null) Destroy(currentSelectedModel);
            return;
        }

        // 1. Update Text
        selectedName.text = highlightedItem.itemName;
        selectedDescription.text = highlightedItem.itemDescription;
        //Debug.Log(selectedName.text);
        //Debug.Log(selectedDescription.text);

        //// 2. Update 3D Model Display
        //if (currentSelectedModel != null)
        //{
        //    Destroy(currentSelectedModel);
        //}

        //if (highlightedItem.itemModelPrefab != null)
        //{
        //    // Spawn the item's 3D model inside the designated UI container
        //    currentSelectedModel = Instantiate(highlightedItem.itemModelPrefab, modelViewerContainer);

        //    // Note: To render 3D models properly in a UI canvas, you usually need 
        //    // to adjust their layer to "UI" or use a Render Texture setup.
        //}
    }

    public void OnUseClick(int slotIndex)
    { 
        InteractableItem useItem = items[slotIndex];
        useItem.InventoryUse();
        if (useItem.canUse)
        {
            OnRemove(slotIndex);
        }
        else
        {
            Debug.Log("That don't fit!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryUIPanel.activeSelf == false)
            {
                OpenInventory();
            }
            else
            { 
                ExitInventory();
            }
        }
    }
}