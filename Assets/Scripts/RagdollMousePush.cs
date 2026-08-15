using UnityEngine;

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem; // New Input System namespace
#endif

public class RagdollMousePush : MonoBehaviour
{
#pragma warning disable CS0414
    [TextArea]
    [SerializeField]
    private string description = "This script applies a force to a selected Rigidbody in the direction of the mouse cursor. "
                                + "Simply attach this script to a GameObject, assign the Rigidbody to 'Target Rigidbody', "
                                + "and adjust the force magnitude as needed. Press the left mouse button to apply the force. "
                                + "This script works with both the old Input Manager and the new Input System.";
#pragma warning restore CS0414

    [Header("Ragdoll Settings")]
    [Tooltip("Select the Rigidbody part of the ragdoll to apply the force.")]
    public Rigidbody targetRigidbody;

    [Header("Force Settings")]
    [Tooltip("The magnitude of the force to apply.")]
    public float forceMagnitude = 100f;

    [Tooltip("Key to trigger the force push.")]
    public KeyCode pushKey = KeyCode.Mouse0; // Default to left mouse button

    private Camera mainCamera;

    private void Start()
    {
        // Cache the main camera reference
        mainCamera = Camera.main;
    }

    private void Update()
    { // Check for input based on the selected input system
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        // New Input System: Check if the left mouse button is pressed
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ApplyForce();
        }
#else
        // Old Input Manager: Check if the push key is pressed
        if (Input.GetKeyDown(pushKey))
        {
            ApplyForce();
        }
#endif
    }


    // Applies a force to the selected Rigidbody in the direction of the mouse cursor.

    private void ApplyForce()
    {
        if (targetRigidbody != null)
        {
            // Calculate the direction from the Rigidbody to the mouse cursor
            Ray ray = mainCamera.ScreenPointToRay(GetMousePosition());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 forceDirection = (hit.point - targetRigidbody.position).normalized;

                // Apply the force in the calculated direction and magnitude
                Vector3 force = forceDirection * forceMagnitude;
                targetRigidbody.AddForce(force, ForceMode.Impulse);

              //  Debug.Log($"Applied force {force} to {targetRigidbody.name} in the direction of the mouse cursor.");
            }
            else
            {
                Debug.LogWarning("Mouse cursor did not hit any object. No force applied.");
            }
        }
        else
        {
            Debug.LogWarning("No Rigidbody selected. Please assign a target Rigidbody in the Inspector.");
        }
    }

    // This method retrieves the mouse position based on the selected input system.
    private Vector3 GetMousePosition()
    {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        // New Input System: Get mouse position
        return Mouse.current != null ? Mouse.current.position.ReadValue() : Vector3.zero; // The text here is grey color because it is not used in the code, but it is used in the new input system.
#else
        // Old Input Manager: Get mouse position
        return Input.mousePosition;
#endif
    }
}
