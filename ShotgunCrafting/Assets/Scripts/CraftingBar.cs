using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CraftingBar : MonoBehaviour
{
    [Header("Script Refs")]
    [SerializeField] Shotgun shotgunScript;
    [SerializeField] float redZoneMin = 0.4f;
    [SerializeField] float redZoneMax = 0.65f;
    [SerializeField] float greenZoneMin = 0.85f;
    [SerializeField] float greenZoneMax = 0.9f;
    [SerializeField] float sliderSpeedMultEasy = 0.85f;
    [SerializeField] float sliderSpeedMultMed = 1f;
    [SerializeField] float sliderSpeedMultHard = 1.2f;
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
        StartCoroutine(CraftingCheck());
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

            //slider speed determined by weapon level

            if(shotgunScript.isLvlOne)
            {
                upgradeSlider.value += Time.deltaTime * sliderSpeedMultEasy;
            }
            else if (shotgunScript.isLvlTwo)
            {
                upgradeSlider.value += Time.deltaTime * sliderSpeedMultMed;
            }
            else if (shotgunScript.isLvlThree)
            {
                upgradeSlider.value += Time.deltaTime * sliderSpeedMultHard;
            }
            

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
    public IEnumerator CraftingCheck()
    {
        if (Input.GetKeyUp(KeyCode.E) && isUpgrading)
        {
            if (upgradeSlider.value >= redZoneMin &&  upgradeSlider.value <= redZoneMax)
            {
                Debug.Log("Gain small durability " + upgradeSlider.value);
                shotgunScript.GainSmallDurability();
                isUpgrading = false;
                yield return new WaitForSeconds(0.5f);
                upgradeSlider.value = 0;
                HideUI();
            }
            else if (upgradeSlider.value >= greenZoneMin && upgradeSlider.value <= greenZoneMax)
            {
                Debug.Log("Gain Large durability " + upgradeSlider.value);
                shotgunScript.GainDurability();
                isUpgrading = false;
                yield return new WaitForSeconds(0.5f);
                upgradeSlider.value = 0;
                HideUI();
            }
            else
            {
                Debug.Log("Failed " + upgradeSlider.value);
                shotgunScript.FailDurability();
                isUpgrading = false;
                yield return new WaitForSeconds(0.5f);
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
        gameObject.transform.localScale = Vector3.one * 2f;
    }
}
