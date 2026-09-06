using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class Spy2 : MonoBehaviour
{
    public Text displayName, industryText, manpowerText, fuelText, nukes1Text, nukes2Text;

    [HideInInspector]
    public int industry, manpower, fuel, nukes1, nukes2;
    [HideInInspector]
    public string cname;

    void Start()
    {
        displayName.text = cname;
        float chance = UnityEngine.Random.Range(0f, 100f);
        if (chance <= 50f) {
            industryText.text = CustomFunctions.TranslateText("Industry: ") + industry.ToString();
            manpowerText.text = CustomFunctions.TranslateText("Manpower: ") + manpower.ToString();
            fuelText.text = CustomFunctions.TranslateText("Fuel: ") + fuel.ToString();
            nukes1Text.text = CustomFunctions.TranslateText("Atomic Nukes: ") + nukes1.ToString();
            nukes2Text.text = CustomFunctions.TranslateText("Hydrogen Nukes: ") + nukes2.ToString();
        } else {
            industryText.text = "ESPIONAGE FAILED";
            manpowerText.text = "ESPIONAGE FAILED";
            fuelText.text = "ESPIONAGE FAILED";
            nukes1Text.text = "ESPIONAGE FAILED";
            nukes2Text.text = "ESPIONAGE FAILED";
        }
    }

    void Update()
    {
        
    }

    public void Enter() {
        Destroy(gameObject);
    }

    public void Exit() {
        Destroy(gameObject);
    }
}