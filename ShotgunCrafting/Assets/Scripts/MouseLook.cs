using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class MouseLook : MonoBehaviour
{
    [SerializeField] CraftingWheelController craftingWheelController;
    public Vector2 mouseInput;

    [SerializeField] float sensitivityX = 8f;
    [SerializeField] float sensitivityY = 0.5f;
    float mouseY;
    float mouseX;
    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    float xRotation = 0f;
    private void Update()
    {
        mouseX = mouseInput.x * sensitivityX;
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);
        
        mouseY = mouseInput.y * sensitivityY;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = playerCamera.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation; 
    }
    public void MouseLookX(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (craftingWheelController.isCrafting == false)
            {
                mouseInput.x = context.ReadValue<float>();
            }
            
        }
        //stopped performing context
        else
        {

        }
    }
    public void MouseLookY(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (craftingWheelController.isCrafting == false)
            {
                mouseInput.y = context.ReadValue<float>();
            }
        }
        //stopped performing context
        else
        {

        }
    }
}
