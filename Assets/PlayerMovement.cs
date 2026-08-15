using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player axis values")]
    public float plrMoveX;
    public float plrMoveZ;
    public Vector2 plrMoveValue;
    public float plrMovementSpeed = 5f;

    [Header("Player input components")]
    [SerializeField] InputAction moveAction;
    [SerializeField] CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void Update()
    {
        plrMoveValue = moveAction.ReadValue<Vector2>();
    }

    void ApplyMovement()
    {
        Vector3 moveDirection = new Vector3(plrMoveValue.x, 0, plrMoveValue.y);
        moveDirection = transform.TransformDirection(moveDirection);
        // Gravity:
        if (!characterController.isGrounded)
        {
            moveDirection.y -= 9.81f * Time.fixedDeltaTime;
        }
        characterController.Move(moveDirection * plrMovementSpeed * Time.fixedDeltaTime);
    }

}
