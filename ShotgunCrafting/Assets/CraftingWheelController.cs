using UnityEngine;
using UnityEngine.UI;

public class CraftingWheelController : MonoBehaviour
{
    [SerializeField] Shotgun shotgunScript;
    [SerializeField] PlayerResources playerResourcesScript;
    [SerializeField] CraftingBar craftingBarScript;
    public Animator anim;
    private bool craftingWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int craftingID;
    public bool isCrafting = false;

    [Header("Crafting Settings")]
    [SerializeField] int upgradeCost = 20;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            craftingWheelSelected = !craftingWheelSelected;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isCrafting = true;
            shotgunScript.canShoot = false;
            Time.timeScale = 0.2f;
        }
        

        if (craftingWheelSelected)
        {
            anim.SetBool("OpenCraftingWheel", true);
        }
        else
        {
            anim.SetBool("OpenCraftingWheel", false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            isCrafting = false;
            shotgunScript.canShoot = true;
            Time.timeScale = 1f;
        }

        switch(craftingID)
        {
            case 0: //nothing is selected
                selectedItem.sprite = noImage;
                break;
            case 1: //upgrade selected
                if(playerResourcesScript.currentScrap >= upgradeCost)
                {
                    Debug.Log("upgrading");
                    playerResourcesScript.currentScrap -= upgradeCost;
                    craftingBarScript.isUpgrading = true;
                }
               
                break;
            case 2: //parry bullet
                Debug.Log("parry bullet crafted");
                
                break;
        }
    }

}
