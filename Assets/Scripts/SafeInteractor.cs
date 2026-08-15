using UnityEngine;

public class SafeInteractor : MonoBehaviour, IInteractable
{
    public Camera mainCamera;
    public Camera puzzleCamera;
    public PlayerMovement movementControl;
    public MouseLook aimControl;
    public HallwayDialLogic powerSource;
    public SafeswitchBehavior powerSwitch;
    public SafePuzzleLogic puzzleScript;

    //private void Start()
    //{

    //}

    public void Interact()
    {
        if (powerSource.sendsPower == true && powerSwitch.isActive == true)
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
        else
        {
            Debug.Log("Not powered!");
        }
    }
}
