using UnityEngine;
using UnityEngine.UI;

public class CraftingBar : MonoBehaviour
{
    [Header("Script Refs")]
    [SerializeField] Shotgun shotgunScript;
    [SerializeField] float redZoneMin = 0.5f;
    [SerializeField] float redZoneMax = 0.7f;
    [SerializeField] float sliderSpeedMult = 1f;
    [SerializeField] public bool isUpgrading = false;
    [SerializeField] Slider upgradeSlider;
    void Start()
    {
        upgradeSlider.value = 0;
        HideUI();
    }

    // Update is called once per frame
    void Update()
    {
        CraftingCheck();
    }

    private void FixedUpdate()
    {
        SliderMovement();
    }

    void SliderMovement()
    {
        if(isUpgrading)
        {
            ShowUI();
            upgradeSlider.value += Time.deltaTime * sliderSpeedMult;

            upgradeSlider.value = Mathf.Clamp01(upgradeSlider.value);  

            if (upgradeSlider.value == 1)
            {
                Debug.Log("Gain small durability");
                shotgunScript.GainSmallDurability();
                isUpgrading = false;
                upgradeSlider.value = 0;
                HideUI();
            }
        }
        

    }
    public void CraftingCheck()
    {
        if (Input.GetKeyUp(KeyCode.E) && isUpgrading)
        {
            if (upgradeSlider.value >= redZoneMin &&  upgradeSlider.value <= redZoneMax)
            {
                Debug.Log("Gain durability");
                shotgunScript.GainDurability();
                isUpgrading = false;
                upgradeSlider.value = 0;
                HideUI();
            }
            else
            {
                Debug.Log("Gain small durability");
                shotgunScript.GainSmallDurability();
                isUpgrading = false;
                upgradeSlider.value = 0;
                HideUI();
            }
        }
       
    }

    void HideUI()
    {
        gameObject.transform.localScale = Vector3.zero;
    }

    void ShowUI()
    {
        gameObject.transform.localScale = Vector3.one;
    }
}
