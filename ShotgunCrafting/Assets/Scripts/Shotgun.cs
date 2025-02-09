using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using TMPro;

public class Shotgun : MonoBehaviour
{
    [Header("Shotgun Parameters")]
    Renderer shotgunRenderer;
    [SerializeField] Material[] shotgunMaterials;
    private float damage;
    public float brokenDamage;
    public float lvlOneDamage;
    public float lvlTwoDamage;
    public float lvlThreeDamage;
    private bool canShoot = true;
    [SerializeField] TextMeshProUGUI ammoCounterText;
    [SerializeField] GameObject muzzle;
    [SerializeField] int ammo;
    [SerializeField] float maxDistance = 50f;
    private bool isReloading = false;
    [SerializeField] float reloadSpeed = 0.5f;

    public bool isLvlOne;
    public bool isLvlTwo;
    public bool isLvlThree;

    [Header("Durability Settings")]
    [SerializeField] TextMeshProUGUI durabilityText;
    [SerializeField] float durability = 100f;
    [SerializeField] float durabilityLostOnShot = 5f;
    [SerializeField] float durabilityLostPerSecond = 3f;
    [SerializeField] float durabilityGain = 40;
    [SerializeField] float durabilityGainWeak = 25;
    [SerializeField] float durabilityFail = 10f;

    [Header("Object References")]
    [SerializeField] PlayerResources playerResourcesScript;

    Ray ray1;
    Ray ray2;
    Ray ray3;

    private void Start()
    {
     shotgunRenderer = GetComponent<Renderer>();

        ammo = 2;
        //maxAmmo = 2;

        ray1 = new Ray(muzzle.transform.position, muzzle.transform.forward);
        ray2 = new Ray(muzzle.transform.position, Quaternion.Euler(-2, 1, 0) * muzzle.transform.forward);
        ray3 = new Ray(muzzle.transform.position, Quaternion.Euler(-2, -1, 0) * muzzle.transform.forward);
    }
    private void Update()
    {
        durabilityText.text = durability.ToString();
        ammoCounterText.text = ammo.ToString(); 

        if(ammo == 0)
        {
            Reload();
        }

        //Change material based on level
        //Change damage based on level

        if (durability <= 0)
        {
            damage = brokenDamage;
        }
        if (isLvlOne & durability != 0)
        {
            shotgunRenderer.material = shotgunMaterials[0];
            damage = lvlOneDamage;

        }
        else if (isLvlTwo)
        {
            shotgunRenderer.material = shotgunMaterials[1];
            damage = lvlTwoDamage;
        }
        else
        {
            shotgunRenderer.material = shotgunMaterials[2];
            damage = lvlThreeDamage;
        }
        

    }
    private void FixedUpdate()
    {
        DurabilityDrain();
    }
    public void Shoot_Event(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (ammo != 0 && canShoot)
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
                    playerResourcesScript.currentRage += damage;
                }
                

            }

            if (Physics.Raycast(muzzle.transform.position, Quaternion.Euler(-2, 1, 0) * muzzle.transform.forward, out RaycastHit hit2, maxDistance))
            {
                Debug.Log(hit2.collider.gameObject.name + " was hit by ray2!");
                if (hit2.collider.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    hit2.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                    playerResourcesScript.currentRage += damage;
                }
            }

            if (Physics.Raycast(muzzle.transform.position, Quaternion.Euler(-2, -1, 0) * muzzle.transform.forward, out RaycastHit hit3, maxDistance))
            {
                Debug.Log(hit3.collider.gameObject.name + " was hit by ray3!");
                if (hit3.collider.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    hit3.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                    playerResourcesScript.currentRage += damage;
                }
            }
        
    }

    void DurabilityDrain()
    {
        if (durability > 0)
        {
            durability -= Time.deltaTime * durabilityLostPerSecond;
        }
        else
        {
            durability = 0;
        }

        if (durability > 300)
        {
            durability = 300;
        }

        if (durability < 100)
        {
            isLvlOne = true;
            isLvlTwo = false;
            isLvlThree = false;
        }
        else if (durability >= 100 && durability < 200)
        {
            isLvlOne = false;
            isLvlTwo = true;
            isLvlThree = false;
        }
        else
        {
            isLvlOne = false;
            isLvlTwo = false;
            isLvlThree = true;
        }
    }
    
    public void GainDurability()
    {
        durability += durabilityGain;
    }

    public void GainSmallDurability()
    {
        durability += durabilityGainWeak;
    }

    public void FailDurability()
    {
        durability -= durabilityFail;
    }
}
