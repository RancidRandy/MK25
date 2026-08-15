using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialBehavior : MonoBehaviour
{
    [SerializeField]
    int[] dialPositions;

    private int currentIndex = 0;

    
    //void Start()
    //{
        
    //}
    
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
    }
}
