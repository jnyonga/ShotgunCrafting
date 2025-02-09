using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{ 
    public Vector2 input;
    CharacterController playerController;
    PlayerResources playerResourcesScript;
    public float currentSpeed;
    public float baseSpeed = 10f;

    [SerializeField] float gravity = -25f;
    public Vector3 movementVelocity;
    public Vector3 verticalVelocity = Vector3.zero;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] public bool isGrounded;
    [SerializeField] GameObject groundCheck;

    bool shouldJump = false;
    [SerializeField] float jumpHeight = 3.5f;

    [SerializeField] private int numberOfJumps = 0;
    [SerializeField] private int maxNumberOfJumps = 2;

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
        playerResourcesScript = GetComponent<PlayerResources>();
        
    }
    private void Start()
    {
       Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        //use groundCheck gameObject attached to player
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, 0.1f, groundLayer);
        if (isGrounded)
        {
            verticalVelocity.y = 0;
            numberOfJumps = 0;
        }

        //speed based on rage
        currentSpeed = baseSpeed + ((playerResourcesScript.currentRage / 20) * 1.2f);

        movementVelocity = (transform.right * input.x + transform.forward * input.y) * currentSpeed;

        //moves player
        playerController.Move(movementVelocity * Time.deltaTime);


        if (shouldJump)
        {
            Jump();
        }

        //gravity
        verticalVelocity.y += gravity * Time.deltaTime;
        playerController.Move(verticalVelocity * Time.deltaTime);

       
    }

    

    public void Move_Event(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           input = context.ReadValue<Vector2>();
        }
        //stopped performing context
        else
        {
            input = Vector2.zero;
        }
    }

    void Jump()
    {
        if (isGrounded || numberOfJumps < maxNumberOfJumps)
        {
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
            numberOfJumps++;
            shouldJump = false;
        }
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            shouldJump = true;
        }
        //stopped performing context
        else if (context.canceled)
        {
            shouldJump = false;
        }
        
    }
    
 
}
