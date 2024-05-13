using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class ResourceSaveManager {
    public static ResourceSave rSave = new ResourceSave();
    public static ResourceSave rList = new ResourceSave();
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/Resources/";
    public static void Init() {

        if (!Directory.Exists(SAVE_FOLDER)) {
            Directory.CreateDirectory(SAVE_FOLDER);
            SaveFirstTime();
        }
    }
    public static List<RSave> Load() {
        if (Directory.Exists(SAVE_FOLDER)) {
            string saveString = File.ReadAllText(SAVE_FOLDER + "save.txt");
            rList = JsonUtility.FromJson<ResourceSave>(saveString);
            return rList.hashResources;
        }
        else {
            Debug.Log("No C Resource Save Directory");
            return null;
        }
    }
    public static void Save() {
        string saveString = JsonUtility.ToJson(rList);
        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
    }

    public static void AddResource(GameObject gObject) {
        rSave.hashResources.Add(new RSave { hashResource = Animator.StringToHash(gObject.name), amountResource = gObject.GetComponent<Resource>().GetInitResourceAmount() });
    }

    public static void SaveFirstTime() {
        rList.hashResources = rSave.hashResources.ToList();
        string saveString = JsonUtility.ToJson(rSave);
        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
    }
    public static void DecreaseResource(int hashR, int amount) {
        foreach (var hashResource in rList.hashResources.ToList()) {
            if (hashResource.hashResource == hashR) {
                if (amount <= 0) {
                    rList.hashResources.Remove(hashResource);
                }
                else {
                    hashResource.amountResource = amount;
                }
            }
        }
    }


}

public class ResourceSave {
    public List<RSave> hashResources = new List<RSave>();
}

[Serializable]
public class RSave {
    public int hashResource;
    public int amountResource;
}
