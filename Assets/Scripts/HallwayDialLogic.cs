using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HallwayDialLogic : MonoBehaviour
{
    [SerializeField]
    int[] dialPositions;

    private int currentIndex;
    public bool sendsPower = false;

    void Start()
    {
        currentIndex = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = dialPositions.Length - 1;
            }

            Debug.Log($"Dial position: {currentIndex}");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentIndex++;
            if (currentIndex >= dialPositions.Length)
            {
                currentIndex = 0;
            }

            Debug.Log($"Dial position: {currentIndex}");
        }

        if (currentIndex == 3)
        {
            sendsPower = true;
        }
    }
}
