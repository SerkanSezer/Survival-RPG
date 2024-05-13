using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform settingsPanel;
    [SerializeField] private Transform tutorialPanel;
    [SerializeField] private Transform bG;
    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings() {
        bG.gameObject.SetActive(true);
        AudioManager.instance.LoadMusicAndSoundVolume();
        settingsPanel.gameObject.SetActive(true);
        tutorialPanel.gameObject.SetActive(false);
    }

    public void CloseSettings() {
        bG.gameObject.SetActive(false);
        AudioManager.instance.SaveMusicSoundVolume();
        settingsPanel.gameObject.SetActive(false);
        tutorialPanel.gameObject.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }

}
