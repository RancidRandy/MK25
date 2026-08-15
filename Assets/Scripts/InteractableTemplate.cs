using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTemplate : MonoBehaviour, IInteractable
{    
    public void Interact()
    {
        //What does the object do upon interaction? Insert here.
        Debug.Log("Boop!");        
    }
}
