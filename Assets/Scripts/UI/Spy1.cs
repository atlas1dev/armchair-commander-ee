using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class Spy1 : MonoBehaviour
{
    public Button yesButton, noButton;
    public GameObject spyPrefab;

    [HideInInspector]
    public string country;
    [HideInInspector]
    public Controller controller;

    void Start()
    {
        if (controller.countryDatas[controller.playerCountry].industry < 100) {
            yesButton.interactable = false;
        }
    }

    void Update()
    {
        
    }

    public void Enter() {
        Spy2 p = Instantiate(spyPrefab, GameObject.Find("Canvas").transform).GetComponent<Spy2>();
		p.transform.position = new Vector2(Screen.width / 2f, Screen.height / 2f);
		p.industry = controller.countryDatas[country].industry;
        p.manpower = controller.countryDatas[country].manpower;
        p.fuel = controller.countryDatas[country].fuel;
        p.nukes1 = controller.countryDatas[country].nukes[0];
        p.nukes2 = controller.countryDatas[country].nukes[1];
        p.cname = country;
        controller.countryDatas[controller.playerCountry].industry -= 100;

        Destroy(gameObject);
    }

    public void Exit() {
        Destroy(gameObject);
    }
}
