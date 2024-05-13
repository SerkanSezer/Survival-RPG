using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class CollectibleResourceSaveManager
{
    public static CrSave crSave = new CrSave();
    public static CrSave crList = new CrSave();
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/CollectibleResources/";
    public static void Init() {
        
        if (!Directory.Exists(SAVE_FOLDER)) {
            Directory.CreateDirectory(SAVE_FOLDER);
            SaveFirstTime();
        }
    }
    public static CrSave Load() {
        if (Directory.Exists(SAVE_FOLDER)) {
            string saveString = File.ReadAllText(SAVE_FOLDER + "save.txt");
            crList = JsonUtility.FromJson<CrSave>(saveString);
            return crList;
        }
        else {
            return new CrSave();
        }
    }
    public static void Save() {
        string saveString = JsonUtility.ToJson(crList);
        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
    }

    public static void AddCollectibleResource(GameObject gObject) {
        crSave.hashCr.Add(Animator.StringToHash(gObject.name));
    }

    public static void SaveFirstTime() {
        crList.hashCr = crSave.hashCr.ToList();
        string saveString = JsonUtility.ToJson(crSave);
        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
    }

}

public class CrSave {
    public List<int> hashCr = new List<int>();

}
