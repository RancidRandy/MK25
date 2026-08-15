using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange;
    // Define a small offset distance
    public float rayStartOffset = 0.1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 1. Calculate the new, slightly offset start position
            Vector3 rayStartPos = interactorSource.position + interactorSource.forward * rayStartOffset;

            // 2. Create the Ray using the offset position
            Ray r = new Ray(rayStartPos, interactorSource.forward);

            // 3. Adjust the interactRange to account for the offset
            // We subtract the offset so the total distance checked is still 'interactRange'
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange - rayStartOffset))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }

            // Optional Debug Line to visualize the ray's start and path
            Debug.DrawRay(rayStartPos, interactorSource.forward * (interactRange - rayStartOffset), Color.yellow, 1f);
        }
    }
}