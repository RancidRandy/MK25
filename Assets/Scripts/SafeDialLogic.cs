using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeDialLogic : MonoBehaviour
{
    [SerializeField]
    int[] dialPositions = { 0, 1, 2, 3, 4, 5, 6, 7 };

    [SerializeField]
    int[] safeCombination = new int[] { 1, 2, 3, 4, 3, 2, 3, 4, 5, 4, 5, 6, 7 };    

    [SerializeField]
    private List<int> inputSequence = new List<int>();

    private int currentIndex = 0;   

    void Start()
    {
        currentIndex = 0;
        Debug.Log($"Dial position: {currentIndex}");        
    }

    void CheckSequence()
    {
        string combinationString = safeCombination.ToString();
        string sequenceString = inputSequence.ToString();
        if (combinationString == sequenceString)
        {
            Debug.Log("Sequence completed, safe unlocked!");
        }        
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
            inputSequence.Add(currentIndex);
            CheckSequence();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentIndex++;
            if (currentIndex >= dialPositions.Length)
            {
                currentIndex = 0;
            }

            Debug.Log($"Dial position: {currentIndex}");
            inputSequence.Add(currentIndex);
            CheckSequence();
        }              
    }
}
