using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightswitchBehavior : MonoBehaviour, IInteractable
{
    public GameObject[] lights;

    public void Interact()
    {
        Debug.Log("Boop!");

        foreach (GameObject light in lights)
        {
            light.SetActive(!light.activeSelf);
        }
    }
}