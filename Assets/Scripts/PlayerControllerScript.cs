using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : MonoBehaviour
{
    PlayerActions playerInput;
    public PlayerAnimHandler playerAnimHandler;
    public CharacterController characterController;
    public GameObject CaneCollider;
    public HealthComponent healthComponent;
    public float MeleeDamage = 5f;

    Vector2 currentMovementInput;
    Vector3 currentMovement;

    //Movement Bools
    bool isMoving = false;
    bool isRunning = false;
    bool isBlocking = false;
    bool isAttacking = false;

    public float MovementSpeed = 4f;
    public float RunSpeed = 8f;

    float rotationFactorPerFrame = 8f;
    private void Awake()
    {
        playerInput = new PlayerActions();
        playerAnimHandler= GetComponent<PlayerAnimHandler>();
        characterController = this.transform.parent.GetComponent<CharacterController>();
        healthComponent = new HealthComponent(100f);
        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;
        playerInput.CharacterControls.Run.performed += OnRun;
        playerInput.CharacterControls.Block.started += OnBlock;
        playerInput.CharacterControls.Block.canceled += OnBlock;
        playerInput.CharacterControls.MeleeAttack.started += OnMeleeAttack;
       




    }

  

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }


    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
       
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        /*if (positionToLookAt.z < 0)
        {
            positionToLookAt.z = 0;
            positionToLookAt.x = 0;
        }*/

        Quaternion currentRotation = transform.rotation;

        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    public void OnMeleeAttack(InputAction.CallbackContext context)
    {
        if (!isAttacking)
        {
            isAttacking = context.ReadValueAsButton();
            CaneCollider.SetActive(true);
            playerAnimHandler.CallAttack();
        }
    }
    public void OnRun(InputAction.CallbackContext context) { 
        isRunning = context.ReadValueAsButton();
        
    }
    public void OnBlock(InputAction.CallbackContext context)
    {
        isBlocking = context.ReadValueAsButton();
        playerAnimHandler.CallBlock(isBlocking);
    }
    public void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMoving = currentMovementInput.x !=0 || currentMovementInput.y !=0;
    }

    public void OnAttackEnd()
    {
        CaneCollider.SetActive(false);
        isAttacking = false;
        playerAnimHandler.EndAttack();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (characterController != null)
        {
           
                Vector3 movementVector = currentMovement * (isRunning ? RunSpeed : MovementSpeed);
            if (isBlocking || isAttacking)
            {
                movementVector = Vector3.zero;
            }
            characterController.SimpleMove(movementVector);
            
            HandleRotation();
        
            playerAnimHandler.UpdateVelocity(currentMovement,isMoving, isMoving ? isRunning : false);
        }
    }
}
