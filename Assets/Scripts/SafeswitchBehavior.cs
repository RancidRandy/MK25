using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeswitchBehavior : MonoBehaviour, IInteractable
{
    //public GameObject[] lights;
    public bool isActive = true;

    public void Interact()
    {
        //Debug.Log("Boop!");

        if (isActive == true)
        {
            isActive = false;
            Debug.Log("Down");
        }
        else
        {
            isActive = true;
            Debug.Log("Up");
        }
        
    }
}