using System.Collections.Generic;
using System.IO;
using UnityEngine;
public static class PlayerResourceSaveManager {

    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/PlayerResource/";
    public static void Init() {
        if (!Directory.Exists(SAVE_FOLDER)) {
            Directory.CreateDirectory(SAVE_FOLDER);
            SaveFirstTime();
        }
        else {
            Load();
        }
    }

    public static void Save() {
        int i = 1;
        foreach (var resource in ResourceManager.instance.GetResourceList()) {
            PlayerResourceSave savePlayerResource = new PlayerResourceSave();
            savePlayerResource.resourceSO = resource.resourceSO;
            savePlayerResource.amount = resource.amount;
            string saveString = JsonUtility.ToJson(savePlayerResource);
            File.WriteAllText(SAVE_FOLDER + "save" + i + ".txt", saveString);
            i++;
        }
    }

    public static void Load() {
        List<PlayerResource> savePlayerResourceList = new List<PlayerResource>();
        if (Directory.Exists(SAVE_FOLDER)) {
            for (int i = 1; i <= 3; i++) {
                string saveString = File.ReadAllText(SAVE_FOLDER + "save"+i+".txt");
                PlayerResource savePlayerResource = JsonUtility.FromJson<PlayerResource>(saveString);
                savePlayerResourceList.Add(savePlayerResource);
            }

            foreach (var playerResource in ResourceManager.instance.GetResourceList()) {
                foreach (var savePlayerResource in savePlayerResourceList) {
                    if (playerResource.resourceSO == savePlayerResource.resourceSO) {
                        playerResource.amount = savePlayerResource.amount;
                    }
                }
            }
            ResourceManager.instance.RefreshResource();
        }
        else {
            Debug.Log("No Resource Save Directory");
        }
    }
    public static void SaveFirstTime() {
        int i = 1;
        foreach (var resource in ResourceManager.instance.GetResourceList()) {
            PlayerResourceSave savePlayerResource = new PlayerResourceSave();
            savePlayerResource.resourceSO = resource.resourceSO;
            savePlayerResource.amount = 1000;
            string saveString = JsonUtility.ToJson(savePlayerResource);
            File.WriteAllText(SAVE_FOLDER + "save"+i+".txt", saveString);
            i++;
        }
    }

}

public class PlayerResourceSave {
    public ResourceSO resourceSO;
    public int amount;

}
