using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Shotgun : MonoBehaviour
{
    int maxAmmo;
    [SerializeField] GameObject muzzle;
    [SerializeField] int ammo;
    [SerializeField] float maxDistance = 20f;

    Ray ray1;
    Ray ray2;
    Ray ray3;

    private void Start()
    {
        ammo = 6;
        maxAmmo = 6;

        ray1 = new Ray(muzzle.transform.position, muzzle.transform.forward);
        ray2 = new Ray(muzzle.transform.position, Quaternion.Euler(0, 15, 0) * muzzle.transform.forward);
        ray3 = new Ray(muzzle.transform.position, Quaternion.Euler(0, -15, 0) * muzzle.transform.forward);
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
            Debug.DrawRay(muzzle.transform.position, Quaternion.Euler(0, 15, 0) * muzzle.transform.forward * maxDistance, Color.red, 2f);
            Debug.DrawRay(muzzle.transform.position, Quaternion.Euler(0, -15, 0) * muzzle.transform.forward * maxDistance, Color.red, 2f);
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
            Debug.Log("Shot");
            if (Physics.Raycast(ray1, out RaycastHit hit1, maxDistance))
            {
                Debug.Log(hit1.collider.gameObject.name + " was hit!");

            }

            if (Physics.Raycast(ray2, out RaycastHit hit2, maxDistance))
            {
                Debug.Log(hit2.collider.gameObject.name + " was hit!");
            }

            if (Physics.Raycast(ray3, out RaycastHit hit3, maxDistance))
            {
                Debug.Log(hit3.collider.gameObject.name + " was hit!");
            }
        }
    }
}
