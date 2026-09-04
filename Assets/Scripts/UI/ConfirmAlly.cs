using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class ConfirmAlly : MonoBehaviour
{
    public Button yesButton, noButton;

    [HideInInspector]
    public string country;
    [HideInInspector]
    public Controller controller;

    void Start()
    {
        if (controller.countryDatas[controller.playerCountry].industry < 1000) {
            yesButton.interactable = false;
        }
    }

    void Update()
    {
        
    }

    public void Enter() {
        if (controller.countriesIsNeutral.Contains(country)) {
            controller.countriesIsNeutral.Remove(country);
        }
        controller.countriesIsAxis[country] = controller.countriesIsAxis[controller.playerCountry];

		GameEvent myEvent = new GameEvent();

		myEvent.countryTarget = country;
		myEvent.eventType = EventType.Diplomacy;

        if (controller.countriesIsAxis[controller.playerCountry] == 0) {
		    myEvent.eventValue = 0;
        } else if (controller.countriesIsAxis[controller.playerCountry] == 1) {
            myEvent.eventValue = 1;
        } else if (controller.countriesIsAxis[controller.playerCountry] == 3) {
            myEvent.eventValue = 3;
        } else if (controller.countriesIsAxis[controller.playerCountry] == 4) {
            myEvent.eventValue = 4;
        }

		myEvent.title = CustomFunctions.TranslateText("Alliance!");
		string playerCountry = Controller.CheckCustomFlag(controller.countryCustomNameOverrides, controller.playerCountry) == "" ? controller.playerCountry : Controller.CheckCustomFlag(controller.countryCustomNameOverrides, controller.playerCountry);
		string targetCountry = Controller.CheckCustomFlag(controller.countryCustomNameOverrides, country) == "" ? country : Controller.CheckCustomFlag(controller.countryCustomNameOverrides, country);
		myEvent.description = CustomFunctions.TranslateText(playerCountry) + CustomFunctions.TranslateText(" has added ") + CustomFunctions.TranslateText(targetCountry) + CustomFunctions.TranslateText(" to its alliance.");

		controller.NewsPopup(myEvent);

		foreach (Unit u in controller.soldiers) {
			if (u != null)
				u.CheckCountry();
		}
    }

    public void Exit() {
        Destroy(gameObject);
    }
}
