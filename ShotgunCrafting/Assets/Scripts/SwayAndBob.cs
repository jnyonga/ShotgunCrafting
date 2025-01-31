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

    Vector3 bobPosition;
    void BobOffset()
    {
        //used to generate our sin and cos waves
        
    }
    void BobRotation()
    {

    }
    float smooth = 10f;
    float smoothRot = 12f;
    void CompositePositionRotation()
    {
        //position
        transform.localPosition = Vector3.Lerp(transform.localPosition, swayPos, Time.deltaTime * smooth);

        //rotation
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(swayEulerRot), Time.deltaTime * smoothRot);
    }

}
