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
		UnityWebRequest r = UnityWebRequest.Get("https://raw.githubusercontent.com/atlas1dev/armchair-commander-ee/refs/heads/main/Version.txt");
		yield return r.SendWebRequest();
		displayedMessage = r.downloadHandler.text;
		if (displayedMessage != "BETA 1.1") {
			print(displayedMessage);
			if (language == "English") {
				displayedMessage = "An update is available! Check the Discord for more information!";
			} else if (language == "Chinese") {
				displayedMessage = "最新消息正在更新！请访问Discord获取更多信息！";
			} else if (language == "Japanese") {
				displayedMessage = "最新情報が掲載されています!詳細はDiscordをチェックしてください!";
			} else if (language == "Russian") {
				displayedMessage = "Доступно обновление! Проверьте Discord для получения дополнительной информации!";
			} else if (language == "Spanish") {
				displayedMessage = "¡Hay una actualización disponible! ¡Consulta el Discord para más información!";
			} else if (language == "French") {
				displayedMessage = "Une mise à jour est disponible ! Consultez le Discord pour plus d'informations !";
			} else {

			}
			RedDotImage.enabled = true;
		} else {
			if (language == "English") {
				displayedMessage = "Welcome to Armchair Commander version BETA 1.0.\nChangelog: https://github.com/atlas1dev/armchair-commander-ee/blob/main/Changelog.txt";
			} else if (language == "Chinese") {
				displayedMessage = "欢迎来到《扶手椅指挥官》版本 BETA 1.0.\n更新日志： https://github.com/atlas1dev/armchair-commander-ee/blob/main/Changelog.txt";
			} else if (language == "Japanese") {
				displayedMessage = "アームチェアコマンダーバージョンBETA 1.0へようこそ。\nChangelog: https://github.com/atlas1dev/armchair-commander-ee/blob/main/Changelog.txt";
			} else if (language == "Spanish") {
				displayedMessage = "Bienvenidos a la versión de Armchair Commander BETA 1.0.\nChangelog: https://github.com/atlas1dev/armchair-commander-ee/blob/main/Changelog.txt";
			} else if (language == "French") {
				displayedMessage = "Bienvenue dans la version BETA 1.0 de Armchair Commander. Changelog: https://github.com/atlas1dev/armchair-commander-ee/blob/main/Changelog.txt";
			} else if (language == "Russian") {
				displayedMessage = "Добро пожаловать в версию Armchair Commander BETA 1.0.\nChangelog: https://github.com/atlas1dev/armchair-commander-ee/blob/main/Changelog.txt";
			} else {

			}
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
