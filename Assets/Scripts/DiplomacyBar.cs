using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class DiplomacyBar : MonoBehaviour {
	public Image countryDisplay;
	public Text countryNameDisplay, manpowerTotal, industryTotal, fuelTotal, nukeTotal;
	public Button manpowerAid, industryAid, fuelAid, nukeAid, warDeclaration;
	public GameObject nukePrefab, dipPrefab;
	
	[HideInInspector]
	public DiplomacyControl dipco;
	[HideInInspector]
	public string country;
	[HideInInspector]
	public Controller controller;

	void Update() {
		if (controller.countriesIsNeutral.Contains(country) || controller.countriesIsNeutral.Contains(country) || !controller.countriesIsNeutral.Contains(controller.playerCountry) && controller.countriesIsAxis[country] != controller.playerIsAxis && !controller.countriesIsNeutral.Contains(country)) {
			if ((controller.countryDatas[country].nukes[0] + controller.countryDatas[country].nukes[1]) < 11) {
				nukeTotal.text = "0-10";
			} 
			else if ((controller.countryDatas[country].nukes[0] + controller.countryDatas[country].nukes[1]) < 101 && (controller.countryDatas[country].nukes[0] + controller.countryDatas[country].nukes[1]) > 10) {
				nukeTotal.text = "11-100";
			}
			else if ((controller.countryDatas[country].nukes[0] + controller.countryDatas[country].nukes[1]) < 501 && (controller.countryDatas[country].nukes[0] + controller.countryDatas[country].nukes[1]) > 100) {
				nukeTotal.text = "101-500";
			}
			else if ((controller.countryDatas[country].nukes[0] + controller.countryDatas[country].nukes[1]) < 1001 && (controller.countryDatas[country].nukes[0] + controller.countryDatas[country].nukes[1]) > 500) {
				nukeTotal.text = "501-1000";
			}
			else if ((controller.countryDatas[country].nukes[0] + controller.countryDatas[country].nukes[1]) < 10001 && (controller.countryDatas[country].nukes[0] + controller.countryDatas[country].nukes[1]) > 1000) {
				nukeTotal.text = "1001-10000";
			}
			else if ((controller.countryDatas[country].nukes[0] + controller.countryDatas[country].nukes[1]) < 20000 && (controller.countryDatas[country].nukes[0] + controller.countryDatas[country].nukes[1]) > 10000) {
				nukeTotal.text = ">10001";
			}
		} else {
			nukeTotal.text = (controller.countryDatas[country].nukes[0] + controller.countryDatas[country].nukes[1]).ToString();
		}

		if (manpowerAid) {
			manpowerTotal.text = controller.countryDatas[country].manpower.ToString();
			industryTotal.text = controller.countryDatas[country].industry.ToString();
			fuelTotal.text = controller.countryDatas[country].fuel.ToString();
		}
		if (manpowerAid) {
			if (controller.countryDatas[controller.playerCountry].manpower < 50) {
				if (manpowerAid.interactable)
					manpowerAid.interactable = false;
			} else {
				if (!manpowerAid.interactable)
					manpowerAid.interactable = true;
			}
			if (controller.countryDatas[controller.playerCountry].fuel < 50) {
				if (fuelAid.interactable)
					fuelAid.interactable = false;
			} else {
				if (!fuelAid.interactable)
					fuelAid.interactable = true;
			}
			if (controller.countryDatas[controller.playerCountry].industry < 50) {
				if (industryAid.interactable)
					industryAid.interactable = false;
			} else {
				if (!industryAid.interactable)
					industryAid.interactable = true;
			}
		}
	}

	public void DiplomacyOptions() {
		DipOptions p = Instantiate(dipPrefab, GameObject.Find("Canvas").transform).GetComponent<DipOptions>();
		p.transform.position = new Vector2(Screen.width / 2f, Screen.height / 2f);
		p.go = dipco;
		p.country = country;
		p.controller = controller;
	}

	public void ManpowerAid() {
		if (controller.countryDatas[controller.playerCountry].manpower >= 50) {
			controller.countryDatas[controller.playerCountry].manpower -= 50;
			controller.countryDatas[country].manpower += 50;
		}
	}
	public void IndustryAid() {
		if (controller.countryDatas[controller.playerCountry].industry >= 50) {
			controller.countryDatas[controller.playerCountry].industry -= 50;
			controller.countryDatas[country].industry += 50;
		}
	}
	public void FuelAid() {
		if (controller.countryDatas[controller.playerCountry].fuel >= 50) {
			controller.countryDatas[controller.playerCountry].fuel -= 50;
			controller.countryDatas[country].fuel += 50;
		}
	}
	public void NukeAid() {
		Nuclear p = Instantiate(nukePrefab, GameObject.Find("Canvas").transform).GetComponent<Nuclear>();
		p.transform.position = new Vector2(Screen.width / 2f, Screen.height / 2f);
		p.country = country;
		p.controller = controller;
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

}
