using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class DipOptions : MonoBehaviour {
    public Button addAlly, removeAlly, declareWar, spy;
    public GameObject spyConfirm, allyPrefab;
    
    [HideInInspector]
    public string country;
    [HideInInspector]
    public Controller controller;
    [HideInInspector]
    public DiplomacyControl go;

    void Start()
    {
        if (!controller.adipon || controller.countriesIsNeutral.Contains(controller.playerCountry) || controller.countriesIsAxis[country] != controller.countriesIsAxis[controller.playerCountry] && !controller.countriesIsNeutral.Contains(country) || controller.countriesIsAxis[country] == controller.countriesIsAxis[controller.playerCountry]) {
            addAlly.interactable = false;
        } 

        if (controller.countriesIsAxis[country] != controller.countriesIsAxis[controller.playerCountry]) {
            removeAlly.interactable = false;
        }

        if (!controller.countriesIsNeutral.Contains(country) && controller.countriesIsAxis[country] != controller.countriesIsAxis[controller.playerCountry]) {
            declareWar.interactable = false;
        }

        if (controller.countriesIsAxis[country] == controller.countriesIsAxis[controller.playerCountry]) {
            spy.interactable = false;
        }
    }

    void Update() {
    }

	public void Ally() {
		ConfirmAlly p = Instantiate(allyPrefab, GameObject.Find("Canvas").transform).GetComponent<ConfirmAlly>();
		p.transform.position = new Vector2(Screen.width / 2f, Screen.height / 2f);
		p.country = country;
		p.controller = controller;
	}

	public void RemoveAlly() {
		controller.countriesIsNeutral.Add(country);
		controller.countriesIsAxis[country] = 2;

		GameEvent myEvent = new GameEvent();

		myEvent.countryTarget = country;
		myEvent.eventType = EventType.Diplomacy;
		myEvent.eventValue = 2;

		myEvent.title = CustomFunctions.TranslateText("Neutrality!");
		string playerCountry = Controller.CheckCustomFlag(controller.countryCustomNameOverrides, controller.playerCountry) == "" ? controller.playerCountry : Controller.CheckCustomFlag(controller.countryCustomNameOverrides, controller.playerCountry);
		string targetCountry = Controller.CheckCustomFlag(controller.countryCustomNameOverrides, country) == "" ? country : Controller.CheckCustomFlag(controller.countryCustomNameOverrides, country);
		myEvent.description = CustomFunctions.TranslateText(playerCountry) + CustomFunctions.TranslateText(" has removed ") + CustomFunctions.TranslateText(targetCountry) + CustomFunctions.TranslateText(" from its alliance.");

		controller.NewsPopup(myEvent);

		foreach (Unit u in controller.soldiers) {
			if (u != null)
				u.CheckCountry();
		}
	}

    public void DeclareWar() {
		//will only put AI into the two default alliances
		if (controller.countriesIsNeutral.Contains(controller.playerCountry)) {
			if (controller.countriesIsAxis[country] != 1) {
				ChangeAlliance(1, true);
			} else {
				ChangeAlliance(0, true);
			}
		} else {
			if (controller.playerIsAxis != 1) {
				ChangeAlliance(1);
			} else {
				ChangeAlliance(0);
			}
		}
		foreach (Unit u in controller.soldiers) {
			if (u != null)
				u.CheckCountry();
		}
	}
	void ChangeAlliance(int teamNumber, bool forPlayer = false) {
		if (forPlayer) {
			controller.countriesIsAxis[controller.playerCountry] = teamNumber;
			controller.playerIsAxis = teamNumber;
		} else {
			controller.countriesIsAxis[country] = teamNumber;
		}

		GameEvent myEvent = new GameEvent();

		if (!controller.countriesIsNeutral.Contains(country) && !controller.countriesIsNeutral.Contains(controller.playerCountry)) {
			//has to be a country that isn't neutral to be betrayal

			myEvent.countryTarget = controller.playerCountry;
			myEvent.eventType = EventType.Health;
			myEvent.eventValue = 5;

			myEvent.title = CustomFunctions.TranslateText("Betrayal!");
			myEvent.description = CustomFunctions.TranslateText("We have betrayed our allies. Our morale and supply were sabotaged.");

			controller.ConsumeEvent(myEvent);
			controller.NewsPopup(myEvent);
		}
		if (forPlayer) {
			if (controller.countriesIsNeutral.Contains(controller.playerCountry))
				controller.countriesIsNeutral.Remove(controller.playerCountry);
		} else {
			if (controller.countriesIsNeutral.Contains(country))
				controller.countriesIsNeutral.Remove(country);
		}

		myEvent = new GameEvent();

		myEvent.countryTarget = country;
		myEvent.eventType = EventType.Diplomacy;
		myEvent.eventValue = teamNumber;

		myEvent.title = CustomFunctions.TranslateText("War!");
		string playerCountry = Controller.CheckCustomFlag(controller.countryCustomNameOverrides, controller.playerCountry) == "" ? controller.playerCountry : Controller.CheckCustomFlag(controller.countryCustomNameOverrides, controller.playerCountry);
		string targetCountry = Controller.CheckCustomFlag(controller.countryCustomNameOverrides, country) == "" ? country : Controller.CheckCustomFlag(controller.countryCustomNameOverrides, country);
		myEvent.description = CustomFunctions.TranslateText(playerCountry) + " " + CustomFunctions.TranslateText("declared war on") + " " + CustomFunctions.TranslateText(targetCountry);

		controller.NewsPopup(myEvent);
	}
    
    public void Spy() {
        Spy1 p = Instantiate(spyConfirm, GameObject.Find("Canvas").transform).GetComponent<Spy1>();
		p.transform.position = new Vector2(Screen.width / 2f, Screen.height / 2f);
		p.country = country;
		p.controller = controller;
    }

    public void ExitGo() {
        Destroy(go.gameObject);
    }

    public void Exit() {
        Destroy(gameObject);
    }
}
