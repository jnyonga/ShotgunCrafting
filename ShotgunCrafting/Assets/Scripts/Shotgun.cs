using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Shotgun : MonoBehaviour
{
    public float damage = 6f;
    //int maxAmmo;
    [SerializeField] GameObject muzzle;
    [SerializeField] int ammo;
    [SerializeField] float maxDistance = 50f;

    Ray ray1;
    Ray ray2;
    Ray ray3;

    private void Start()
    {
     

        ammo = 6;
        //maxAmmo = 6;

        ray1 = new Ray(muzzle.transform.position, muzzle.transform.forward);
        ray2 = new Ray(muzzle.transform.position, Quaternion.Euler(-2, 1, 0) * muzzle.transform.forward);
        ray3 = new Ray(muzzle.transform.position, Quaternion.Euler(-2, -1, 0) * muzzle.transform.forward);
    }
    private void Update()
    {
        
    }
    public void Shoot_Event(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
            Debug.DrawRay(muzzle.transform.position, muzzle.transform.forward * maxDistance, Color.red, 2f);
            Debug.DrawRay(muzzle.transform.position, Quaternion.Euler(-2, 1, 0) * muzzle.transform.forward * maxDistance, Color.green, 2f);
            Debug.DrawRay(muzzle.transform.position, Quaternion.Euler(-2, -1, 0) * muzzle.transform.forward * maxDistance, Color.blue, 2f);
        }
        //stopped performing context
        else
        {
            
        }
    }

    void Shoot()
    {
        if (ammo > 0)
        {
            //Debug.Log("Shot");

            ray1.origin = muzzle.transform.position;
            ray2.origin = muzzle.transform.position;
            ray3.origin = muzzle.transform.position;

            ray1.origin = muzzle.transform.forward;
            ray2.origin = Quaternion.Euler(-2, 1, 0) * muzzle.transform.forward;
            ray3.origin = Quaternion.Euler(-2, -1, 0) * muzzle.transform.forward;

            if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out RaycastHit hit1, maxDistance))
            {
                Debug.Log(hit1.collider.gameObject.name + " was hit by ray1!");


            }

            if (Physics.Raycast(muzzle.transform.position, Quaternion.Euler(-2, 1, 0) * muzzle.transform.forward, out RaycastHit hit2, maxDistance))
            {
                Debug.Log(hit2.collider.gameObject.name + " was hit by ray2!");
                

            }

            if (Physics.Raycast(muzzle.transform.position, Quaternion.Euler(-2, -1, 0) * muzzle.transform.forward, out RaycastHit hit3, maxDistance))
            {
                Debug.Log(hit3.collider.gameObject.name + " was hit by ray3!");
                

            }
        }
    }
}
