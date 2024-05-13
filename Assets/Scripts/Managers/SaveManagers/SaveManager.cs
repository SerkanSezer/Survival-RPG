using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private void Awake() {
        instance = this;
        Time.timeScale = 1;
        CollectibleResourceSaveManager.Init();
        ResourceSaveManager.Init();
    }
    private void Start() {
        InitAllSaveData();
    }

    public void InitAllSaveData() {
        TimeSaveManager.Init();
        PlayerResourceSaveManager.Init();
        PlayerInventorySaveManager.Init();
        PlayerVisualSaveManager.Init();
    }
    public static void SaveAllGameData() {
        TimeSaveManager.Save();
        PlayerResourceSaveManager.Save();
        PlayerInventorySaveManager.Save();
        CollectibleResourceSaveManager.Save();
        ResourceSaveManager.Save();
        PlayerVisualSaveManager.Save();
    }

}
