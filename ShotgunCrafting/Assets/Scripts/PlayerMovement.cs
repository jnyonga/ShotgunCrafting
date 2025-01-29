using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 input;
    CharacterController playerController;
    [SerializeField] float speed = 10f;

    [SerializeField] float gravity = -25f;
    public Vector3 verticalVelocity = Vector3.zero;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;
    [SerializeField] GameObject groundCheck;

    bool shouldJump = false;
    [SerializeField] float jumpHeight = 3.5f;

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        //use groundCheck gameObject attached to player
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, 0.1f, groundLayer);
        if (isGrounded)
        {
            verticalVelocity.y = 0;
        }

        Vector3 movementVelocity = (transform.right * input.x + transform.forward * input.y) * speed;

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

        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
        }
        shouldJump = false;
    }

    public void OnJump()
    {
        shouldJump = true;
    }
}
