using UnityEngine;
using UnityEngine.UI;

public class CraftingWheelController : MonoBehaviour
{
    public Animator anim;
    private bool craftingWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int craftingID;
    public bool isCrafting = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            craftingWheelSelected = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isCrafting = true;
        }
        
        if(isCrafting == false)
        {
            craftingWheelSelected = false;
            Debug.Log("crafting wheel closed");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
        }

        if (craftingWheelSelected)
        {
            anim.SetBool("OpenCraftingWheel", true);
        }
        else
        {
            anim.SetBool("OpenCraftingWheel", false);
        }

        switch(craftingID)
        {
            case 0: //nothing is selected
                selectedItem.sprite = noImage;
                break;
            case 1: //upgrade selected
                Debug.Log("upgrading");
                isCrafting = false;
                break;
            case 2: //parry bullet
                Debug.Log("parry bullet crafted");
                isCrafting = false;
                break;
        }
    }

}
