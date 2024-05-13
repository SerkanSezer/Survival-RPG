using System.IO;
using UnityEngine;
public static class TimeSaveManager {

    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/Time/";
    public static void Init() {
        if (!Directory.Exists(SAVE_FOLDER)) {
            Directory.CreateDirectory(SAVE_FOLDER);
            SaveFirstTime();
        }
    }

    public static void Save() {
        TotalTime saveTime = new TotalTime();
        saveTime.totalPlayTime = Load() + (int)Time.realtimeSinceStartup;
        string saveString = JsonUtility.ToJson(saveTime);
        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
    }

    public static int Load() {
        if (Directory.Exists(SAVE_FOLDER)) {
            string saveString = File.ReadAllText(SAVE_FOLDER + "save.txt");
            TotalTime saveTime = JsonUtility.FromJson<TotalTime>(saveString);
            int totalTime = saveTime.totalPlayTime;
            return totalTime;
        }
        else {
            return 1;
        }
    }
    public static void SaveFirstTime() {
        TotalTime saveTime = new TotalTime();
        saveTime.totalPlayTime = 1;
        string saveString = JsonUtility.ToJson(saveTime);
        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
    }

}

public class TotalTime {
    public int totalPlayTime; 
}
