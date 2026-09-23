using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableTemplate : MonoBehaviour, IInteractable
{
    public virtual void Interact()
    {
        //What does the object do upon interaction? Insert here.
        Debug.Log("Boop!");
        Destroy(gameObject);
    }
}
