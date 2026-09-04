using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CustomMap : MonoBehaviour
{
    public ReadCustomMapPopup readCustomMapPopup;
    public Text mapNameText;
    public Button openButton;

    public string mapName;

    void Start()
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.WindowsEditor) {
			openButton.gameObject.SetActive(false);
        }
        readCustomMapPopup = FindFirstObjectByType<ReadCustomMapPopup>();
        mapNameText.text = mapName;
    }

    public void OpenFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "CustomMaps", mapName + ".txt");
		string fullPath = Path.GetFullPath(filePath);
		if (File.Exists(filePath)) {
            try 
            {
			    System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + fullPath + "\"");
            }
            catch 
            {
                Debug.LogWarning("Unable to open file.");
            }
		} else {
			Debug.LogWarning("File does not exist: " + filePath);
		}
    }

    public void LoadCustomMapFile()
    {
        readCustomMapPopup.LoadCustomMapFile(Path.Combine(Application.persistentDataPath, "CustomMaps", mapName + ".txt"));
    }

    public void DeleteCustomMapFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "CustomMaps", mapName + ".txt");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Destroy(gameObject);
            Debug.Log("File deleted successfully. " + filePath);
        }
        else
        {
            Debug.Log("File not found.");
        }
    }
}
