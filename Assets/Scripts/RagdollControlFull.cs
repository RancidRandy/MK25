using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem; // New Input System namespace
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class RagdollControlFull : MonoBehaviour
{

#pragma warning disable CS0414
    [TextArea]
    [SerializeField]
    private string description = "This script controls the ragdoll behavior of the character. "
                                + "It toggles between animated and ragdoll states, resets the character "
                                + "to the spawn point, and ensures the character follows the spawn point "
                                + "in edit mode.";
#pragma warning restore CS0414

    [Header("References")]
    [SerializeField] private Animator animator; // Animator component of the character
    [SerializeField] private Transform spawnPoint; // Reference to the spawn point in the scene

    [Header("Ragdoll Components")]
    public Rigidbody[] ragdollBodies; // All Rigidbody components in the ragdoll
    private Collider[] ragdollColliders; // All Collider components in the ragdoll

    [Header("State")]
    private bool isRagdollActive = false; // Tracks whether the ragdoll is active

    private void Start()
    {
        // Ensure the character starts at the spawn point
        if (spawnPoint != null)
        {
            transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        }

        // Collect all Rigidbodies and Colliders in the ragdoll
        CollectRagdollComponents();

        // Initially disable all ragdoll components
        SetRagdollState(false);
    }

    private void Update()
    {
        // Handle input for toggling ragdoll state
        if (Application.isPlaying && IsToggleRagdollKeyPressed())
        {
            if (isRagdollActive)
            {
                // Deactivate ragdoll and reset to spawn point
                SetRagdollState(false);
                ResetToSpawnPoint();
            }
            else
            {
                // Activate ragdoll
                SetRagdollState(true);
            }
        }

        // Update position in edit mode if the spawn point is moved
        if (!Application.isPlaying && spawnPoint != null)
        {
            transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        }
    }

    // Checks if the toggle ragdoll key (space) is pressed
    private bool IsToggleRagdollKeyPressed()
    {
        #if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
        #elif ENABLE_LEGACY_INPUT_MANAGER
        return Input.GetKeyDown(KeyCode.Space);
        #else
        return false; // Fallback if no input system is enabled
        #endif
    }

    // Collects all Rigidbody and Collider components in the ragdoll.
    // This runs in both play mode and edit mode.
    private void CollectRagdollComponents()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
    }

    // Enables or disables the ragdoll state.
    // True to activate ragdoll, false to deactivate.
    public void SetRagdollState(bool state)
    {
        Debug.Log($"Setting Ragdoll State to: {state}");

        // Always enable colliders - just control their physics response
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true; // Keep colliders always enabled
            col.isTrigger = !state; // Make colliders triggers when not in ragdoll
        }

        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
            // For better collision detection
            rb.collisionDetectionMode = state ?
                CollisionDetectionMode.ContinuousDynamic :
                CollisionDetectionMode.Discrete;
        }

        isRagdollActive = state;
        animator.enabled = !state;

        // Wake up rigidbodies
        if (state)
        {
            foreach (Rigidbody rb in ragdollBodies)
            {
                rb.WakeUp();
            }
        }
    }

    // Resets the character to the spawn point.

    private void ResetToSpawnPoint()
    {
        if (spawnPoint != null)
        {
            transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Automatically collect ragdoll components in edit mode
        if (!Application.isPlaying)
        {
            CollectRagdollComponents();

            // Update position to match spawn point in edit mode
            if (spawnPoint != null)
            {
                transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
#endif
}
