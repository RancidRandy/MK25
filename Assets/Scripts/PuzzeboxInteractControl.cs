using UnityEngine;

public class PuzzleboxInteractControl : MonoBehaviour , IInteractable
{
    public Camera mainCamera;
    public Camera puzzleCamera;
    public PlayerMovement movementControl;
    public MouseLook aimControl;    
    public SafePuzzleLogic puzzleScript;

    public void Interact()
    {        
        if (puzzleScript.enabled == false)
        {
            mainCamera.enabled = false;
            movementControl.enabled = false;
            aimControl.enabled = false;
            puzzleCamera.enabled = true;
            puzzleScript.enabled = true;
        }
        else
        {
            mainCamera.enabled = true;
            movementControl.enabled = true;
            aimControl.enabled = true;
            puzzleCamera.enabled = false;
            puzzleScript.enabled = false;
        }
    }
}
