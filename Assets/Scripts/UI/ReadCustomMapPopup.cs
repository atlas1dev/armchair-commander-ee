using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadCustomMapPopup : MonoBehaviour
{
    public GameObject mapPrefab;
    public Transform content;

    public Dictionary<string, string> mapDictionary;
    public string[] maps;

    void Start()
    {
        string targetFolder = Path.Combine(Application.persistentDataPath, "CustomMaps");
        if (!Directory.Exists(targetFolder))
        {
            Directory.CreateDirectory(targetFolder);
        }

        maps = Directory.GetFiles(targetFolder, "*.txt", SearchOption.TopDirectoryOnly);

        foreach (string map in maps)
        {
            CustomMap customMap = Instantiate(mapPrefab, content).GetComponent<CustomMap>();
            customMap.mapName = Path.GetFileNameWithoutExtension(map);
        }
    }

    public void LoadCustomMapFile(string filePath)
    {
        try
        {
            MapInfo m = JsonUtility.FromJson<MapInfo>(File.ReadAllText(filePath));
            MyPlayerPrefs.instance.SetInt("map", MyPlayerPrefs.instance.GetInt("currentSaveIndex"));

            if (MyPlayerPrefs.instance.GetString("customDataAll") == "")
            {
                MyPlayerPrefs.instance.SetString("customDataAll", MyPlayerPrefs.instance.GetInt("currentSaveIndex").ToString());
            }
            else
            {
                MyPlayerPrefs.instance.SetString("customDataAll", MyPlayerPrefs.instance.GetString("customDataAll") + "," + MyPlayerPrefs.instance.GetInt("currentSaveIndex"));
            }


            MyPlayerPrefs.instance.SetInt("currentSaveIndex", MyPlayerPrefs.instance.GetInt("map") + 1);

            MyPlayerPrefs.instance.SetString("customData" + MyPlayerPrefs.instance.GetInt("map"), File.ReadAllText(filePath));
            MyPlayerPrefs.instance.SetInt("custom", 1);
            MyPlayerPrefs.instance.SetInt("editor", 1);

            SceneManager.LoadScene(1);

            return;
        }
        catch
        {
            print("Failed");
        }
    }
    public void Dismiss()
    {
        Destroy(gameObject);
    }
}