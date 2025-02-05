using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Shotgun : MonoBehaviour
{
    public float damage = 6f;
    public bool canShoot = true;
    //int maxAmmo;
    [SerializeField] GameObject muzzle;
    [SerializeField] int ammo;
    [SerializeField] float maxDistance = 50f;
    private bool isReloading = false;
    [SerializeField] float reloadSpeed = 0.5f;

    [Header("Durability Settings")]
    [SerializeField] float durability = 100f;
    [SerializeField] float durabilityLostOnShot = 10f;


    Ray ray1;
    Ray ray2;
    Ray ray3;

    private void Start()
    {
     

        ammo = 2;
        //maxAmmo = 2;

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
            if (ammo != 0 && canShoot && durability > 0)
            {
                Shoot();
                Debug.DrawRay(muzzle.transform.position, muzzle.transform.forward * maxDistance, Color.red, 2f);
                Debug.DrawRay(muzzle.transform.position, Quaternion.Euler(-2, 1, 0) * muzzle.transform.forward * maxDistance, Color.green, 2f);
                Debug.DrawRay(muzzle.transform.position, Quaternion.Euler(-2, -1, 0) * muzzle.transform.forward * maxDistance, Color.blue, 2f);
            }
            
        }
        //stopped performing context
        else
        {
            
        }
    }

    public void Reload_Event(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (ammo != 2 && isReloading != true)
            {
                Reload();
            }
        }
        else
        {

        }
    }

    void Reload()
    {
        isReloading = true;
        StartCoroutine(Reloading());
    }
    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadSpeed);
        isReloading = false;
        ammo = 2;
    }
    void Shoot()
    {
        --ammo;
        durability -= durabilityLostOnShot;
       
            //Debug.Log("Shoot");

            ray1.origin = muzzle.transform.position;
            ray2.origin = muzzle.transform.position;
            ray3.origin = muzzle.transform.position;

            ray1.origin = muzzle.transform.forward;
            ray2.origin = Quaternion.Euler(-2, 1, 0) * muzzle.transform.forward;
            ray3.origin = Quaternion.Euler(-2, -1, 0) * muzzle.transform.forward;

            if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out RaycastHit hit1, maxDistance))
            {
                Debug.Log(hit1.collider.gameObject.name + " was hit by ray1!");
                if (hit1.collider.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    hit1.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                }
                

            }

            if (Physics.Raycast(muzzle.transform.position, Quaternion.Euler(-2, 1, 0) * muzzle.transform.forward, out RaycastHit hit2, maxDistance))
            {
                Debug.Log(hit2.collider.gameObject.name + " was hit by ray2!");
                if (hit2.collider.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    hit2.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                }
            }

            if (Physics.Raycast(muzzle.transform.position, Quaternion.Euler(-2, -1, 0) * muzzle.transform.forward, out RaycastHit hit3, maxDistance))
            {
                Debug.Log(hit3.collider.gameObject.name + " was hit by ray3!");
                if (hit3.collider.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    hit3.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                }
            }
        
    }

    
}
