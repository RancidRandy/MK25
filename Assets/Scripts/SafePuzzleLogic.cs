using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafePuzzleLogic : MonoBehaviour
{
    [SerializeField]
    int[] dialPositions = { 0, 1, 2, 3, 4, 5, 6, 7 };

    [SerializeField]
    int[] safeCombination = new int[] { 0, 1, 2, 3, 4, 3, 2, 3, 4, 5, 4, 5, 6, 7 };
    
    private Queue<int> inputSequenceQueue = new Queue<int>();
    private int currentIndex = 0;

    void Start()
    {
        currentIndex = 0;
        // Optionally, prime the queue with initial values
        // For a true rolling window, this might be all zeros or a starting value
        // but for a combination, it's better to start empty
        inputSequenceQueue.Enqueue(currentIndex);
        Debug.Log($"Dial position: {currentIndex}");
    }

    void CheckSequence()
    {
        if (inputSequenceQueue.Count == safeCombination.Length)
        {
            int[] inputArray = inputSequenceQueue.ToArray();
            bool sequencesMatch = true;
            for (int i = 0; i < safeCombination.Length; i++)
            {
                if (safeCombination[i] != inputArray[i])
                {
                    sequencesMatch = false;
                    break;
                }
            }
            if (sequencesMatch)
            {
                Debug.Log("COMPLETED, DONE. YAY!");
                
                // Insert safe unlocking stuff, disable scripts relevant to the puzzle etc.
            }
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
            AddInputAndCheck();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentIndex++;
            if (currentIndex >= dialPositions.Length)
            {
                currentIndex = 0;
            }
            Debug.Log($"Dial position: {currentIndex}");
            AddInputAndCheck();
        }
    }

    void AddInputAndCheck()
    {
        inputSequenceQueue.Enqueue(currentIndex);
        if (inputSequenceQueue.Count > safeCombination.Length)
        {
            inputSequenceQueue.Dequeue();
        }
        CheckSequence();
    }
}
