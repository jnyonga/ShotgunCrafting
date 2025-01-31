using UnityEngine;
using UnityEngine.Windows;

public class SwayAndBob : MonoBehaviour
{
    [Header("Sway And Bob Settings")]
    [SerializeField] MouseLook mouseLookScript;
    [SerializeField] public PlayerMovement playerMovementScript;
    
    [SerializeField] public bool sway = true;
    public bool swayRotation = true;
    public bool bobOffset = true;
    public bool bobSway = true;
    private void Start()
    {
        swayOffset = transform.localPosition;
    }
    private void Update()
    {
        //get player's input
        GetInput();

        //get each movement and rotatation component
        Sway();
        SwayRotation();
        BobOffset();
        BobRotation();

        //apply all movement and rotation components
        CompositePositionRotation();
    }

    //store inputs internally
    Vector2 walkInput; //from keyboard
    Vector2 lookInput; //from mouse

    void GetInput()
    {
        walkInput.x = playerMovementScript.input.x;
        walkInput.y = playerMovementScript.input.y;
        walkInput = walkInput.normalized;

        lookInput.x = mouseLookScript.mouseInput.x;
        lookInput.y = mouseLookScript.mouseInput.y;
    }

    [Header("Sway")]
    [SerializeField] public float step = 0.01f;
    public float maxStepDistance = 0.06f;
    Vector3 swayPos;
    Vector3 swayOffset;
    void Sway()
    {
        if(sway == false)
        {
            swayPos = Vector3.zero;
            return;
        }

        Vector3 invertLook = lookInput * -step;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

        swayPos = invertLook + swayOffset;
    }
    [Header("Sway Rotation")]
    [SerializeField] public float rotationStep = 4f;
    public float maxRotationStep = 5f;
    Vector3 swayEulerRot;
    void SwayRotation()
    {
        if (swayRotation == false)
        {
            swayEulerRot = Vector3.zero;
            return;
        }

        Vector2 invertLook = lookInput * -rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);

        swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);
    }

    [Header("Bobbing")]
    [SerializeField] public float speedCurve;
    float curveSin { get => Mathf.Sin(speedCurve); }
    float curveCos { get => Mathf.Cos(speedCurve); }

    public Vector3 travelLimit = Vector3.one * 0.025f;
    public Vector3 bobLimit = Vector3.one * 0.01f;
    public float bobIntensity = 1;

    Vector3 bobPosition;
    void BobOffset()
    {
        //used to generate our sin and cos waves
        speedCurve += Time.deltaTime * (playerMovementScript.isGrounded ? playerMovementScript.movementVelocity.magnitude/bobIntensity : 1f) + 0.01f;

        if (bobOffset == false)
        {
            bobPosition = Vector3.zero;
            return;
        }

        bobPosition.x = (curveCos * bobLimit.x * (playerMovementScript.isGrounded ? 1 : 0)) - (walkInput.x * travelLimit.x);

        bobPosition.y = (curveSin * bobLimit.y) - (playerMovementScript.movementVelocity.y/bobIntensity * travelLimit.y);

        bobPosition.z = -(walkInput.y * travelLimit.z);
    }
    [Header("Bob Rotation")]
    [SerializeField] public Vector3 multiplier;
    Vector3 bobEulerRotation;
    void BobRotation() //roll, pitch, yaw change as a result of walking
    {
        if (bobSway == false)
        {
            bobEulerRotation = Vector3.zero;
            return;
        }

        bobEulerRotation.x = (walkInput != Vector2.zero ? multiplier.x * (Mathf.Sin(2 * speedCurve)) : multiplier.x * (Mathf.Sin(2 * speedCurve) / 2)); //pitch

        bobEulerRotation.y = (walkInput != Vector2.zero ? multiplier.y * curveCos : 0); //yaw
        
        bobEulerRotation.z = (walkInput != Vector2.zero ? multiplier.z * curveCos * walkInput.x : 0); //roll
    }
    float smooth = 10f;
    float smoothRot = 12f;
    void CompositePositionRotation()
    {
        //position
        transform.localPosition = Vector3.Lerp(transform.localPosition, swayPos + bobPosition, Time.deltaTime * smooth);

        //rotation
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(swayEulerRot) * Quaternion.Euler(bobEulerRotation), Time.deltaTime * smoothRot);
    }

}
