using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PlayerVisualSaveManager
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/PlayerVisual/";
    private static PlayerVisualSave visualListLoad = new PlayerVisualSave();
    public static void Init() {

        if (!Directory.Exists(SAVE_FOLDER)) {
            Directory.CreateDirectory(SAVE_FOLDER);
            SaveFirstTime();
        }
    }
    public static List<ItemSO> Load() {
        if (Directory.Exists(SAVE_FOLDER)) {
            string saveString = File.ReadAllText(SAVE_FOLDER + "save.txt");
            visualListLoad = JsonUtility.FromJson<PlayerVisualSave>(saveString);
            return visualListLoad.visualList;
        }
        else {
            Debug.Log("No C Resource Save Directory");
            return null;
        }
    }
    public static void Save() {
        string saveString = JsonUtility.ToJson(visualListLoad);
        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
    }
    public static void SaveFirstTime() {
        string saveString = JsonUtility.ToJson(visualListLoad);
        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
    }

    public static void AddItem(ItemSO itemSO) {
        if (!visualListLoad.visualList.Contains(itemSO)) {
            visualListLoad.visualList.Add(itemSO);
        }
    }

    public static void RemoveItem(ItemSO itemSO) {
        if (visualListLoad.visualList.Contains(itemSO)) {
            visualListLoad.visualList.Remove(itemSO);
        }
    }
}

public class PlayerVisualSave {
    public List<ItemSO> visualList = new List<ItemSO>();
}
