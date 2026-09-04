using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RecentMessages : MonoBehaviour {
	string language = "English";
	bool messageReady = false;
	public Image RedDotImage;
	void Start() {
		language = MyPlayerPrefs.instance.GetString("language");
		StartCoroutine(GetMessage());
	}
	public GameObject popupMsgPrefab;
	string displayedMessage;
	IEnumerator GetMessage() {
		//			UnityWebRequest r = UnityWebRequest.Get("https://pastebin.com/raw/vV2bH6ap");
//yield return r.SendWebRequest();
			//displayedMessage = r.downloadHandler.text;
		if (language == "English") {
		} else if (language == "Chinese") {
			
		} else if (language == "Japanese") {
			
		} else {
			
		}
		MyPlayerPrefs.instance.SetString("lastMessage", displayedMessage);
		messageReady = true;
	}
	//called to show message
	public void ShowMessage() {
		if (messageReady) {
			if (language == "Chinese") {
				//MyPlayerPrefs.instance.SetString("messages_chinese", displayedMessage);
				//RedDotImage.enabled = false;
			} else {
				//MyPlayerPrefs.instance.SetString("messages_english", displayedMessage);
				//RedDotImage.enabled = false;
			}
			RedDotImage.enabled = false;
			CreatePopup(popupMsgPrefab, displayedMessage);
		}
	}
	void CreatePopup(GameObject g, string msg) {
		GameObject insItem = Instantiate(g, GameObject.Find("Canvas").transform);
		insItem.transform.position = new Vector2(Screen.width / 2f, Screen.height / 2f);
		insItem.GetComponent<IngamePopup>().couponCode.text = msg;
	}
	void Update() {

	}
}
