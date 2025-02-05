using UnityEngine;
using UnityEngine.UI;

public class CraftingWheelController : MonoBehaviour
{
    [SerializeField] Shotgun shotgunScript;
    public Animator anim;
    private bool craftingWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int craftingID;
    public bool isCrafting = false;

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
                Debug.Log("upgrading");
               
                break;
            case 2: //parry bullet
                Debug.Log("parry bullet crafted");
                
                break;
        }
    }

}
