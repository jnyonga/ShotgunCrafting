using UnityEngine;
using UnityEngine.InputSystem;

public class Crafting : MonoBehaviour
{
    InputSystem_Actions playerControls;
    [Header("Object References")]
    [SerializeField] CraftingBar craftingBarScript;
    PlayerResources playerResourcesScript;

    private void Awake()
    {
        playerControls = new InputSystem_Actions();
        playerResourcesScript = GetComponent<PlayerResources>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    private void Update()
    {
        playerControls.PlayerControls.Craft.performed += ctx => Craft();
    }

    void Craft()
    {
        if (playerResourcesScript.currentScrap >= 25 && craftingBarScript.isUpgrading != true)
        {
            playerResourcesScript.currentScrap -= 25;
            craftingBarScript.isUpgrading = true;
        }
        
    }
}
