using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementOld : MonoBehaviour {

    [SerializeField] protected float movementSpeed = 5f;
    [SerializeField] protected float jumpForce = 4f;
    [SerializeField] protected float mass = 1f;
    [SerializeField] protected float damping = 5f;

    protected CharacterController characterController;
    protected float velocityY;
    protected Vector3 currentImpact;
    
    protected bool onSlope;
    protected Vector3 hitNormal;
    protected Vector3 fallDirection;

    private readonly float gravity = Physics.gravity.y;

    private Vector3 movementInput;

    //DEBUG
    private bool isGrounded;
    private Vector3 velocityBeforeMove;
    //private float scale = 1f;

    protected virtual void Awake() {

        characterController = GetComponent<CharacterController>();
        fallDirection = Vector3.up;
    }

    protected virtual void Update() {

        //debug
        isGrounded = characterController.isGrounded;

        Move();
        Jump();
    }

    protected virtual void OnControllerColliderHit(ControllerColliderHit hit) {
        hitNormal = hit.normal;
        onSlope = (Vector3.Angle(Vector3.up, hitNormal) > characterController.slopeLimit);
        if (Vector3.Angle(Vector3.up, hitNormal) > characterController.slopeLimit) {
            onSlope = true;
            Vector3 groundCross = Vector3.Cross(hitNormal, Vector3.up);
            fallDirection = Vector3.Cross(groundCross, hitNormal);
        } else {
            onSlope = false;
            fallDirection = Vector3.up;
        }
        //Debug.DrawRay(hit.point, hit.normal.normalized, Color.green, 1f);
    }

    protected virtual void Move() {

        movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        movementInput = transform.TransformDirection(movementInput);

        if (characterController.isGrounded && velocityY < 0f) {
            velocityY = -2f;
        }

        velocityY += gravity * Time.deltaTime;

        //fallDirection = Vector3.up;

        //if (onSlope) {
        //    Vector3 groundCross = Vector3.Cross(hitNormal, Vector3.up);
        //    fallDirection = Vector3.Cross(groundCross, hitNormal);
        //   // velocityY = Mathf.Lerp(velocityY, terminal_velocity, 0.25f);
        //}

        Vector3 velocity = movementInput * movementSpeed + fallDirection * velocityY;

        if (currentImpact.magnitude > 0.2f){
            velocity += currentImpact;
        }

        velocityBeforeMove = velocity;

        characterController.Move(velocity * Time.deltaTime);

        currentImpact = Vector3.Lerp(currentImpact, Vector3.zero, damping * Time.deltaTime);
    }

    protected virtual void Jump() {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) {

            if (characterController.isGrounded) {
                addForce(Vector3.up, jumpForce);
            }
        }
    }

    protected virtual void ResetImpact() {
        currentImpact = Vector3.zero;
    }

    protected virtual void ResetVelY() {
        velocityY = 0f;
    }

    protected virtual void ResetImpactY() {
        currentImpact.y = 0f;
        velocityY = 0f;
    }

    public void addForce(Vector3 direction, float magnitude) {

        currentImpact += direction.normalized * magnitude / mass;
    }
}
