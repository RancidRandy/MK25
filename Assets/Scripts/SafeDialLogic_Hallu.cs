using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeDialLogic_Hallu : MonoBehaviour
{    
    [SerializeField]
    int[] safeCombination = { 1, 2, 3, 4, 3, 2, 3, 4, 5, 4, 5, 6, 7 };
    private List<int> inputSequence = new List<int>();
    private int currentIndex = 0;
    //[SerializeField]
    private bool combinationCompleted = false;
    bool isCorrect;
    

    void Start()
    {
        currentIndex = 0;
        Debug.Log($"Dial position: {currentIndex}");
    }

    void Update()
    {
        if (combinationCompleted) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            RotateDial(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            RotateDial(1);
        }
        CheckCombination();
    }

    private void RotateDial(int direction)
    {
        currentIndex += direction;

        if (currentIndex < 0)
        {
            currentIndex = 7;
        }
        else if (currentIndex > 7)
        {
            currentIndex = 0;
        }

        Debug.Log($"Dial position: {currentIndex}");
        inputSequence.Add(currentIndex);

        if (currentIndex == 0 && inputSequence.Count > 1)
        {
            Debug.Log("0 equals RESET");
            inputSequence.Clear();
            currentIndex = 0;
        }
    }


    private void CheckCombination()
    {
        if (inputSequence.Count == safeCombination.Length)
        {
            isCorrect = true;
            for (int i = 0; i < inputSequence.Count; i++)
            {
                if (inputSequence[i] != safeCombination[i])
                {
                    Debug.Log("Sequence failed, go again!");
                }
            }
            if (isCorrect)
            {
                combinationCompleted = true;
                Debug.Log("Sequence completed");
            }
        }

        else if (inputSequence.Count > 0 && inputSequence.Count <= safeCombination.Length)
        {
            if (inputSequence[inputSequence.Count - 1] != safeCombination[inputSequence.Count - 1])
            {
                Debug.Log("Sequence incorrect, please reset!");
                //inputSequence.Clear();
            }
        }
        else if (inputSequence.Count > safeCombination.Length)
        {
            Debug.Log("Sequence too long, thus incorrect, so try again!");
        }
    }
}