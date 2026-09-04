using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class Nuclear : MonoBehaviour {
    public Text yourAtom, yourHydro, theirAtom, theirHydro;
    public Button atomButton, hydroButton;

    [HideInInspector]
	public string country;
	[HideInInspector]
	public Controller controller;

    void Start()
    {
        if (controller.countryDatas[controller.playerCountry].nukes[0] >= 1 && controller.countryDatas[country].nukes[0] < 99999) {
			atomButton.interactable = true;
		} else {
            atomButton.interactable = false;
        }
		if (controller.countryDatas[controller.playerCountry].nukes[1] >= 1 && controller.countryDatas[country].nukes[1] < 99999) {
			hydroButton.interactable = true;
		} else {
            hydroButton.interactable = false;
        }
        
        yourAtom.text = "You have: " + controller.countryDatas[controller.playerCountry].nukes[0].ToString();
        theirAtom.text = controller.countryDatas[country].nukes[0].ToString();
        yourHydro.text = "You have: " + controller.countryDatas[controller.playerCountry].nukes[1].ToString();
        theirHydro.text = controller.countryDatas[country].nukes[1].ToString();
    }

    void Update() {
    }

    public void atomAid() {
        controller.countryDatas[controller.playerCountry].nukes[0]--;
        controller.countryDatas[country].nukes[0]++;

        if (controller.countryDatas[controller.playerCountry].nukes[0] >= 1 && controller.countryDatas[country].nukes[0] < 99999) {
			atomButton.interactable = true;
		} else {
            atomButton.interactable = false;
        }

        yourAtom.text = "You have: " + controller.countryDatas[controller.playerCountry].nukes[0].ToString();
        theirAtom.text = controller.countryDatas[country].nukes[0].ToString();
    }

    public void hydroAid() {
        controller.countryDatas[controller.playerCountry].nukes[1]--;
        controller.countryDatas[country].nukes[1]++;

		if (controller.countryDatas[controller.playerCountry].nukes[1] >= 1 && controller.countryDatas[country].nukes[1] < 99999) {
			hydroButton.interactable = true;
		} else {
            hydroButton.interactable = false;
        }

        yourHydro.text = "You have: " + controller.countryDatas[controller.playerCountry].nukes[1].ToString();
        theirHydro.text = controller.countryDatas[country].nukes[1].ToString();
    }

    public void Exit() {
        Destroy(gameObject);
    }
}
