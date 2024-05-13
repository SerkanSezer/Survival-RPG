using System.Collections.Generic;
using System.IO;
using UnityEngine;
public static class PlayerInventorySaveManager {

    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/PlayerInventory/";
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
        ClearDirectory(SAVE_FOLDER);
        int i = 1;
        foreach (var (Key, Value) in PlayerInteract.instance.playerInventory.GetItemList()) {
            PlayerInventorySave savePlayerInventory = new PlayerInventorySave();
            savePlayerInventory.hashItem = Animator.StringToHash(Key.name);
            savePlayerInventory.amount = Value;
            string saveString = JsonUtility.ToJson(savePlayerInventory);
            File.WriteAllText(SAVE_FOLDER + "save" + i + ".txt", saveString);
            i++;
        }
    }

    public static void Load() {
        List<PlayerInventorySave> savePlayerInventoryList = new List<PlayerInventorySave>();
        int fileCount = Directory.GetFiles(SAVE_FOLDER,"save*txt",SearchOption.TopDirectoryOnly).Length;
        if (Directory.Exists(SAVE_FOLDER)) {
            for (int i = 1; i <= fileCount; i++) {
                string saveString = File.ReadAllText(SAVE_FOLDER + "save" + i + ".txt");
                PlayerInventorySave savePlayerIventory = JsonUtility.FromJson<PlayerInventorySave>(saveString);
                savePlayerInventoryList.Add(savePlayerIventory);
            }
            PlayerInteract.instance.playerInventory.SetItemList(savePlayerInventoryList);
        }
        else {
            Debug.Log("No Resource Save Directory");
        }
    }
    public static void SaveFirstTime() {
        int i = 1;
        foreach (var (Key, Value) in PlayerInteract.instance.playerInventory.GetItemList()) {
            PlayerInventorySave savePlayerInventory = new PlayerInventorySave();
            savePlayerInventory.hashItem =  Animator.StringToHash(Key.name);
            savePlayerInventory.amount = Value;
            string saveString = JsonUtility.ToJson(savePlayerInventory);
            File.WriteAllText(SAVE_FOLDER + "save" + i + ".txt", saveString);
            i++;
        }
    }

    public static void ClearDirectory(string path) {
        DirectoryInfo dir = new DirectoryInfo(path);
        foreach (FileInfo file in dir.GetFiles()) {
            file.Delete();
        }
    }

}

public class PlayerInventorySave {
    public int hashItem;
    public int amount;
}
